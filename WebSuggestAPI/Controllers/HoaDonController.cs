using Microsoft.AspNetCore.Mvc;
using WebSuggestAPI.Interface.Interface;
using WebSuggestAPI.Model;
using WebSuggestAPI.Repository.Repository;

namespace WebSuggestAPI.Controllers
{
    [Route("api/hoadon")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private IHoaDonRepository hoadonRepository;

        public HoaDonController(IHoaDonRepository hoadonRepository)
        {
            this.hoadonRepository = hoadonRepository;
        }
        [HttpGet("get-all-bill")]
        public async Task<ActionResult<ResponseInfo>> GetBill()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                ErrorMessageInfo errorInfo = await hoadonRepository.GetHoaDon();
                response.statusCode = System.Net.HttpStatusCode.OK;
                if (errorInfo.isErrorEx || !errorInfo.isSuccess)
                {
                    response.error_code = errorInfo.error_code;
                    response.message = errorInfo.message;
                    return BadRequest(response);
                }
                response.data = errorInfo.data;
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
                response.message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
