using eReconciliationProject.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.DA.Repositories.Abstract
{
    public interface IMailRepository
    {
        void SendMail(SendMailDto sendMailDto);
    }
}
