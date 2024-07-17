using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.BusinessAspect;
using eReconciliationProject.Business.ValidationRules.FluentValidation;
using eReconciliationProject.Core.Aspects.Autofac.Validation;
using eReconciliationProject.Core.Aspects.Caching;
using eReconciliationProject.Core.Aspects.Performance;
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

        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public void Add(User user)
        {

            _userRepository.Add(user);
        }

        [CacheAspect(60)]
        public User GetById(int id)
        {
            return _userRepository.Get(x=>x.Id == id);
        }

        [CacheAspect(60)]
        public User GetByMail(string email)
        {
            return _userRepository.Get(x => x.Email == email);
        }

        [CacheAspect(60)]
        public User GetByMailConfirmValue(string value)
        {
            return _userRepository.Get(x=>x.MailConfirmValue == value);
        }

        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            return _userRepository.GetClaims(user,companyId);
        }


        [PerformanceAspect(3)]
        //[SecuredOperation("User.Update,Admin")]
        [CacheRemoveAspect("IUserService.Get")]
        public void Update(User user)
        {
            _userRepository.Update(user);
        }
    }
}
