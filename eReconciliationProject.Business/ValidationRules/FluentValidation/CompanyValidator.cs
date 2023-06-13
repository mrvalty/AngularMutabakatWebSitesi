using eReconciliationProject.Entities.Concrete;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.ValidationRules.FluentValidation
{
    public class CompanyValidator :AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Şirket adı boş olamaz.");
            RuleFor(x => x.Name).MinimumLength(4).WithMessage("Şirket adı en az 4 karakter olmalı.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Şirket adresi boş olamaz.");
        }
    }
}
