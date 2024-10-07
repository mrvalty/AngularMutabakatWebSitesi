using eReconciliationProject.Core.Entities;

namespace eReconciliationProject.Entities.Dtos
{
    public class UserForRegisterToSecondAccountDto : UserForRegister, IDto
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
    }
}
