using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductManagement.DataAccess.Interface;
using ProductManagement.Models;
using ProductManagement.Utility;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DataAccess.User
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        public RegisterRepository(DataContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> RegisterUser(Register registerDetails)
        {
            if (await _context.Users.AnyAsync(x => x.Email == registerDetails.Email))
                throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });

            if (await _context.Users.AnyAsync(x => x.UserName == registerDetails.Username))
                throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });

            var user = new AppUser
            {
                DisplayName = registerDetails.DisplayName,
                Email = registerDetails.Email,
                UserName = registerDetails.Username
            };

            var result = await _userManager.CreateAsync(user, registerDetails.Password);

            if (!result.Succeeded) throw new Exception("Problem creating user");

            return true;

        }

    }
}
