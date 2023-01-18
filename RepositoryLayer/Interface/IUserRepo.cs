using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserModel Registration(UserModel userModel);

        public string Login(string EmailId, string Password);

        public string ForgotPassword(string EmailId);

        public bool ResetPassword(string EmailId, string Password, string ConfirmPassword);
    }
}