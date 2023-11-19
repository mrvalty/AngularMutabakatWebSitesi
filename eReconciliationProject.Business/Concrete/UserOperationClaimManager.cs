using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.BusinessAspect;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimManager(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }
        [SecuredOperation("UserOperationClaim.Add,Admin")]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimRepository.Add(userOperationClaim);
            return new SuccessResult(Messages.AddedUserOperationClaim);
        }
        [SecuredOperation("UserOperationClaim.Delete,Admin")]
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimRepository.Delete(userOperationClaim);
            return new SuccessResult(Messages.DeletedUserOperationClaim);
        }
        [SecuredOperation("UserOperationClaim.Get,Admin")]
        public IDataResult<UserOperationClaim> GetById(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimRepository.Get(x => x.Id == id));
        }
        [SecuredOperation("UserOperationClaim.GetList,Admin")]
        public IDataResult<List<UserOperationClaim>> GetList(int userId, int companyId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimRepository.GetList(x => x.UserId == userId && x.CompanyId == companyId));
        }
        [SecuredOperation("UserOperationClaim.Update,Admin")]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimRepository.Update(userOperationClaim);
            return new SuccessResult(Messages.UpdatedUserOperationClaim);
        }
    }
}
