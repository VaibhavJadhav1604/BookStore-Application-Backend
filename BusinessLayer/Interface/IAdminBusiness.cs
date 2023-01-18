using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminBusiness
    {
        public string AdminLogin(string EmailId, string Password);
    }
}
