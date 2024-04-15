using Microsoft.AspNetCore.Mvc;
using WebSuggestAPI.Interface.Interface;
using WebSuggestAPI.Model;

namespace WebSuggestAPI.Controllers
{
    [Route("api/sanpham")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private ISanPhamRepository sanPhamRepository;

        public SanPhamController(ISanPhamRepository sanPhamRepository)
        {
            this.sanPhamRepository = sanPhamRepository;
        }
        [HttpGet("get-all-product")]
        public async Task<ActionResult<ResponseInfo>> GetProduct()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                ErrorMessageInfo errorInfo = await sanPhamRepository.GetProduct();
                response.statusCode = System.Net.HttpStatusCode.OK;
                if(errorInfo.isErrorEx || !errorInfo.isSuccess)
                {
                    response.error_code = errorInfo.error_code;
                    response.message = errorInfo.message;
                    return BadRequest(response);
                }
                response.data = errorInfo.data;
                return await Task.FromResult(response);
            }
            catch(Exception ex)
            {
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
                response.message=ex.Message;
                return BadRequest(response);
            }
        }
        [HttpGet("get-product-type")]
        public async Task<ActionResult<ResponseInfo>> GetProductType()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                ErrorMessageInfo errorInfo = await sanPhamRepository.GetProductType();
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
        [HttpGet("get-product-by-id")]
        public async Task<ActionResult<ResponseInfo>> GetProductById(string id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                ErrorMessageInfo errorInfo = await sanPhamRepository.GetProductById(id);
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
        [HttpGet("get-product-by-name")]
        public async Task<ActionResult<ResponseInfo>> GetProductByName(string name)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                ErrorMessageInfo errorInfo = await sanPhamRepository.GetProductByName(name);
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
        [HttpGet("get-product-by-type")]
        public async Task<ActionResult<ResponseInfo>> GetProductByType(string type)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                ErrorMessageInfo errorInfo = await sanPhamRepository.GetProductByType(type);
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

        [HttpGet("get-suggest-product")]
        public async Task<ActionResult<ResponseInfo>> GetSuggestProduct()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                ErrorMessageInfo errorInfo = await sanPhamRepository.SuggestProduct();
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
        [HttpGet("get-frequence-product")]
        public async Task<ActionResult<ResponseInfo>> GetFrequenceProducts()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                ErrorMessageInfo errorInfo = await sanPhamRepository.GetFrequenceProduct();
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
