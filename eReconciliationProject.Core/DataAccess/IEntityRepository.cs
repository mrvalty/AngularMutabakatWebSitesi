using eReconciliationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Core.DataAccess
{
    public interface IEntityRepository<T> where T: class,IEntity,new()
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //select * from where ıd =1 diye yazabilmek için GetList(). deyip çağırabilmek için. hem sorgum olabilir hemde boş da olabilir
        List<T> GetList(Expression<Func<T,bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter = null);
    }
}
