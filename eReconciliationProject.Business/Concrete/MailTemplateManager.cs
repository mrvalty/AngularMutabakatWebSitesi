using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Aspects.Caching;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class MailTemplateManager : IMailTemplateService
    {
        private readonly IMailTemplateRepository _mailTemplateRepository;

        public MailTemplateManager(IMailTemplateRepository mailTemplateRepository)
        {
            _mailTemplateRepository = mailTemplateRepository;
        }

        [CacheRemoveAspect("IMailTemplateService.Get")]
        public IResult Add(MailTemplate mailTemplate)
        {
            _mailTemplateRepository.Add(mailTemplate);
            return new SuccessResult(Messages.MailTemplateAdded);
        }

        [CacheRemoveAspect("IMailTemplateService.Get")]
        public IResult Delete(MailTemplate mailTemplate)
        {
            _mailTemplateRepository.Delete(mailTemplate);
            return new SuccessResult(Messages.MailTemplateDeleted);
        }

        [CacheAspect(60)]
        public IDataResult<List<MailTemplate>> GetAll(int companyId)
        {
            return new  SuccessDataResult<List<MailTemplate>>(_mailTemplateRepository.GetList(x=>x.CompanyId == companyId));
        }

        [CacheAspect(60)]
        public IDataResult<MailTemplate> Get(int id)
        {
            return new SuccessDataResult<MailTemplate>(_mailTemplateRepository.Get(x => x.Id == id));
        }

        [CacheRemoveAspect("IMailTemplateService.Get")]
        public IResult Update(MailTemplate mailTemplate)
        {
            _mailTemplateRepository.Update(mailTemplate);
            return new SuccessResult(Messages.MailTemplateUpdated);
        }

        [CacheAspect(60)]
        public IDataResult<MailTemplate> GetByTemplateName(string name, int CompanyId)
        {
            return new SuccessDataResult<MailTemplate>(_mailTemplateRepository.Get(x => x.Type == name && x.CompanyId==CompanyId));
        }
    }
}
