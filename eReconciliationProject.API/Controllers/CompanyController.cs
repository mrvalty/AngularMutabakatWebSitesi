using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        [HttpGet("getcompanylist")]
        public IActionResult GetCompanyList()
        {
            var result = _companyService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getcompany")]
        public IActionResult GetById(int id)
        {
            var result = _companyService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("addCompanyAndUserCompany")]
        public IActionResult AddCompanyAndUserCompany(CompanyDto companyDto)
        {
            var result = _companyService.AddCompanyAndUserCompany(companyDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("updateCompany")]
        public IActionResult UpdateCompanyAndUserCompany(Company company)
        {
            var result = _companyService.Update(company);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getCompanyListByUserid")]
        public IActionResult GetCompanyListByUserId(int userId)
        {
            var result = _companyService.GetListByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("changeStatusCompany")]
        public IActionResult ChangeStatusCompany(Company company)
        {
            if (company.IsActive)
            {
                company.IsActive = false;
            }
            else
            {
                company.IsActive = true;
            }

            var result = _companyService.Update(company);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
