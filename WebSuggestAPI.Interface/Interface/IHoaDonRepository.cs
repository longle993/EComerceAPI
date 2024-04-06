using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSuggestAPI.Model;
using WebSuggestAPI.Model.Model;

namespace WebSuggestAPI.Interface.Interface
{
    public interface IHoaDonRepository
    {
        Task<ErrorMessageInfo> GetHoaDon();
        Task<ErrorMessageInfo> GetHoaDonByDay(DateTime datefrom, DateTime dateto);
        Task<ErrorMessageInfo> AddHoaDon(HoaDon newhoadon);

    }
}
