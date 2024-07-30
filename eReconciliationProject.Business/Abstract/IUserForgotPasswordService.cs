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
    public interface IUserForgotPasswordService
    {
        IDataResult<UserForgotPassword> CreateForgotPassword(User user);
        IDataResult<List<UserForgotPassword>> GetListByUserId(int userId);
        UserForgotPassword GetForgotPassword(string value);

        void Update(UserForgotPassword userForgotPassword);
    }
}
