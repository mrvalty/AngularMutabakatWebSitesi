using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.BusinessAspect;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.DA.Repositories.Concrete;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class TermsandCoditionManager : ITermsandConditionService
    {

        private readonly ITermsandConditionsRepository _termsandConditionRepo;

        public TermsandCoditionManager(ITermsandConditionsRepository termsandConditionRepo)
        {
            _termsandConditionRepo = termsandConditionRepo;
        }

        public IDataResult<TermsandCondition> Get()
        {
            return new SuccessDataResult<TermsandCondition>(_termsandConditionRepo.GetList().FirstOrDefault());
        }

        [SecuredOperation("Admin")]
        public IResult Update(TermsandCondition termsandCondition)
        {
            var result = _termsandConditionRepo.GetList().FirstOrDefault();
            if (result != null)
            {

                result.Description = termsandCondition.Description;
                _termsandConditionRepo.Update(result);
            }
            else
            {
                _termsandConditionRepo.Add(termsandCondition);
            }
            return new SuccessResult(Messages.UpdateTermsandConditions);
        }
    }
}
