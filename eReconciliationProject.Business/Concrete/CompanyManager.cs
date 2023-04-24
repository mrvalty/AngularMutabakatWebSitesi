using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyManager(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        //Dependency Injection
        //Kullanıcı Yetkisi
        //Transaction
        //Log
        //Validation (bunları kontrol et sonra işlemi yap)

        public IResult Add(Company company)
        {
            if (company.Name.Length > 10)
            {
                _companyRepository.Add(company);
                return new SuccessResult(Messages.AddedCompany);
            }
            return new ErrorResult("Şirket adı en az 10 karakter olmalıdır");

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

        public IDataResult<List<Company>> GetList()
        {
            return new SuccessDataResult<List<Company>>(_companyRepository.GetList(), "Listeleme İşlemi Başarılı");
        }

        public IResult UserCompanyAdd(int userId, int companyId)
        {
            _companyRepository.UserCompanyAdd(userId, companyId);
            return new SuccessResult();
        }
    }
}
