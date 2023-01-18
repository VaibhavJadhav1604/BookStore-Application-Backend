using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        public UserModel Registration(UserModel model)
        {
            try
            {
                return this.userRepo.Registration(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Login(string EmailId, string Password) 
        {
            try
            {
                return this.userRepo.Login(EmailId, Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ForgotPassword(string Password)
        {
            try
            {
                return this.userRepo.ForgotPassword(Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ResetPassword(string EmailId,string Password,string Confirmpassword)
        {
            try
            {
                return this.userRepo.ResetPassword(EmailId, Password, Confirmpassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
