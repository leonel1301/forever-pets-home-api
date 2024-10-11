using ForeverPetsHome.Application.Command.UserManagement.Response;
using ForeverPetsHome.Domain;

namespace ForeverPetsHome.Application.Security
{
    public interface ITokenService {
        public string GenerateUserToken(string Email, Guid Id);
        public Task<RefreshToken> GenerateRefreshToken(Guid UserId);
        public Task<AccessTokenReponse> RenewTokens(string refreshToken, Guid userId);
    }
}