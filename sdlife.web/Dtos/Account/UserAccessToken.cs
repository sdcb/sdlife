using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Dtos.Account
{
    public class UserAccessToken
    {
        public string Token { get; set; }

        public DateTime RefreshTime { get; set; }

        public DateTime Expiration { get; set; }
    }
}
