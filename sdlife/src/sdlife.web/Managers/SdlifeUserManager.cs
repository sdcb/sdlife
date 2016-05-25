using Microsoft.AspNetCore.Identity;
using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace sdlife.web.Managers
{
    public class SdlifeUserManager : UserManager<User>
    {
        public SdlifeUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
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
    }
}
