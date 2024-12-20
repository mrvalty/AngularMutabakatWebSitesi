﻿using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.Business.Abstract
{
    public interface ICompanyService
    {
        //CRUD
        IResult Add(Company company);
        IResult Update(Company company);
        IDataResult<Company> GetById(int id);
        IResult AddCompanyAndUserCompany(CompanyDto companyDto);
        IDataResult<List<Company>> GetList();
        IDataResult<UserCompany> GetCompany(int userId);
        IResult CompanyExists(Company company);
        IResult UserCompanyAdd(int userId, int companyId);
        IDataResult<List<Company>> GetListByUserId(int userId);

    }
}
