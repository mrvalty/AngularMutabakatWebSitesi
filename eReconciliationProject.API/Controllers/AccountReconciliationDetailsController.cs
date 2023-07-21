using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReconciliationDetailsController : ControllerBase
    {

        private readonly IAccountReconciliationDetailService _accountReconciliationDetailService;

        public AccountReconciliationDetailsController(IAccountReconciliationDetailService accountReconciliationDetailService)
        {
            _accountReconciliationDetailService = accountReconciliationDetailService;
        }

        [HttpPost("add")]
        public IActionResult Add(AccountReconciliationDetail accountReconciliaton)
        {
            var result = _accountReconciliationDetailService.Add(accountReconciliaton);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(AccountReconciliationDetail accountReconciliaton)
        {
            var result = _accountReconciliationDetailService.Update(accountReconciliaton);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete(AccountReconciliationDetail accountReconciliaton)
        {
            var result = _accountReconciliationDetailService.Delete(accountReconciliaton);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _accountReconciliationDetailService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public IActionResult GetList(int accountReconciliationId)
        {
            var result = _accountReconciliationDetailService.GetList(accountReconciliationId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
         
        [HttpPost("addFromExcel")]
        public IActionResult AddFromExcel(IFormFile file, int accountReconciliationId)
        {

            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + ".xlsx";
                var filePath = $"{Directory.GetCurrentDirectory()}/Content/{fileName}";
                using (FileStream stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                    stream.Flush();
                }

                var result = _accountReconciliationDetailService.AddToExcel(filePath, accountReconciliationId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Lütfen Dosya Seçiniz.");


        }
    }
}
