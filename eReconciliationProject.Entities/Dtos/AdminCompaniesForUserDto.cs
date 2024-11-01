using eReconciliationProject.Core.Entities;
using eReconciliationProject.Entities.Concrete;

namespace eReconciliationProject.Entities.Dtos;
public class AdminCompaniesForUserDto : Company, IDto
{
    public bool IsThere { get; set; }
}
