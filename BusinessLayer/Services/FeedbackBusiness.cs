using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedbackBusiness:IFeedbackBusiness
    {
        public IFeedbackRepo feedbackRepo;
        public FeedbackBusiness(IFeedbackRepo feedbackRepo)
        {
            this.feedbackRepo = feedbackRepo;
        }
        public FeedbackModel AddFeedback(int UserId, int BookId, FeedbackModel feedbackModel)
        {
            try
            {
                return feedbackRepo.AddFeedback(UserId, BookId, feedbackModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<FeedbackModel> GetAllFeedback(int BookId)
        {
            try
            {
                return feedbackRepo.GetAllFeedback(BookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
