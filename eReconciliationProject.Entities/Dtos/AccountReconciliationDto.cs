using eReconciliationProject.Core.Entities;
using eReconciliationProject.Entities.Concrete;

namespace eReconciliationProject.Entities.Dtos
{
    public class AccountReconciliationDto : AccountReconciliaton, IDto
    {
        public string CompanyName { get; set; }
        public string CompanyTaxDepartment { get; set; }
        public string CompanyTaxIdNumber { get; set; }
        public string CompanyIdentityNumber { get; set; }

        public string AccountName { get; set; }
        public string AccountTaxDepartment { get; set; }
        public string AccountTaxIdNumber { get; set; }
        public string AccountIdentityNumber { get; set; }
        public string AccountEmail { get; set; }
        public string CurrencyCode { get; set; }
    }
}
