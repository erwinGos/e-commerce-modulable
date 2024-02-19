using Data.DTO.User;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Contract
{
    public interface IAddressService
    {
        public Task<List<Address>> GetAllAddresses(int UserId, bool userRole);

        public Task<Address> GetSingleAddress(int AddressId, int UserId, bool userRole);

        public Task<Address> CreateAddress(Address Address);

        public Task<Address> UpdateAddress(Address Address);

        public Task<Address> DeleteAddress(int AddressId, int userId, bool userRole);
    }
}
