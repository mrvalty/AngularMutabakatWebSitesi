﻿using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Entities.Dtos
{
    public class UserCompanyDto:User ,IDto
    {
        public int CompanyId { get; set; }
    }
}
