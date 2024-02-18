﻿using Data.Repository.Contract;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ShoppingCartRepository : GenericRepository<UserCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<UserCart> Update(UserCart element)
        {
            try
            {
                // Mettre à jour l'élément dans la base de données
                var elementUpdated = _table.Update(element);
                await _db.SaveChangesAsync().ConfigureAwait(false);

                // Recharger l'entité mise à jour depuis la base de données avec les inclusions souhaitées
                var result = await _table
                    .Include(usercart => usercart.Product)
                    .ThenInclude(product => product.ProductImages).Take(1)
                    .FirstOrDefaultAsync(usercart => usercart.Id == elementUpdated.Entity.Id); // Assurez-vous que UserCart a un Id ou utilisez la clé appropriée

                if (result == null)
                {
                    throw new Exception("L'élément mis à jour n'a pas été trouvé.");
                }

                return result;
            }
            catch (Exception ex)
            {
                // Il est généralement une bonne idée de logger l'exception ici
                throw new Exception($"Une erreur est survenue lors de la mise à jour: {ex.Message}", ex);
            }
        }
    }
}
