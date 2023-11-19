using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;

namespace eReconciliationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReconciliationsController : ControllerBase
    {
        private readonly IAccountReconciliationService _accountReconciliationService;

        public AccountReconciliationsController(IAccountReconciliationService accountReconciliationService)
        {
            _accountReconciliationService = accountReconciliationService;
        }

        [HttpPost("add")]
        public IActionResult Add(AccountReconciliaton accountReconciliaton)
        {
            var result = _accountReconciliationService.Add(accountReconciliaton);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(AccountReconciliaton accountReconciliaton)
        {
            var result = _accountReconciliationService.Update(accountReconciliaton);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete(AccountReconciliaton accountReconciliaton)
        {
            var result = _accountReconciliationService.Delete(accountReconciliaton);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _accountReconciliationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public IActionResult GetList(int companyId)
        {
            var result = _accountReconciliationService.GetListDto(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("addFromExcel")]
        public IActionResult AddFromExcel(IFormFile file, int companyId)
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

                var result = _accountReconciliationService.AddToExcel(filePath, companyId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Lütfen Dosya Seçiniz.");


        }

        [HttpPost("sendReconciliationMail")]
        public IActionResult SendReconciliationMail(AccountReconciliationDto accountReconciliatonDto)
        {
            var result = _accountReconciliationService.SendReconciliationMail(accountReconciliatonDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getByCode")]
        public IActionResult GetByCode(string code)
        {
            var result = _accountReconciliationService.GetByCode(code);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
