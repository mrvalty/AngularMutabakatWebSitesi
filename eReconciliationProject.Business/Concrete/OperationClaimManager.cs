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
    public class OperationClaimManager : IOperationClaimService
    {

        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimManager(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        [SecuredOperation("Admin")]
        public IResult Add(OperationClaim operationClaim)
        {
            _operationClaimRepository.Add(operationClaim);
            return new SuccessResult(Messages.AddedOperationClaim);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimRepository.Delete(operationClaim);
            return new SuccessResult(Messages.DeletedOperationClaim);
        }

        [SecuredOperation("Admin")]
        public IDataResult<OperationClaim> GetById(int id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimRepository.Get(i => i.Id == id));
        }

        [SecuredOperation("OperationClaim.GetList,Admin")]
        public IDataResult<List<OperationClaim>> GetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimRepository.GetList());

        }

        [SecuredOperation("Admin")]
        public IResult Update(OperationClaim operationClaim)
        {
            _operationClaimRepository.Update(operationClaim);
            return new SuccessResult(Messages.UpdatedOperationClaim);
        }
    }
}
