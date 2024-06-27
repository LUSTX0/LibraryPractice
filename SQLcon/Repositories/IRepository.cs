using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SQLcon.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity, int id);
        void Delete(int id);
    }
}
