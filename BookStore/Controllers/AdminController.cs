using BusinessLayer.Interface;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminBusiness adminBusiness;
        public AdminController(IAdminBusiness adminBusiness)
        {
            this.adminBusiness = adminBusiness;
        }

        [HttpPost("AdminLogin")]
        public ActionResult AdminLogin(string EmailId, string Password)
        {
            try
            {
                var result = adminBusiness.AdminLogin(EmailId, Password);
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
    }
}
