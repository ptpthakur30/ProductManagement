using Microsoft.AspNetCore.Identity;
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
    public class LoginRepository : ILoginRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        public LoginRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserDTO> LoginUser(Login loginDetails)
        {
            var user = await _userManager.FindByEmailAsync(loginDetails.Email);

            if (user == null)
                throw new RestException(HttpStatusCode.Unauthorized);

            //to check if email is confirmed or not
            if (!user.EmailConfirmed) throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email is not confirmed" });

            //the last paramter of CheckPasswordSignInAsync is islockout if password attemp failed
            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDetails.Password, false);

            if (result.Succeeded)
            {

                return new UserDTO(user, _jwtGenerator);
            }

            throw new RestException(HttpStatusCode.Unauthorized);
        }
    }
}
