using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost("Registration")]
        public ActionResult Registration(UserModel userModel)
        {
            try
            {
                var result = userBusiness.Registration(userModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successfull" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Login")]
        public ActionResult Login(string EmailId, string Password)
        {
            try
            {
                var result = userBusiness.Login(EmailId, Password);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successfull", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(string Password)
        {
            try
            {
                var result = userBusiness.ForgotPassword(Password);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Mail Sent" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Mail Sending Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(string Password, string ConfirmPassword)
        {
            var EmailId = User.FindFirst(ClaimTypes.Email).Value.ToString();
            var result = userBusiness.ResetPassword(EmailId, Password, ConfirmPassword);
            if (result)
            {
                return Ok(new { success = true, message = "Password Reset SuccessFull" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Password Resetting Failed" });
            }
        }
        [HttpGet("GetUserById")]
        public ActionResult GetUserById()
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = userBusiness.GetUserById(UserId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data Gained Successfully", Response = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Data Gaining Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
