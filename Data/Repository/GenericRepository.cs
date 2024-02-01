using Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repository
{
    public abstract class GenericRepository<T> where T : class
    {
        protected readonly DatabaseContext _db;

        protected readonly DbSet<T> _table;

        protected GenericRepository(DatabaseContext db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public async Task<List<T>> FindBy(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _table;

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return query.Where(expression).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<T> FindSingleBy(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                return _table.Where(expression).SingleOrDefault();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                return await _table.IgnoreAutoIncludes().ToListAsync().ConfigureAwait(false);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                T element = await _table.FindAsync(id).ConfigureAwait(false);

                if (element == null)
                {
                    throw new Exception("Element non trouvé.");
                }

                return element;
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> Insert(T element)
        {
            try
            {
                var elementAdded = await _table.AddAsync(element).ConfigureAwait(false);
                await _db.SaveChangesAsync().ConfigureAwait(false);

                return elementAdded.Entity;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> Update(T element)
        {
            try
            {
                var elementUpdated = _table.Update(element);
                await _db.SaveChangesAsync().ConfigureAwait(false);

                return elementUpdated.Entity;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> Delete(T element)
        {
            try
            {
                var elementDeleted = _table.Remove(element);
                await _db.SaveChangesAsync().ConfigureAwait(false);

                return elementDeleted.Entity;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
