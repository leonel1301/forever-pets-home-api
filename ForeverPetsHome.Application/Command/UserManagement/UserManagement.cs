using BCrypt.Net;
using ForeverPetsHome.Application.Command.UserManagement.Request;
using ForeverPetsHome.Application.Command.UserManagement.Response;
using ForeverPetsHome.Application.Dtos;
using ForeverPetsHome.Application.Interfaces;
using ForeverPetsHome.Application.Security;
using ForeverPetsHome.Domain;
using System;

namespace ForeverPetsHome.Application.Command.UserManagement
{
    public class UserManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        
        public UserManagement(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AccessTokenReponse> RegisterUser(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAysnc(registerDto.Email);
            if (existingUser == null)
            {
                AppUser newUser = new AppUser {
                    Email = registerDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                    Roles = registerDto.Roles,
                    Names = registerDto.Names,
                    LastName = registerDto.LastName,
                    MotherLastName = registerDto.MotherLastName
                };
                await _userRepository.AddUser(newUser);
                var token = _tokenService.GenerateUserToken(registerDto.Email, newUser.Id);
                var refreshToken = await _tokenService.GenerateRefreshToken(newUser.Id);
                AccessTokenReponse response = new AccessTokenReponse {
                    UserId = newUser.Id.ToString(),
                    Token = token,
                    RefreshToken = refreshToken.Token
                };
                return response;
            }
            else {
                throw new Exception("El usuario ya existe");
            }
        }

        public async Task<AccessTokenReponse> LoginUser(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAysnc(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new Exception("Credenciales incorrectas");
            }
            else {
                var token = _tokenService.GenerateUserToken(loginDto.Email, user.Id);
                var refreshToken = await _tokenService.GenerateRefreshToken(user.Id);
                AccessTokenReponse response = new AccessTokenReponse
                {
                    UserId = user.Id.ToString(),
                    Token = token,
                    RefreshToken = refreshToken.Token
                };
                return response;
            }
        }

        public async Task<AccessTokenReponse> RenewalToken(TokenRenewalDto tokenRenewalDto)
        {
            return await _tokenService.RenewTokens(tokenRenewalDto.RefreshToken, tokenRenewalDto.UserId);
        }

    }
}