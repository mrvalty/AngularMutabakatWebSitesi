﻿using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Constans;
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

        public IResult Add(MailTemplate mailTemplate)
        {
            _mailTemplateRepository.Add(mailTemplate);
            return new SuccessResult(Messages.MailTemplateAdded);
        }

        public IResult Delete(MailTemplate mailTemplate)
        {
            _mailTemplateRepository.Delete(mailTemplate);
            return new SuccessResult(Messages.MailTemplateDeleted);
        }

        public IDataResult<List<MailTemplate>> GetAll(int companyId)
        {
            return new  SuccessDataResult<List<MailTemplate>>(_mailTemplateRepository.GetList(x=>x.CompanyId == companyId));
        }

        public IDataResult<MailTemplate> Get(int id)
        {
            return new SuccessDataResult<MailTemplate>(_mailTemplateRepository.Get(x => x.Id == id));
        }

        public IResult Update(MailTemplate mailTemplate)
        {
            _mailTemplateRepository.Update(mailTemplate);
            return new SuccessResult(Messages.MailTemplateUpdated);
        }

        public IDataResult<MailTemplate> GetByTemplateName(string name, int CompanyId)
        {
            return new SuccessDataResult<MailTemplate>(_mailTemplateRepository.Get(x => x.Type == name && x.CompanyId==CompanyId));
        }
    }
}
