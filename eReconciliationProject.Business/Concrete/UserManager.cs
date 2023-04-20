using eReconciliationProject.Business.Abstract;
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

        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userRepository.Get(x => x.Email == email);
        }

        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            return _userRepository.GetClaims(user,companyId);
        }
    }
}
