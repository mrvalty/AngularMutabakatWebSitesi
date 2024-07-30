using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class UserForgotPasswordManager : IUserForgotPasswordService
    {

        private readonly IUserForgotPasswordRepository _userForgotPasswordRepository;

        public UserForgotPasswordManager(IUserForgotPasswordRepository userForgotPasswordRepository)
        {
            _userForgotPasswordRepository = userForgotPasswordRepository;
        }

        public IDataResult<UserForgotPassword> CreateForgotPassword(User user)
        {
            UserForgotPassword userForgotPassword = new UserForgotPassword()
            {
                IsActive = true,
                SendDate = DateTime.Now,
                UserId = user.Id,
                Value = Guid.NewGuid().ToString()
            };
            _userForgotPasswordRepository.Add(userForgotPassword);
            return new SuccessDataResult<UserForgotPassword>(userForgotPassword);
        }

        public UserForgotPassword GetForgotPassword(string value)
        {
            return _userForgotPasswordRepository.Get(p=>p.Value ==  value); 
        }

        public IDataResult<List<UserForgotPassword>> GetListByUserId(int userId)
        {
            return new SuccessDataResult<List<UserForgotPassword>>(_userForgotPasswordRepository.GetList(p => p.UserId == userId && p.IsActive == true).ToList()); 
        }

        public void Update(UserForgotPassword userForgotPassword)
        {
            _userForgotPasswordRepository.Update(userForgotPassword);
        }
    }
}
