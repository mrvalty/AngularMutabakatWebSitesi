﻿using eReconciliationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Entities.Concrete
{
    public class TermsandCondition : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }

    }
}
