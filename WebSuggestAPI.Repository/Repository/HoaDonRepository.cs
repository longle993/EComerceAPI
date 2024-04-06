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

namespace WebSuggestAPI.Repository.Repository
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private DbContextWebSuggest db;
        private string connectionString;

        public HoaDonRepository(DbContextWebSuggest db, string connectionString)
        {
            this.db = db;
            this.connectionString = connectionString;
        }

        public Task<ErrorMessageInfo> AddHoaDon(HoaDon newhoadon)
        {
            throw new NotImplementedException();
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
