using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;

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
        IDataResult<List<BaBsReconciliation>> GetByIdCurrencyAccount(int currencyAccountId);
        IDataResult<List<BaBsReconciliationDto>> GetListDto(int companyId);

        IResult SendReconciliationMail(BaBsReconciliationDto babsReconciliatonDto);

    }
}
