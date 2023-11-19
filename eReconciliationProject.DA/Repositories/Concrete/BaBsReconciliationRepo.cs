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
    public class BaBsReconciliationRepo : EfEntityRepositoryBase<BaBsReconciliation, ProjectContext>, IBaBsReconciliationRepository
    {
        public List<BaBsReconciliationDto> GetAllDto(int companyId)
        {
            using (var context = new ProjectContext())
            {
                var result = from account in context.BaBsReconciliations.Where(x=>x.CompanyId == companyId)
                             join company in context.Companies on account.CompanyId equals company.Id
                             join currency in context.CurrencyAccounts on account.CurrencyAccountId equals currency.Id
                             select new BaBsReconciliationDto
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
                                 Total = account.Total,
                                 EmailReadDate = account.EmailReadDate,
                                 Guid = account.Guid,
                                 Id = account.Id,
                                 IsEmailRead = account.IsEmailRead,
                                 IsResultSucceed = account.IsResultSucceed,
                                 IsSendEmail = account.IsSendEmail,
                                 ResultDate = account.ResultDate,
                                 ResultNote = account.ResultNote,
                                 SendEmailDate = account.SendEmailDate,
                                 CurrencyCode = "TL",
                                 AccountEmail = currency.Email,
                                 Mounth = account.Mounth,
                                 Type = account.Type,
                                 Year = account.Year,
                                 Quantity = account.Quantity
                             };
                return result.ToList();
            }
        }
    }
}
