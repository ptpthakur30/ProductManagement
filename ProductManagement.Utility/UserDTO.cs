using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Utility
{
    public class UserDTO
    {
        public UserDTO(AppUser user, IJwtGenerator jwtGenerator)
        {
            DisplayName = user.DisplayName;
            Token = jwtGenerator.CreateToken(user);
            Username = user.UserName;

        }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        
    }
}
