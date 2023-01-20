using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRepo
    {
        public FeedbackModel AddFeedback(int UserId, int BookId, FeedbackModel feedbackModel);
        public List<FeedbackModel> GetAllFeedback(int BookId);
    }
}
