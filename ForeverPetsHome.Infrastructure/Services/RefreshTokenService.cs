using ForeverPetsHome.Application.Security;
using ForeverPetsHome.Domain;
using ForeverPetsHome.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForeverPetsHome.Infrastructure.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ApplicationDbContext _db;
        public RefreshTokenService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<RefreshToken> GetRefreshToken(string token)
        {
            var refreshToken = await _db.RefreshTokens.Include(r => r.AppUser).FirstOrDefaultAsync(r => r.Token == token);
            return refreshToken;
        }

        public async Task RevokeRefreshToken(RefreshToken token)
        {
            token.Revoked = DateTime.UtcNow;
            _db.RefreshTokens.Update(token);
            await _db.SaveChangesAsync();
        }

        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();
        }
    }
}