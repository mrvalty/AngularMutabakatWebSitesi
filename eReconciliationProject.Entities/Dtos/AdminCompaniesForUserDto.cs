using eReconciliationProject.Core.Entities;
using eReconciliationProject.Entities.Concrete;

namespace eReconciliationProject.Entities.Dtos;
public class AdminCompaniesForUserDto : Company, IDto
{
    public bool IsThere { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string? TaxDepartment { get; set; }
    public string? TaxIdNumber { get; set; }
    public string? IdentityNumber { get; set; }
    public DateTime AddedAt { get; set; }
    public bool IsActive { get; set; }
}
