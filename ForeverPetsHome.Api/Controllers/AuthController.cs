
using ForeverPetsHome.Application.Command.UserManagement;
using ForeverPetsHome.Application.Command.UserManagement.Request;
using ForeverPetsHome.Application.Command.UserManagement.Response;
using ForeverPetsHome.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ForeverPetsHome.Api.MapControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManagement _userManagement;
        public AuthController(UserManagement userManagement)
        {
            _userManagement = userManagement;
        }
        [HttpPost("register")]
        public async Task<AccessTokenReponse> Register(RegisterDto registerDto)
        {
            AccessTokenReponse tokenResponse = await _userManagement.RegisterUser(registerDto);
            return tokenResponse;
        }

        [HttpPost("login")]
        public async Task<AccessTokenReponse> Login(LoginDto loginDto)
        {

            AccessTokenReponse tokenResponse = await _userManagement.LoginUser(loginDto);    
            return tokenResponse;
        }

        [HttpPost("renewal-token")]
        public async Task<AccessTokenReponse> RenewalToken(TokenRenewalDto renewalDto)
        {
            AccessTokenReponse tokenResponse = await _userManagement.RenewalToken(renewalDto);
            return tokenResponse;
        }
    }
}