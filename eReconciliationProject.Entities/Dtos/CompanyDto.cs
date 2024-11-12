using eReconciliationProject.Core.Entities;
using eReconciliationProject.Entities.Concrete;

namespace eReconciliationProject.Entities.Dtos
{
    public class CompanyDto : Company, IDto
    {
        public int UserId { get; set; }
    }
}
