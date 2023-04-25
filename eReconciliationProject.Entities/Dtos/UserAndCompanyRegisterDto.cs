using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Entities;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Entities.Dtos
{
    public class UserAndCompanyRegisterDto :IDto
    {
        public UserForRegister userForRegister { get; set; }
        public Company company { get; set; }
    }
}
