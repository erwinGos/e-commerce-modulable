using Data.Repository.Contract;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Address> Update(Address address)
        {
            try
            {
                Address checkIfExists = await _table.FindAsync(address.Id).ConfigureAwait(false) ?? throw new Exception("Cette adresse n'existe pas.");
                _db.Entry(checkIfExists).State = EntityState.Detached;

                var elementUpdated = _table.Update(address);
                await _db.SaveChangesAsync().ConfigureAwait(false);

                if (elementUpdated == null)
                {
                    throw new Exception("L'adresse n'a pas pu être mise à jour.");
                }

                return elementUpdated.Entity;
            }
            catch (Exception ex)
            {
                // Il est généralement une bonne idée de logger l'exception ici
                throw new Exception($"Une erreur est survenue lors de la mise à jour: {ex.Message}", ex);
            }
        }
    }
}
