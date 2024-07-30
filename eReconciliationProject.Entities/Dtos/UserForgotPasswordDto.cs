using eReconciliationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Entities.Dtos
{
    public class UserForgotPasswordDto : IDto
    {
        public string Value { get; set; }
        public string Password { get; set; }
    }
}
