using BusinessLayer.Interface;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        public IWishlistBusiness wishlistBusiness;
        public WishlistController(IWishlistBusiness wishlistBusiness)
        {
            this.wishlistBusiness = wishlistBusiness;
        }

        [Authorize]
        [HttpPost("AddToWishlist")]
        public ActionResult AddToWishlist(int BookId)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool result = wishlistBusiness.AddToWishlist(UserId, BookId);
                if (result)
                {
                    return Ok(new { success = true, message = "Book added to Wishlist" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book not Wishlist !!" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteFromWishlist")]
        public ActionResult DeleteFromWishlist(int WishlistId)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                bool result = wishlistBusiness.DeleteFromWishlist(UserId,WishlistId);
                if (result)
                {
                    return Ok(new { success = true, message = "Wishlist Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Wishlist Deletion Failed" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetWishlistDetails")]
        public ActionResult GetWishlistDetails()
        {
            try
            {
                var UserId=Convert.ToInt32(User.Claims.FirstOrDefault(e=>e.Type == "UserId").Value);
                var result = wishlistBusiness.GetWishlisDetails(UserId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Getting Wishlist Details", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Getting Wishlist Details Failed" });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
