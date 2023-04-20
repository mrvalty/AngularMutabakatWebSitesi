using eReconciliationProject.Business.Abstract;
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
    }
}
