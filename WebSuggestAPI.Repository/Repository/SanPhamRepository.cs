using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using WebSuggestAPI.Interface.Interface;
using WebSuggestAPI.Model;
using WebSuggestAPI.Model.Model;
using WebSuggestAPI.Model.Algorithm;
using System.Diagnostics;

namespace WebSuggestAPI.Repository.Repository
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private DbContextWebSuggest db;
        private string connectionString;

        public SanPhamRepository(DbContextWebSuggest db, IConfiguration configuration)
        {
            this.db = db;
            this.connectionString = configuration.GetConnectionString("connectionString");
        }

        public async Task<ErrorMessageInfo> GetProduct()
        {
            ErrorMessageInfo error = new ErrorMessageInfo();
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                error.isSuccess = true;
                error.data = JsonSerializer.Serialize(await db.SanPhams
                                     .Include(s => s.HinhAnhSanPhams)
                                     .Select(p => new
                                     {
                                         p.IdSanPham,
                                         p.TenSanPham,
                                         p.GiaSp,
                                         p.IdLoaiSp,
                                         HinhAnh = p.HinhAnhSanPhams.Select(hinhanh => hinhanh.HinhAnh).ToList()
                                     })
                                     .ToListAsync(), options);
            }
            catch (Exception ex)
            {
                error.isErrorEx = true;
                error.message = ex.Message;
                error.error_code = "ErrorProduct001";
            }
            return error;

        }

        public async Task<ErrorMessageInfo> GetProductById(string id)
        {
            ErrorMessageInfo error = new ErrorMessageInfo();
            try
            {
                error.isSuccess = true;
                error.data = await db.SanPhams
                    .Include(s => s.HinhAnhSanPhams)
                    .Where(p => p.IdSanPham == id)
                    .Select(p => new
                    {
                        p.IdSanPham,
                        p.TenSanPham,
                        p.GiaSp,
                        p.IdLoaiSp,
                        HinhAnh = p.HinhAnhSanPhams.Select(hinhanh => hinhanh.HinhAnh).ToList()
                    })
                    .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                error.isErrorEx = true;
                error.message = ex.Message;
                error.error_code = "ErrorGetID";
            }
            return error;
        }

        public async Task<ErrorMessageInfo> GetProductByName(string name)
        {
            ErrorMessageInfo error = new ErrorMessageInfo();
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                error.isSuccess = true;
                error.data = JsonSerializer.Serialize(await db.SanPhams
                                     .Include(s => s.HinhAnhSanPhams)
                                     .Where(p => p.TenSanPham.Contains(name))
                                     .Select(p => new
                                     {
                                         p.IdSanPham,
                                         p.TenSanPham,
                                         p.GiaSp,
                                         p.IdLoaiSp,
                                         HinhAnh = p.HinhAnhSanPhams.Select(hinhanh => hinhanh.HinhAnh).ToList()
                                     })
                                     .ToListAsync(), options);
            }
            catch (Exception ex)
            {
                error.isErrorEx = true;
                error.message = ex.Message;
                error.error_code = "ErrorGetName";
            }
            return error;

        }

        public async Task<ErrorMessageInfo> GetProductByType(string type)
        {
            ErrorMessageInfo error = new ErrorMessageInfo();
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                error.isSuccess = true;
                error.data = JsonSerializer.Serialize(await db.SanPhams
                                     .Include(s => s.HinhAnhSanPhams)
                                     .Where(p => p.IdLoaiSp == type)
                                     .Select(p => new
                                     {
                                         p.IdSanPham,
                                         p.TenSanPham,
                                         p.GiaSp,
                                         p.IdLoaiSp,
                                         HinhAnh = p.HinhAnhSanPhams.Select(hinhanh => hinhanh.HinhAnh).ToList()
                                     })
                                     .ToListAsync(), options);
            }
            catch (Exception ex)
            {
                error.isErrorEx = true;
                error.message = ex.Message;
                error.error_code = "ErrorGetProductByType";
            }
            return error;
        }

        public async Task<ErrorMessageInfo> GetProductType()
        {
            ErrorMessageInfo error = new ErrorMessageInfo();
            try
            {
                error.isSuccess = true;
                error.data = await db.LoaiSanPhams.ToListAsync();
            }
            catch (Exception ex)
            {
                error.message = ex.Message;
                error.isErrorEx = true;
                error.error_code = "ErrorProductTypeGET";
            }
            return error;
        }

        public async Task<ErrorMessageInfo> SuggestProduct(string id)
        {
            ErrorMessageInfo error = new ErrorMessageInfo();
            try
            {
                var ts1sp = db.TanSuatMotSanPhams.ToList();
                var ts2sp = db.TanSuatHaiSanPhams.ToList();
                db.RemoveRange(ts1sp);
                db.RemoveRange(ts2sp);
                db.SaveChanges();

                ImproveAlgorithm algorithm = new ImproveAlgorithm(db, 0.15);
                error.data = algorithm.Execute();
                error.isSuccess = true;
            }
            catch (Exception e)
            {
                error.error_code = "ErrorAlgorithm";
                error.isErrorEx = true;
            }
            return error;

        }
    }
}
