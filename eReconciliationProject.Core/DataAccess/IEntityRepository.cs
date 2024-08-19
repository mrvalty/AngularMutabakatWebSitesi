using eReconciliationProject.Core.Entities;
using System.Linq.Expressions;

namespace eReconciliationProject.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //select * from where ıd =1 diye yazabilmek için GetList(). deyip çağırabilmek için. hem sorgum olabilir hemde boş da olabilir
        List<T> GetList(Expression<Func<T, bool>>? filter = null);
        T? Get(Expression<Func<T, bool>> filter);
    }
}
