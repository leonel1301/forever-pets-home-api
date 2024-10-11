using ForeverPetsHome.Domain;
using MediatR;

namespace ForeverPetsHome.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(AppUser user);
        Task<AppUser> GetUserByEmailAysnc(string Email);
        Task<AppUser> GetUserByIdAsync(Guid Id, EnumRoles? roles);
    }
}