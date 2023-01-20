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
    public class WishlistRepo:IWishlistRepo
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public WishlistRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddToWishlist(int UserId, int BookId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Insert Into Wishlist Values({UserId},{BookId})";
                SqlCommand sqlCommand=new SqlCommand(query,sqlConnection);
                int result=sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public bool DeleteFromWishlist(int UserId, int WishlistId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Delete From Wishlist Where WishlistId={WishlistId} And UserId={UserId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public List<WishlistModel> GetWishlisDetails(int UserId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetWishlistByUserId",sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("UserId", UserId);
                SqlDataReader sqlDataReader=sqlCommand.ExecuteReader();
                if(sqlDataReader.HasRows)
                {
                    List<WishlistModel> wishlistDetails=new List<WishlistModel>();
                    while(sqlDataReader.Read())
                    {
                        BookModel book=new BookModel();
                        WishlistModel wishlist=new WishlistModel();
                        book.BookName = sqlDataReader["BookName"].ToString();
                        book.AuthorName= sqlDataReader["AuthorName"].ToString();
                        book.DiscountPrice = Convert.ToDouble(sqlDataReader["DiscountPrice"]);
                        book.OriginalPrice = Convert.ToDouble(sqlDataReader["OriginalPrice"]);
                        book.BookImage = sqlDataReader["BookImage"].ToString();
                        wishlist.WishlistId = Convert.ToInt32(sqlDataReader["WishlistId"]);
                        wishlist.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        wishlist.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        wishlist.bookModel= book;
                        wishlistDetails.Add(wishlist);
                    }
                    return wishlistDetails;
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
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
