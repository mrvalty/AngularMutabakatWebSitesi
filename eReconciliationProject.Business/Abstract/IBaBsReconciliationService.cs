using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Abstract
{
    public interface IBaBsReconciliationService
    {

        IResult Add(BaBsReconciliation baBsReconciliation);
        IResult AddToExcel(string filePath, int companyId);
        IResult Update(BaBsReconciliation baBsReconciliation);
        IResult Delete(BaBsReconciliation baBsReconciliation);
        IDataResult<BaBsReconciliation> GetById(int id);
        IDataResult<BaBsReconciliation> GetByCode(string code);

        IDataResult<List<BaBsReconciliation>> GetList(int companyId);
        IDataResult<List<BaBsReconciliationDto>> GetListDto(int companyId);

        IResult SendReconciliationMail(BaBsReconciliationDto babsReconciliatonDto);

    }
}
