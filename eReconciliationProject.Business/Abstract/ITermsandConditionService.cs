using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Abstract
{
    public interface ITermsandConditionService
    {
        IResult Update(TermsandCondition termsandCondition);
        IDataResult<TermsandCondition> Get();

    }
}
