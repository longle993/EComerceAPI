using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSuggestAPI.Model;

namespace WebSuggestAPI.Interface.Interface
{
    public interface ISanPhamRepository
    {
        Task<ErrorMessageInfo> GetProduct();
        Task<ErrorMessageInfo> GetProductById(string id);
        Task<ErrorMessageInfo> GetProductByName(string name);
        Task<ErrorMessageInfo> GetProductByType(string type);
        Task<ErrorMessageInfo> SuggestProduct(string productId);
        Task<ErrorMessageInfo> GetProductType();
        Task<ErrorMessageInfo> GetFrequenceProduct();

    }
}
