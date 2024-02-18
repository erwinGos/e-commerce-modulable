using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<List<T>> FindBy(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);

        public Task<T> FindSingleBy(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);

        public Task<List<T>> GetAll();

        public Task<T> GetById(int id);

        public Task<T> Insert(T element);

        public Task<T> Update(T element, int? Id = 0);

        public Task<T> Delete(T element);
    }
}
