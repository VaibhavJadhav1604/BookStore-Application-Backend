using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness orderBusiness;
        public OrderController(IOrderBusiness orderBusiness)
        {
            this.orderBusiness = orderBusiness;
        }

        [Authorize]
        [HttpPost("AddOrder")]
        public ActionResult AddOrder(OrderModel orderModel)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                var result = orderBusiness.AddOrder(UserId, orderModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Order Placed Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Order Placing Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllOrders")]
        public ActionResult GetAllOrders()
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
                var result = orderBusiness.GetAllOrders(UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting All Orders", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Getting All Orders Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
