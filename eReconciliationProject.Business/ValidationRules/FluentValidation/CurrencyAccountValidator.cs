using eReconciliationProject.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.ValidationRules.FluentValidation
{
    public class CurrencyAccountValidator :AbstractValidator<CurrencyAccount>
    {
        public CurrencyAccountValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Firma Adı Boş olamaz");
            RuleFor(x => x.Name).MinimumLength(4).WithMessage("Firma Adı En Az 4 Karakter Olmalıdır.");
            
            RuleFor(x => x.Address).NotEmpty().WithMessage("Firma Adresi Boş olamaz");
            RuleFor(x => x.Address).MinimumLength(4).WithMessage("Firma Adresi En Az 4 Karakter Olmalıdır.");
        }
    }
}
