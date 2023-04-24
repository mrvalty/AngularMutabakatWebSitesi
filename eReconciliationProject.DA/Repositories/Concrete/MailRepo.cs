using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.DA.Repositories.Concrete
{
    public class MailRepo : IMailRepository
    {
        public void SendMail(SendMailDto sendMailDto)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(sendMailDto.mailParameter.Email);
                mail.To.Add(sendMailDto.tomail);
                mail.Subject = sendMailDto.subject;
                mail.Body = sendMailDto.body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add();

                using(SmtpClient smtp = new SmtpClient(sendMailDto.mailParameter.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials= new NetworkCredential(sendMailDto.mailParameter.Email,sendMailDto.mailParameter.Password);
                    smtp.EnableSsl = sendMailDto.mailParameter.SSL;
                    smtp.Port = sendMailDto.mailParameter.Port;
                    smtp.Send(mail);
                }
            }
        }
    }
}
