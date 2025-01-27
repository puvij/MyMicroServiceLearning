using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicaionUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator, RoleManager<IdentityRole> roleManager, UserManager<ApplicaionUser> userManager)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicaionUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var usertoreturn = _db.ApplicaionUsers.First(u => u.UserName == registrationRequestDto.Email);
                    UserDto userDto = new()
                    {
                        Email = usertoreturn.Email,
                        ID = usertoreturn.Id,
                        Name = usertoreturn.Name,
                        PhoneNumber = usertoreturn.PhoneNumber

                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return "Error Occuered";
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicaionUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Name.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<bool> AssignRole(string email, string rolename)
        {
            var user = _db.ApplicaionUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null )
            {
                if (!_roleManager.RoleExistsAsync(rolename).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(rolename)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user,rolename);
                return true;
            }
            return false;
        }
    }
        
}
