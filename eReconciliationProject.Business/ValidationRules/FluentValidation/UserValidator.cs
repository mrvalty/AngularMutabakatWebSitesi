using eReconciliationProject.Core.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.ValidationRules.FluentValidation
{
    public class UserValidator :AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(x => x.Name).MinimumLength(4).WithMessage("Kullanıcı adı en az 4 karakter olmalıdır");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail adresi boş olamaz");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi yazın.");


        }
    }
}
