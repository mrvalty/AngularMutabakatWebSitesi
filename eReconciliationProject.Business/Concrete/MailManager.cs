using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class MailManager : IMailService
    {
        private readonly IMailRepository _mailrepository;

        public MailManager(IMailRepository mailrepository)
        {
            _mailrepository = mailrepository;
        }

        public IResult SendMail(SendMailDto sendMailDto)
        {
            _mailrepository.SendMail(sendMailDto);
            return new SuccessResult(Messages.MailSendSuccess);
        }
    }
}
