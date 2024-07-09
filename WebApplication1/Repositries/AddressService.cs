using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    public interface IAddressService
    {
        Task AddAddressAsync(Address address);
        Task UpsertAddressAsync(Address address);
        Task<Address> GetAddressByUserId(Guid userId);

        Task<UserManagement> UpdateUser(UpdateUserDto user);
    }

    public class AddressService : IAddressService
    {
        private readonly ScrapMangementDbContext _context;

        public AddressService(ScrapMangementDbContext context)
        {
            _context = context;
        }

        public async Task AddAddressAsync(Address address)
        {
            _context.Address.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task<Address> GetAddressByUserIdAsync(Guid userId)
        {
            return await _context.Address.Where(item => item.UserId == userId).FirstOrDefaultAsync();
        }
        public async Task UpsertAddressAsync(Address address)
        {
            var existingAddress = await GetAddressByUserIdAsync(address.UserId);

            if (existingAddress != null)
            {
                existingAddress.StreetName = address.StreetName;
                existingAddress.City = address.City;
                existingAddress.PostalCode = address.PostalCode;
                existingAddress.State = address.State;
                existingAddress.Number = address.Number;
                existingAddress.FloorUnit = address.FloorUnit;

                _context.Address.Update(existingAddress);
            }
            else
            {
                _context.Address.Add(address);
            }

            await _context.SaveChangesAsync();
        }


        public async Task<Address> GetAddressByUserId(Guid userId)
        {
            var existingAddress = await GetAddressByUserIdAsync(userId);

            if (existingAddress != null)
            {
                return existingAddress;
            }
            else
            {
                return null;
               
            }

           
        }

        public async Task<UserManagement> UpdateUser(UpdateUserDto newUser)
        {
            var existingUser = await _context.UserManagements.Where(user=> user.UserId == newUser.UserId).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                // Update the existing user with new values
                existingUser.FirstName = newUser.FirstName;
                existingUser.LastName = newUser.LastName;
                existingUser.Email = newUser.Email;
                existingUser.PhoneNumber = newUser.PhoneNumber;
                existingUser.RoleId = newUser.RoleId;
                existingUser.CategoryID = newUser.CategoryID;
                existingUser.Description = newUser.Description;
            
                // Save the changes to the database
                await _context.SaveChangesAsync();

                return existingUser;
            }
            return null;

        }


    }
}
