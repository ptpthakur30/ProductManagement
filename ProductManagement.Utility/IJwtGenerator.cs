using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Utility
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
