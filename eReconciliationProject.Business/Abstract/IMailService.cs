using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Abstract
{
    public interface IMailService
    {
        IResult SendMail(SendMailDto sendMailDto);
    }
}
