using ForeverPetsHome.Domain;

namespace ForeverPetsHome.Application.Security
{
    public interface IRefreshTokenService
    {
        public Task SaveRefreshToken(RefreshToken refreshToken);
        public Task<RefreshToken> GetRefreshToken(string token);
        public Task RevokeRefreshToken(RefreshToken token);
    }
}