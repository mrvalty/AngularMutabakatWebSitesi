using eReconciliationProject.Core.Entities;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Entities.Dtos
{
    public class SendMailDto :IDto
    {
        public MailParameter mailParameter { get; set; }
        public string tomail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
