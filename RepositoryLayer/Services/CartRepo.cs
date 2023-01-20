using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRepo:ICartRepo
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public CartRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool AddCart(int UserId, int BookId, int Quantity)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Insert Into Cart Values({Quantity},{UserId},{BookId})";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public bool UpdateCart(int UserId, int CartId, int Quantity)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Update Cart Set Quantity={Quantity} Where CartId={CartId} And UserId={UserId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
        public bool DeleteCart(int UserId, int CartId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Delete From Cart Where CartId={CartId} And UserId={UserId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
        public List<CartModel> GetCartDetails(int Userid)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetCartByUserId", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@Userid", Userid);

                SqlDataReader sqlDataReader=sqlCommand.ExecuteReader();
                if(sqlDataReader.HasRows)
                {
                    List<CartModel> cartDetails = new List<CartModel>();
                    while(sqlDataReader.Read())
                    {
                        BookModel bookModel=new BookModel();
                        CartModel cartModel=new CartModel();
                        bookModel.BookName = sqlDataReader["BookName"].ToString();
                        bookModel.AuthorName= sqlDataReader["AuthorName"].ToString();
                        bookModel.DiscountPrice = Convert.ToDouble(sqlDataReader["DiscountPrice"]);
                        bookModel.OriginalPrice= Convert.ToDouble(sqlDataReader["OriginalPrice"]);
                        bookModel.BookImage = sqlDataReader["BookImage"].ToString();
                        cartModel.CartId = Convert.ToInt32(sqlDataReader["CartId"]);
                        cartModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        cartModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        cartModel.Quantity= Convert.ToInt32(sqlDataReader["Quantity"]);
                        cartModel.bookModel= bookModel;
                        cartDetails.Add(cartModel);
                    }
                    return cartDetails;
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
