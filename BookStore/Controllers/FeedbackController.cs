using BusinessLayer.Interface;
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
    public class FeedbackController : ControllerBase
    {
        private IFeedbackBusiness feedbackBusiness;
        public FeedbackController(IFeedbackBusiness feedbackBusiness)
        {
            this.feedbackBusiness = feedbackBusiness;
        }
        [Authorize]
        [HttpPost("AddFeedback")]
        public ActionResult AddFeedback(int BookId,FeedbackModel feedbackModel)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result=feedbackBusiness.AddFeedback(UserId, BookId, feedbackModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Feedback Added Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Feedback Adding Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetAllFeedback")]
        public ActionResult GetAllFeedback(int BookId)
        {
            try
            {
                var result=feedbackBusiness.GetAllFeedback(BookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting All Feedbacks", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = " Getting Feedbacks Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
