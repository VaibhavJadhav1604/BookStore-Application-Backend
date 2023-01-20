using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedbackRepo:IFeedbackRepo
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public FeedbackRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public FeedbackModel AddFeedback(int UserId, int BookId, FeedbackModel feedbackModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Insert into Feedback Values('{feedbackModel.Comment}',{feedbackModel.Rating},{BookId},{UserId})";
                SqlCommand sqlCommand=new SqlCommand(query,sqlConnection);
                int result=sqlCommand.ExecuteNonQuery();
                if(result != 0)
                {
                    return feedbackModel;
                }
                else
                {
                    return null;
                }
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
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetFeedbackByBookId", sqlConnection)
                {
                    CommandType=CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@BookId", BookId);
                SqlDataReader sqlDataReader=sqlCommand.ExecuteReader();
                if(sqlDataReader.HasRows)
                {
                    List<FeedbackModel> feedbackModel = new List<FeedbackModel>();
                    while(sqlDataReader.Read())
                    {
                        FeedbackModel feedback=new FeedbackModel();
                        UserModel user=new UserModel();
                        feedback.FeedbackId = Convert.ToInt32(sqlDataReader["FeedbackId"]);
                        feedback.Comment = sqlDataReader["Comment"].ToString();
                        feedback.Rating= Convert.ToInt32(sqlDataReader["Rating"]);
                        feedback.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        user.FullName= sqlDataReader["FullName"].ToString();
                        feedback.UserModel = user;
                        feedbackModel.Add(feedback);
                    }
                    return feedbackModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
