using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class FeedbackModel
    {
        public int FeedbackId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public UserModel UserModel { get; set; }
    }
}
