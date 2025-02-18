using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication4.Interface;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public class AuthService : IAuthService
    {
        
        #region Ctor
        private readonly UserManager<AppUser> userManager;
        private readonly Jwt jwt;

        public AuthService(UserManager<AppUser> userManager , IOptions<Jwt> Jwt)
        {
            this.userManager = userManager;
            this.jwt = Jwt.Value;
        }
        #endregion
         public async Task<AuthModel> RegisterAsync(RegisterModel inputDto)
         {
            if (await userManager.FindByEmailAsync(inputDto.Email) is not null)
                return new AuthModel() { Message = "Email Already Registerd" };

            if (await userManager.FindByNameAsync(inputDto  .UserName) is not null)
                return new AuthModel() { Message = "UserName Already Registerd" };

            var user = new AppUser()
            {
                UserName = inputDto.UserName,
                Email = inputDto.Email,
                FirstName = inputDto.FirstName,
                LastName = inputDto.LastName,
            };

            var res = await userManager.CreateAsync(user , inputDto.Password);

            if(!res .Succeeded) 
            {
                string erorrs = string.Empty;
                foreach (var r in res.Errors)
                {
                    erorrs += $"{r.Description}, ";
                }
                return new AuthModel() { Message = erorrs };
            }

            return new AuthModel()
            {
                Email = inputDto.Email,
                UserName = inputDto.UserName,
                IsAuth = true,
            };
         }

        public async Task<AuthModel> LoginAsync(LoginModel inputDto)
        {
            var user = await userManager.FindByEmailAsync(inputDto.Email);
            
            if(user == null || !await userManager.CheckPasswordAsync(user , inputDto.Password))
            {
                return new AuthModel() { Message = "Email or Password is incorrect" };
            }

            var JwtToken = await CreateJwtToken(user);



            var res = new AuthModel()
            {
                IsAuth = true,
                Email = inputDto.Email,
                UserName = user.UserName,
                Message = JwtToken.ToString(),
                Token = new JwtSecurityTokenHandler().WriteToken(JwtToken)
            };
            return res;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(AppUser appUser)
        {
            var userClaims = await userManager.GetClaimsAsync(appUser);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
        new Claim("uid", appUser.Id)
    }
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims, 
                expires: DateTime.Now.AddDays(jwt.DurationInDay), 
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }

    }
}
