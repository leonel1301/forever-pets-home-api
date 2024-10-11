using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ForeverPetsHome.Application.Command.UserManagement.Response;
using ForeverPetsHome.Application.Security;
using ForeverPetsHome.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ForeverPetsHome.Infrastructure
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly IRefreshTokenService _refreshTokenService;
        public TokenService(IConfiguration config, IRefreshTokenService refreshTokenService)
        {
            _config = config;
            _refreshTokenService = refreshTokenService;
        }

        public string GenerateUserToken(string Email, Guid Id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Email, Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<RefreshToken> GenerateRefreshToken(Guid UserId)
        {
            var randomNumber = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                UserId = UserId,
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedOn = DateTime.UtcNow
            };
            await _refreshTokenService.SaveRefreshToken(refreshToken);
            return refreshToken;
        }

        public async Task<AccessTokenReponse> RenewTokens(string refreshToken, Guid userId)
        {
            var storedToken = await _refreshTokenService.GetRefreshToken(refreshToken);
            if (storedToken == null || storedToken.UserId != userId)
            {
                throw new Exception("Token invalido");
            }
            if (!storedToken.IsActive || storedToken.IsExpired)
            {
                throw new Exception("Token expirado o revocado");
            }
            var newAccessToken = GenerateUserToken(storedToken.AppUser.Email, storedToken.AppUser.Id);
            var newRefreshToken = await GenerateRefreshToken(storedToken.UserId);

            storedToken.Revoked = DateTime.UtcNow;
            await _refreshTokenService.RevokeRefreshToken(storedToken);

            return new AccessTokenReponse
            {
                UserId = storedToken.AppUser.Id.ToString(),
                Token = newAccessToken,
                RefreshToken = newRefreshToken.Token,
            };
        }
    }
}