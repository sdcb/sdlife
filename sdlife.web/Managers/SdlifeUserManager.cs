using Microsoft.AspNetCore.Identity;
using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using sdlife.web.Dtos.Account;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace sdlife.web.Managers
{
    public class SdlifeUserManager : UserManager<User>
    {
        private readonly IConfigurationRoot _config;

        public SdlifeUserManager(
            IUserStore<User> store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<User> passwordHasher, 
            IEnumerable<IUserValidator<User>> userValidators, 
            IEnumerable<IPasswordValidator<User>> passwordValidators, 
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<User>> logger,
            IConfigurationRoot config) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _config = config;
        }

        public async Task<User> FindByNameOrEmailAsync(string userNameOrEmail)
        {
            var findByNameUser = await FindByNameAsync(userNameOrEmail).ConfigureAwait(false);
            if (findByNameUser != null)
            {
                return findByNameUser;
            }

            var findByEmailUser = await FindByEmailAsync(userNameOrEmail).ConfigureAwait(false);
            if (findByEmailUser != null)
            {
                return findByEmailUser;
            }

            return null;
        }

        public static TimeSpan RefreshPeriod = TimeSpan.FromMinutes(15);

        public TimeSpan GetExpirationPeriod(bool rememberMe)
        {
            return rememberMe ?
                TimeSpan.FromDays(7) :
                TimeSpan.FromMinutes(30);
        }

        public async Task<UserAccessToken> CreateUserAccessToken(User user, bool rememberMe)
        {
            var userClaims = await GetClaimsAsync(user);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,
                    user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Convert.ToBase64String(Guid.NewGuid().ToByteArray())),
                new Claim(JwtRegisteredClaimNames.Email,
                    user.Email),
                new Claim("RememberMe", 
                    rememberMe.ToString()), 
            }.Concat(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expirationPeriod = GetExpirationPeriod(rememberMe);

            var token = new JwtSecurityToken(
                issuer: _config["Tokens:Issuer"],
                audience: _config["Tokens:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(expirationPeriod),
                signingCredentials: cred);

            return new UserAccessToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token), 
                Expiration = token.ValidTo, 
                RefreshTime = DateTime.UtcNow.Add(RefreshPeriod)
            };
        }

        
    }
}
