using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DataAccess.Interface;
using ProductManagement.Models;
using ProductManagement.Utility;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public UserController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody]Login loginDetails)
        {
            if(ModelState.IsValid)
            {
                var user = await _loginRepository.LoginUser(loginDetails);
                if (user!=null)
                {
                    return Ok(user);
                }
                else
                {
                    return Unauthorized("User not exist");
                }
            }
          
            return BadRequest();
        }
    }
}
