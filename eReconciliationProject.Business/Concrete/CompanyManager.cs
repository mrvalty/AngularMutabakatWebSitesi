using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.BusinessAspect;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Business.ValidationRules.FluentValidation;
using eReconciliationProject.Core.Aspects.Autofac.Transaction;
using eReconciliationProject.Core.Aspects.Autofac.Validation;
using eReconciliationProject.Core.Aspects.Caching;
using eReconciliationProject.Core.Aspects.Performance;
using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserOperationClaimService _userOperationClaimService;

        public CompanyManager(ICompanyRepository companyRepository, IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService)
        {
            _companyRepository = companyRepository;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
        }

        //Dependency Injection
        //Kullanıcı Yetkisi
        //Transaction
        //Log
        //Validation (bunları kontrol et sonra işlemi yap)

        [CacheRemoveAspect("ICompanyService.Get")]
        [ValidationAspect(typeof(CompanyValidator))]
        public IResult Add(Company company)
        {
            _companyRepository.Add(company);
            return new SuccessResult(Messages.AddedCompany);
        }

        [CacheRemoveAspect("ICompanyService.Get")]
        [ValidationAspect(typeof(CompanyValidator))]
        [TransactionScopeAspect]
        public IResult AddCompanyAndUserCompany(CompanyDto companyDto)
        {
            Company company = new Company()
            {
                Id = companyDto.Id,
                Name = companyDto.Name,
                TaxDepartment = companyDto.TaxDepartment,
                TaxIdNumber = companyDto.TaxIdNumber,
                IdentityNumber = companyDto.IdentityNumber,
                Address = companyDto.Address,
                AddedAt = companyDto.AddedAt,
                IsActive = companyDto.IsActive
            };

            _companyRepository.Add(company);
            _companyRepository.UserCompanyAdd(companyDto.UserId, company.Id);

            var operationClaims = _operationClaimService.GetList().Data;

            foreach (var operationClaim in operationClaims)
            {
                if (operationClaim.Id != 1 && operationClaim.Id != 47 && operationClaim.Id != 48 && !operationClaim.Name.Contains("UserOperationClaim"))
                {
                    UserOperationClaim userOperationClaim = new UserOperationClaim()
                    {
                        CompanyId = company.Id,
                        AddedAt = DateTime.Now,
                        OperationClaimId = operationClaim.Id,
                        IsActive = true,
                        UserId = companyDto.UserId,
                    };
                    _userOperationClaimService.Add(userOperationClaim);

                }
            }


            return new SuccessResult(Messages.AddedCompany);

        }

        public IResult CompanyExists(Company company)
        {
            var result = _companyRepository.Get(x => x.Name == company.Name && x.TaxDepartment == company.TaxDepartment && x.TaxIdNumber == company.TaxIdNumber && x.IdentityNumber == company.IdentityNumber);
            if (result != null)
            {
                return new ErrorResult(Messages.CompanyAlreadyExists);
            }
            return new SuccessResult();
        }

        [CacheAspect(60)]
        public IDataResult<UserCompany> GetCompany(int userId)
        {
            return new SuccessDataResult<UserCompany>(_companyRepository.GetCompany(userId));
        }

        [CacheAspect(60)]
        public IDataResult<List<Company>> GetList()
        {
            return new SuccessDataResult<List<Company>>(_companyRepository.GetList(), "Listeleme İşlemi Başarılı");
        }

        [CacheAspect(60)]
        public IDataResult<Company> GetById(int id)
        {
            return new SuccessDataResult<Company>(_companyRepository.Get(x => x.Id == id));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("Company.Update,Admin")]
        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult Update(Company company)
        {
            _companyRepository.Update(company);
            return new SuccessResult(Messages.UpdateCompany);
        }

        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult UserCompanyAdd(int userId, int companyId)
        {
            _companyRepository.UserCompanyAdd(userId, companyId);
            return new SuccessResult();
        }

        public IDataResult<List<Company>> GetListByUserId(int userId)
        {
            _companyRepository.GetListByUserId(userId);
            return new SuccessDataResult<List<Company>>(_companyRepository.GetListByUserId(userId));
        }
    }
}
