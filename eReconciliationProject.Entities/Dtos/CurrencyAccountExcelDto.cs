using eReconciliationProject.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace eReconciliationProject.Entities.Dtos
{
    public class CurrencyAccountExcelDto : IDto
    {
        public IFormFile File { get; set; }

        public int CompanyId { get; set; }
    }
}
