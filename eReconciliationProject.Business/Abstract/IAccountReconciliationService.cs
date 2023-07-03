using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Abstract
{
    public interface IAccountReconciliationService
    {
        IResult Add(AccountReconciliaton accountReconciliaton);
        IResult AddToExcel(string filePath, int companyId);
        IResult Update(AccountReconciliaton accountReconciliaton);
        IResult Delete(AccountReconciliaton accountReconciliaton);
        IDataResult<AccountReconciliaton> GetById(int id);
        IDataResult<List<AccountReconciliaton>> GetList(int companyId);
    }
}
