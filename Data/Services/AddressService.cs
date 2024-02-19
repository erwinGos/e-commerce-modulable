using AutoMapper;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<List<Address>> GetAllAddresses(int UserId, bool userRole)
        {
            try
            {
                List<Address> addresses = await _addressRepository.FindBy(address => address.UserId == UserId || userRole);
                if(addresses.Count == 0)
                {
                    throw new Exception("Vous n'avez pas enregistré d'adresse.");
                }
                return addresses;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Address> GetSingleAddress(int AddressId, int UserId, bool userRole)
        {
            try
            {
                Address address = await _addressRepository.FindSingleBy(address => address.Id == AddressId && address.UserId == UserId || userRole) ?? throw new Exception("Cette adresse n'existe pas.");
                return address;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Address> CreateAddress(Address createAddress)
        {
            try
            {
                return await _addressRepository.Insert(createAddress);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Address> UpdateAddress(Address updateAddress)
        {
            try
            {
                Address address = await _addressRepository.FindSingleBy(address => address.Id == updateAddress.Id && address.UserId == updateAddress.UserId) ?? throw new Exception("Cette adresse n'existe pas.");
                return await _addressRepository.Update(updateAddress);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Address> DeleteAddress(int AddressId, int userId, bool userRole)
        {
            try
            {
                Address address = await _addressRepository.FindSingleBy(address => address.Id == AddressId && (address.UserId == userId || userRole)) ?? throw new Exception("Cette adresse n'existe pas.");
                return await _addressRepository.Delete(address);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
