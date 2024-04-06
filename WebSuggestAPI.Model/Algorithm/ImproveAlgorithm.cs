using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSuggestAPI.Model.Model;

namespace WebSuggestAPI.Model.Algorithm
{
    public class ImproveAlgorithm
    {
        private DbContextWebSuggest db;
        private double s_min;
        List<List<List<SanPham>>> listtong;

        public ImproveAlgorithm(DbContextWebSuggest db,double s_min)
        {
            this.db = db;
            this.s_min = s_min;
            listtong = new List<List<List<SanPham>>>();
        }

        public List<List<List<SanPham>>> Execute()
        {
            var hoadons = db.HoaDons.ToList();
            var ldssp = db.SanPhams.ToList();
            var ts1sp = db.TanSuatMotSanPhams.ToList();
            var ts2sp = db.TanSuatHaiSanPhams.ToList();
            for (int i = 0; i < ldssp.Count; i++)
            {
                var ts1 = new TanSuatMotSanPham();
                ts1.ThuTu = i + 1;
                ts1.TanSuat = 0;
                ts1.IdSanPham = ldssp[i].IdSanPham;
                ts1sp.Add(ts1);
            }
            foreach (var hoadon in hoadons)
            {
                var dssp = System.Text.Json.JsonSerializer.Deserialize<List<SanPham>>(hoadon.SanPham);

                foreach (var sp in dssp)
                {
                    for (int i = 0; i < ts1sp.Count; i++)
                    {
                        if (ts1sp[i].IdSanPham == sp.IdSanPham)
                        {
                            ts1sp[i].TanSuat++;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < ts1sp.Count - 1; i++)
            {
                for (int j = 0; j < ts1sp.Count - i - 1; j++)
                {
                    if (ts1sp[j].TanSuat < ts1sp[j + 1].TanSuat)
                    {
                        var ts1 = new TanSuatMotSanPham
                        {
                            IdSanPham = ts1sp[j + 1].IdSanPham,
                            TanSuat = ts1sp[j + 1].TanSuat,

                        };

                        ts1sp[j + 1].IdSanPham = ts1sp[j].IdSanPham;
                        ts1sp[j + 1].TanSuat = ts1sp[j].TanSuat;

                        ts1sp[j].IdSanPham = ts1.IdSanPham;
                        ts1sp[j].TanSuat = ts1.TanSuat;
                    }
                }
            }
            for (int i = 0; i < ts1sp.Count; i++)
            {
                var ts2 = new TanSuatHaiSanPham();
                ts2.ThuTu = i + 1;
                ts2.IdHaiSanPham = ts1sp[i].IdSanPham;
                var listtong = new List<TanSuatMotSanPham>();
                for (int j = 0; j < i; j++)
                {
                    var listphu = new TanSuatMotSanPham();
                    listphu.ThuTu = j;
                    listphu.IdSanPham = ts1sp[j].IdSanPham;
                    listphu.TanSuat = 0;
                    listtong.Add(listphu);
                }
                ts2.TanSuat = System.Text.Json.JsonSerializer.Serialize(listtong);
                ts2sp.Add(ts2);
            }
            List<List<TanSuatMotSanPham>> listts2sp = new List<List<TanSuatMotSanPham>>();
            foreach (var ts in ts2sp)
            {
                List<TanSuatMotSanPham> list = new List<TanSuatMotSanPham>();
                list = System.Text.Json.JsonSerializer.Deserialize<List<TanSuatMotSanPham>>(ts.TanSuat);
                listts2sp.Add(list);
            }
            foreach (var hoadon in hoadons)
            {
                var dssp = System.Text.Json.JsonSerializer.Deserialize<List<SanPham>>(hoadon.SanPham);
                int ssp = 0;
                int ssphd = dssp.Count;
                for (int i = ts2sp.Count - 1; i >= 0; i--)
                {
                    foreach (var sp in dssp)
                    {
                        if (sp.IdSanPham == ts2sp[i].IdHaiSanPham)
                        {
                            dssp.Remove(sp);
                            foreach (var spp in dssp)
                            {
                                foreach (var dshsp in listts2sp[i])
                                {
                                    if (spp.IdSanPham == dshsp.IdSanPham)
                                    {
                                        dshsp.TanSuat++;
                                        break;
                                    }
                                }
                            }
                            ssp++;
                            break;
                        }
                    }
                    if (ssp == ssphd)
                    {
                        break;
                    }
                }
            }
            for (int i = 0; i < ts2sp.Count; i++)
            {
                ts2sp[i].TanSuat = System.Text.Json.JsonSerializer.Serialize(listts2sp[i]);
                db.TanSuatHaiSanPhams.Add(ts2sp[i]);
                db.TanSuatMotSanPhams.Add(ts1sp[i]);

            }
            db.SaveChanges();

            GetPotentialProduct();
            return this.listtong;
        }
        private async void GetPotentialProduct()
        {
                var sohoadon = db.HoaDons.Count();
                double k = s_min * sohoadon;
                var ts1sp = db.TanSuatMotSanPhams.ToList();
                var ts2sp = db.TanSuatHaiSanPhams.ToList();

                for (int i = ts1sp.Count - 1; i >= 0; i--)
                {

                    if (ts1sp[i].TanSuat < k)
                    {
                        ts2sp.Remove(ts2sp[i]);
                    }
                    else break;
                }
                foreach (var ts in ts2sp)
                {
                    var dstss = System.Text.Json.JsonSerializer.Deserialize<List<TanSuatMotSanPham>>(ts.TanSuat);
                    List<SanPham> listsp = new List<SanPham>();
                    for (int i = dstss.Count - 1; i >= 0; i--)
                    {
                        if (dstss[i].TanSuat > k)
                        {
                            listsp.Add(db.SanPhams.FirstOrDefault(c => c.IdSanPham == dstss[i].IdSanPham));
                        }
                    }
                    for (int i = 1; i <= listsp.Count; i++)
                    {
                        var items = await LayTatCaTongHop(i, listsp);
                        foreach (var item in items)
                        {
                            item.Add(db.SanPhams.FirstOrDefault(c => c.IdSanPham == ts.IdHaiSanPham));
                        }

                        listtong.Add(items);
                    }
                    List<SanPham> list1sp = new List<SanPham>();
                    list1sp.Add(db.SanPhams.FirstOrDefault(c => c.IdSanPham == ts.IdHaiSanPham));
                    List<List<SanPham>> list1sp2 = new List<List<SanPham>>();
                    list1sp2.Add(list1sp);
                    listtong.Add(list1sp2);
                }
                

        }
        private async Task<List<List<SanPham>>> LayTatCaTongHop(int k, List<SanPham> sanphams)
        {
            List<List<SanPham>> tatCaTongHop = new List<List<SanPham>>();
            List<SanPham> tongHopHienTai = new List<SanPham>();

            await LayTatCaTongHopHelper(k, sanphams, 0, tongHopHienTai, tatCaTongHop);

            return tatCaTongHop;
        }

        private async Task LayTatCaTongHopHelper(int k, List<SanPham> sanphams, int index, List<SanPham> tongHopHienTai, List<List<SanPham>> tatCaTongHop)
        {
            if (tongHopHienTai.Count == k)
            {
                tatCaTongHop.Add(new List<SanPham>(tongHopHienTai));
                return;
            }

            for (int i = index; i < sanphams.Count; i++)
            {
                tongHopHienTai.Add(sanphams[i]);
                await LayTatCaTongHopHelper(k, sanphams, i + 1, tongHopHienTai, tatCaTongHop);
                tongHopHienTai.RemoveAt(tongHopHienTai.Count - 1);
            }
        }

    }
}
