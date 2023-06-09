﻿using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class MailParameterManager : IMailParameterService
    {
        private readonly IMailParameterRepository _mailParameterRepository;

        public MailParameterManager(IMailParameterRepository mailParameterRepository)
        {
            _mailParameterRepository = mailParameterRepository;
        }

        public IResult Update(MailParameter mailParameter)
        {
            var result = Get(mailParameter.CompanyId);
            if (result.Data == null)
            {
                _mailParameterRepository.Add(mailParameter);
            }
            else
            {
                result.Data.SMTP=mailParameter.SMTP;
                result.Data.Port=mailParameter.Port;
                result.Data.SSL=mailParameter.SSL;
                result.Data.Email=mailParameter.Email;
                result.Data.Password=mailParameter.Password;

                _mailParameterRepository.Update(result.Data);
            }

            return new SuccessResult(Messages.MailParameterUpdate);
        }

        public IDataResult<MailParameter> Get(int companyId)
        {
            return new SuccessDataResult<MailParameter>(_mailParameterRepository.Get(x=>x.CompanyId == companyId));
        }
    }
}
