﻿using eReconciliationProject.Core.DataAccess;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.DA.Repositories.Abstract
{
    public interface IBaBsReconciliationRepository : IEntityRepository<BaBsReconciliation>
    {
        List<BaBsReconciliationDto> GetAllDto(int companyId);
    }
}
