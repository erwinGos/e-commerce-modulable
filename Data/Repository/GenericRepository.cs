using Database;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<T>> GetAll()
        {
            return await _table.IgnoreAutoIncludes().ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetById(object id)
        {
            T element = await _table.FindAsync(id).ConfigureAwait(false);

            if (element == null)
            {
                throw new Exception("Element non trouvé.");
            }

            return element;
        }

        public async Task<T> Insert(T element)
        {
            var elementAdded = await _table.AddAsync(element).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return elementAdded.Entity;
        }

        public async Task<T> Update(T element)
        {
            var elementUpdated = _table.Update(element);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return elementUpdated.Entity;
        }

        public async Task<T> Delete(T element)
        {
            var elementDeleted = _table.Remove(element);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return elementDeleted.Entity;
        }
    }
}
