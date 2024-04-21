using Combinatorics.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using WebSuggestAPI.Model.Model;

namespace CNDM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TMDTController : ControllerBase
    {
        private readonly DbContextWebSuggest db;
        public TMDTController(DbContextWebSuggest context)
        {
            db = context;
        }
        [HttpGet("tao-danh-sach-pho-bien")]
        public IActionResult TaoDanhSachPhoBien()
        {
            var sanphams = db.SanPhams.ToList();
            var hoadons=db.HoaDons.ToList();
            int sosp = sanphams.Count;
            Dictionary<string, Dictionary<string, int>> dsphobien = new Dictionary<string, Dictionary<string, int>>(sosp);
            Dictionary<string, int> phu = new Dictionary<string, int>(sosp);
            List<List<SanPham>> dssps = new List<List<SanPham>>();
            foreach (var hoadon in hoadons)
            {
                var items = System.Text.Json.JsonSerializer.Deserialize<List<SanPham>>(hoadon.SanPham);
                if (items.Count > 1)
                {
                    dssps.Add(items);
                }
                
            }
                foreach (var sp in sanphams)
            {
                phu.Add(sp.IdSanPham, 0);
            }
            foreach(var sp in sanphams) 
            {
                dsphobien.Add(sp.IdSanPham, phu.ToDictionary());
            }
            foreach(var dssp in dssps)
            {
                var items = new Combinations<SanPham>(dssp, 2);
                foreach (var item in items)
                {
                    string idSanPham1 = item[0].IdSanPham;
                    string idSanPham2 = item[1].IdSanPham;
                    Dictionary<string, int> innerDict1 = dsphobien[idSanPham1];
                    innerDict1[idSanPham2] ++;
                    Dictionary<string, int> innerDict2 = dsphobien[idSanPham2];
                    innerDict2[idSanPham1] ++;
                }
            }
            foreach(var pb in dsphobien)
            {
                var dspb = new DanhSachPhoBien()
                {
                    IdSanPham=pb.Key,
                    TanSuat= JsonSerializer.Serialize(pb.Value)
                };

                db.DanhSachPhoBiens.Add(dspb);
            }
            db.SaveChanges();
            return Ok();
        }
        [HttpDelete("xoa-bang-pho-bien")]
        public IActionResult XoaBangPhoBien()
        {
            var dspbs = db.DanhSachPhoBiens.ToList();
            db.RemoveRange(dspbs);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet("danh-sach-phoi-hop/{idSanPham}")]
        public IActionResult DanhSachPhoiHop(string idSanPham)
        {
            var dspbs = db.DanhSachPhoBiens
                .ToDictionary(d => d.IdSanPham, d => JsonSerializer.Deserialize<Dictionary<string, int>>(d.TanSuat));
            var sortedDspbs = dspbs[idSanPham]
                .OrderByDescending(kv => kv.Value)
                .ToList();
            var topList = sortedDspbs.Take(8).ToList();
            List<SanPham> dssp = db.SanPhams.ToList();
            List<SanPham> result = new List<SanPham>();
            topList.ForEach(suggest =>
            {
                result.Add(dssp.Where(p => p.IdSanPham == suggest.Key).SingleOrDefault());
            });
            
            return Ok(result);
        }
    }
}
