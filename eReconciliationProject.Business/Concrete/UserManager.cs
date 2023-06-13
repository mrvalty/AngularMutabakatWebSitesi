using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.ValidationRules.FluentValidation;
using eReconciliationProject.Core.Aspects.Autofac.Validation;
using eReconciliationProject.Core.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [ValidationAspect(typeof(UserValidator))]
        public void Add(User user)
        {

            _userRepository.Add(user);
        }

        public User GetById(int id)
        {
            return _userRepository.Get(x=>x.Id == id);
        }

        public User GetByMail(string email)
        {
            return _userRepository.Get(x => x.Email == email);
        }

        public User GetByMailConfirmValue(string value)
        {
            return _userRepository.Get(x=>x.MailConfirmValue == value);
        }

        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            return _userRepository.GetClaims(user,companyId);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }
    }
}
