using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Utility
{
    public interface IUserAccessor
    {
        string GetCurrentUsername();
    }
}
