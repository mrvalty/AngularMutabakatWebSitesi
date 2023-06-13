using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Abstract
{
    public interface ICompanyService
    {
        //CRUD
        IResult Add(Company company);
        IDataResult<List<Company>> GetList();
        IDataResult<UserCompany> GetCompany(int userId);
        IResult CompanyExists(Company company);
        IResult UserCompanyAdd(int userId, int companyId);
    }
}
