using eReconciliationProject.Core.Entities;
using eReconciliationProject.Entities.Concrete;

namespace eReconciliationProject.Entities.Dtos
{
    public class CompanyDto : IDto
    {
        public Company Company { get; set; }
        public int UserId { get; set; }
    }
}
