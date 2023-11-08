using Castle.DynamicProxy;
using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using eReconciliationProject.Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace eReconciliationProject.Core.Aspects.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                string body = $"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}--> {_stopwatch.Elapsed.TotalSeconds}";
                SendConfirmEmail(body);
                // mail kodları

            }
            _stopwatch.Reset();
        }

        void SendConfirmEmail(string _body)
        {
            string subject = "Performans Maili";

            SendMailDto sendMailDto = new SendMailDto()
            {
                Email = "emutabakat06@zohomail.eu",
                Password = "205079g.s.*",
                Port = 587,
                SMTP = "smtp.zoho.eu",
                SSL = true,
                tomail = "emutabakat06@zohomail.eu",
                subject = subject,
                body = _body
            };

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(sendMailDto.Email);
                mail.To.Add(sendMailDto.tomail);
                mail.Subject = sendMailDto.subject;
                mail.Body = sendMailDto.body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add();

                using (SmtpClient smtp = new SmtpClient(sendMailDto.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(sendMailDto.Email, sendMailDto.Password);
                    smtp.EnableSsl = sendMailDto.SSL;
                    smtp.Port = sendMailDto.Port;
                    smtp.Send(mail);
                }
            }
        }
    }
}
