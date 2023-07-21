using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Aspects.Autofac.Transaction;
using eReconciliationProject.Core.Aspects.Caching;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class BaBsReconciliationDetailManager : IBaBsReconciliationDetailService
    {
        private readonly IBaBsReconciliationDetailRepository _baBsReconciliationRepository;

        public BaBsReconciliationDetailManager(IBaBsReconciliationDetailRepository baBsReconciliationRepository)
        {
            _baBsReconciliationRepository = baBsReconciliationRepository;
        }
        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]

        public IResult Add(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            _baBsReconciliationRepository.Add(baBsReconciliationDetail);
            return new SuccessResult(Messages.AddedBaBsReconciliationDetail);
        }
        [TransactionScopeAspect]
        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]

        public IResult AddToExcel(string filePath, int baBsReconciliationId)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string description = reader.GetString(1);

                        if (description != "Açıklama" && description != null)
                        {
                            DateTime date = reader.GetDateTime(0);
                            double amount = reader.GetDouble(2);

                            BaBsReconciliationDetail baBsReconciliationDetail = new BaBsReconciliationDetail()
                            {
                                BaBsReconciliationId = baBsReconciliationId,
                                Date = date,
                                Description = description,
                                Amount=Convert.ToDecimal(amount)


                            };

                            _baBsReconciliationRepository.Add(baBsReconciliationDetail);
                        }
                    }
                }
                File.Delete(filePath);
            }

            return new SuccessResult(Messages.AddedBaBsReconciliation);
        }
        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]

        public IResult Delete(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            _baBsReconciliationRepository.Delete(baBsReconciliationDetail);
            return new SuccessResult(Messages.DeletedBaBsReconciliationDetail);
        }
        [CacheAspect(60)]
        public IDataResult<BaBsReconciliationDetail> GetById(int id)
        {
            return new SuccessDataResult<BaBsReconciliationDetail>(_baBsReconciliationRepository.Get(x => x.Id == id));
        }
        [CacheAspect(60)]

        public IDataResult<List<BaBsReconciliationDetail>> GetList(int baBsReconciliationId)
        {
            return new SuccessDataResult<List<BaBsReconciliationDetail>>(_baBsReconciliationRepository.GetList(x => x.BaBsReconciliationId == baBsReconciliationId));
        }
        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]

        public IResult Update(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            _baBsReconciliationRepository.Update(baBsReconciliationDetail);
            return new SuccessResult(Messages.UpdatedBaBsReconciliationDetail);
        }
    }
}
