using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBusiness:IAdminBusiness
    {
        private IAdminRepo adminRepo;
        public AdminBusiness(IAdminRepo adminRepo)
        {
            this.adminRepo = adminRepo;
        }
        public string AdminLogin(string EmailId, string Password)
        {
            try
            {
                return this.adminRepo.AdminLogin(EmailId, Password);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
