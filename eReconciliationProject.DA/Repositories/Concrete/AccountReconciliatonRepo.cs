using eReconciliationProject.Core.DataAccess.EntityFramework;
using eReconciliationProject.DA.Context;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.DA.Repositories.Concrete
{
    public class AccountReconciliatonRepo : EfEntityRepositoryBase<AccountReconciliaton, ProjectContext>, IAccountReconciliatonRepository
    {
        public List<AccountReconciliationDto> GetAllDto(int companyId)
        {
            using (var context = new ProjectContext())
            {
                var result = from account in context.AccountReconciliatons.Where(x=>x.CompanyId==companyId)
                             join company in context.Companies on account.CompanyId equals company.Id
                             join currency in context.CurrencyAccounts on account.CurrencyAccountId equals currency.Id
                             join curr in context.Currencies on account.CurrencyId equals curr.Id
                             select new AccountReconciliationDto
                             {
                                 CompanyId = companyId,
                                 CurrencyAccountId = currency.Id,
                                 AccountIdentityNumber = currency.IdentityNumber,
                                 AccountName = currency.Name,
                                 AccountTaxDepartment = currency.TaxDepartment,
                                 AccountTaxIdNumber = currency.TaxIdNumber,
                                 CompanyIdentityNumber = company.IdentityNumber,
                                 CompanyName = company.Name,
                                 CompanyTaxDepartment = company.TaxDepartment,
                                 CompanyTaxIdNumber = company.TaxIdNumber,
                                 CurrencyCredit = account.CurrencyCredit,
                                 CurrencyDebit = account.CurrencyDebit,
                                 CurrencyId = account.CurrencyId,
                                 EmailReadDate = account.EmailReadDate,
                                 EndingDate = account.EndingDate,
                                 Guid = account.Guid,
                                 Id = account.Id,
                                 IsEmailRead = account.IsEmailRead,
                                 IsResultSucceed = account.IsResultSucceed,
                                 IsSendEmail = account.IsSendEmail,
                                 ResultDate = account.ResultDate,
                                 ResultNote = account.ResultNote,
                                 SendEmailDate = account.SendEmailDate,
                                 StartingDate = account.StartingDate,
                                 CurrencyCode=curr.Code,
                                 AccountEmail= currency.Email
                             };
                return result.ToList();
            }
        }
    }
}
