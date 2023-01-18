using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAdminRepo
    {
        public string AdminLogin(string EmailId, string Password);
    }
}
