using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public ICartBusiness cartBusiness;
        public CartController(ICartBusiness cartBusiness)
        {
            this.cartBusiness = cartBusiness;
        }

        [Authorize]
        [HttpPost("AddCart")]
        public ActionResult AddCart(int BookId, int Quantity)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool result=cartBusiness.AddCart(UserId,BookId,Quantity);
                if (result)
                {
                    return Ok(new { success = true, message = "Book added to cart" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book not added !!" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateCart")]
        public ActionResult UpdateCart(int CartId, int Quantity)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool result = cartBusiness.UpdateCart(UserId,CartId,Quantity);
                if (result)
                {
                    return Ok(new { success = true, message = "Cart Updated Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cart Updation Failed" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteCart")]
        public ActionResult DeleteCart(int CartId)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool result = cartBusiness.DeleteCart(UserId, CartId);
                if (result)
                {
                    return Ok(new { success = true, message = "Cart Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cart Deletion Failed" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetCartDetails")]
        public ActionResult GetCartDetails()
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result=cartBusiness.GetCartDetails(UserId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Getting Cart Details",Response=result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Getting Cart Details Failed" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
