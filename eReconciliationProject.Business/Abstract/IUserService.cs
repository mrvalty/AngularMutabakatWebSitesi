using eReconciliationProject.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user , int companyId);
        void Add(User user);
        User GetByMail(string email);
    }
}
