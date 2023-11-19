using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.BusinessAspect;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Aspects.Autofac.Transaction;
using eReconciliationProject.Core.Aspects.Caching;
using eReconciliationProject.Core.Aspects.Performance;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class BaBsReconciliationManager : IBaBsReconciliationService
    {
        private readonly IBaBsReconciliationRepository _baBsReconciliationRepository;
        private readonly ICurrencyAccountService _currencyAccountService;
        private readonly IMailService _mailService;
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IMailParameterService _mailParameterService;


        public BaBsReconciliationManager(IBaBsReconciliationRepository baBsReconciliationRepository, ICurrencyAccountService currencyAccountService, IMailService mailService, IMailTemplateService mailTemplateService, IMailParameterService mailParameterService)
        {
            _baBsReconciliationRepository = baBsReconciliationRepository;
            _currencyAccountService = currencyAccountService;
            _mailService = mailService;
            _mailTemplateService = mailTemplateService;
            _mailParameterService = mailParameterService;
        }


        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Add,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Add(BaBsReconciliation baBsReconciliation)
        {
            string guid = Guid.NewGuid().ToString();
            baBsReconciliation.Guid = guid;
            _baBsReconciliationRepository.Add(baBsReconciliation);
            return new SuccessResult(Messages.AddedBaBsReconciliation);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Delete,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Delete(BaBsReconciliation baBsReconciliation)
        {
            _baBsReconciliationRepository.Delete(baBsReconciliation);
            return new SuccessResult(Messages.DeletedBaBsReconciliation);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<BaBsReconciliation> GetById(int id)
        {
            return new SuccessDataResult<BaBsReconciliation>(_baBsReconciliationRepository.Get(x => x.Id == id));
        }


        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<BaBsReconciliation>> GetList(int companyId)
        {
            return new SuccessDataResult<List<BaBsReconciliation>>(_baBsReconciliationRepository.GetList(x => x.CompanyId == companyId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Update,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Update(BaBsReconciliation baBsReconciliation)
        {
            _baBsReconciliationRepository.Update(baBsReconciliation);
            return new SuccessResult(Messages.UpdatedBaBsReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Add,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath, int companyId)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);

                        if (code != "Cari Kodu" && code != null)
                        {
                            string type = reader.GetString(1);
                            double mounth = reader.GetDouble(2);
                            double year = reader.GetDouble(3);
                            double quantity = reader.GetDouble(4);
                            double total = reader.GetDouble(5);

                            int currencyAccountId = _currencyAccountService.GetByCode(code, companyId).Data.Id;
                            string guid = Guid.NewGuid().ToString();
                            BaBsReconciliation baBsReconciliation = new BaBsReconciliation()
                            {
                                CompanyId = companyId,
                                CurrencyAccountId = currencyAccountId,
                                Type = type,
                                Mounth = Convert.ToInt16(mounth),
                                Year = Convert.ToInt16(year),
                                Quantity = Convert.ToInt16(quantity),
                                Total = Convert.ToDecimal(quantity),
                                Guid = guid

                            };

                            _baBsReconciliationRepository.Add(baBsReconciliation);
                        }
                    }
                }
                File.Delete(filePath);
            }

            return new SuccessResult(Messages.AddedBaBsReconciliation);
        }

        public IDataResult<BaBsReconciliation> GetByCode(string code)
        {
            return new SuccessDataResult<BaBsReconciliation>(_baBsReconciliationRepository.Get(x => x.Guid == code));

        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.SendMail,Admin")]
        public IResult SendReconciliationMail(BaBsReconciliationDto babsReconciliatonDto)
        {
            string subject = "Mutabakat Maili";
            string body = $"Gönderici Şirket  : {babsReconciliatonDto.CompanyName} <br/>" +
                          $"Gönderici Vergi Dairesi : {babsReconciliatonDto.CompanyTaxDepartment} <br/>" +
                          $"Gönderici Vergi Numarası : {babsReconciliatonDto.CompanyIdentityNumber} <br/> <hr>" +
                          $"Alıcı Şirket : {babsReconciliatonDto.AccountName} <br/>" +
                          $"Alıcı Vergi Dairesi : {babsReconciliatonDto.AccountTaxDepartment} <br/>" +
                          $"Alıcı Vergi Numarası : {babsReconciliatonDto.AccountTaxIdNumber} - {babsReconciliatonDto.AccountIdentityNumber}  <br/> <hr>" +
                          $"Ay / Yıl : {babsReconciliatonDto.Mounth} / {babsReconciliatonDto.Year} <br/>" +
                          $"Adet : {babsReconciliatonDto.Quantity} <br/>" +
                          $"Tutar : {babsReconciliatonDto.Total} {babsReconciliatonDto.CurrencyCode} <br/>";
            string link = "https://localhost:7256/api/BaBsReconciliations/GetByCode?code=" + babsReconciliatonDto.Guid;
            string linkDescription = "Mutabakatı Cevaplamak için Tıklayın";

            var mailTemplate = _mailTemplateService.GetByTemplateName("Kayıt", 4);
            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", subject);
            templateBody = templateBody.Replace("{{message}}", body);
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", linkDescription);


            var mailparam = _mailParameterService.Get(4);
            Entities.Dtos.SendMailDto sendMailDto = new Entities.Dtos.SendMailDto()
            {
                mailParameter = mailparam.Data,
                tomail = babsReconciliatonDto.AccountEmail,
                subject = subject,
                body = templateBody
            };
            _mailService.SendMail(sendMailDto);

            return new SuccessResult(Messages.MailSendSuccess);
        }
        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<BaBsReconciliationDto>> GetListDto(int companyId)
        {
            return new SuccessDataResult<List<BaBsReconciliationDto>>(_baBsReconciliationRepository.GetAllDto(companyId));
        }
    }
}
