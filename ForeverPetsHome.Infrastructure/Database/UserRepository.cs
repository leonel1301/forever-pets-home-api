using System.IO.Compression;
using ForeverPetsHome.Application.Interfaces;
using ForeverPetsHome.Domain;
using ForeverPetsHome.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForeverPetsHome.Infrastructure.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddUser(AppUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<AppUser> GetUserByEmailAysnc(string Email)
        {
            AppUser user = await _context.Users.SingleOrDefaultAsync(u => u.Email == Email);
            return user;
        }

        public async Task<AppUser> GetUserByIdAsync(Guid Id, EnumRoles? roles)
        {
            AppUser user = await _context.Users.SingleOrDefaultAsync(u => u.Id == Id && u.Roles == roles);
            return user;
        }
        
    }
}