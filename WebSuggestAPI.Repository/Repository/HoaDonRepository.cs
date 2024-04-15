using Microsoft.EntityFrameworkCore;
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
using Microsoft.Extensions.Configuration;

namespace WebSuggestAPI.Repository.Repository
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private DbContextWebSuggest db;
        private string connectionString;

        public HoaDonRepository(DbContextWebSuggest db, IConfiguration configuration)
        {
            this.db = db;
            this.connectionString = configuration.GetConnectionString("connectionString");
        }

        public async Task<ErrorMessageInfo> AddHoaDon(HoaDon newhoadon)
        {
            ErrorMessageInfo error = new ErrorMessageInfo();
            try
            {
                
                db.HoaDons.Add(newhoadon);
                db.SaveChanges();
                error.isSuccess = true;
                error.message = "Add new bill Success";
            }
            catch(Exception ex)
            {
                error.isErrorEx = true;
                error.message = ex.Message;
                error.error_code = "ErrorAddHoaDon";
            }
            return error;
        }

        public async Task<ErrorMessageInfo> GetHoaDon()
        {
            ErrorMessageInfo error = new ErrorMessageInfo();
            try
            {
                error.isSuccess = true;
                error.data = await db.HoaDons.ToListAsync();
            }
            catch (Exception ex)
            {
                error.isErrorEx = true;
                error.message = ex.Message;
                error.error_code = "ErrorHoaDon";
            }
            return error;
        }

        public Task<ErrorMessageInfo> GetHoaDonByDay(DateTime datefrom, DateTime dateto)
        {
            throw new NotImplementedException();
        }
    }
}
