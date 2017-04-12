using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Dtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public static explicit operator User(CreateUserDto dto)
        {
            return new User
            {
                UserName = dto.UserName, 
                Email = dto.Email, 
            };
        }
    }
}
