using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRepo:IOrderRepo
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public OrderRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public OrderModel AddOrder(int UserId, OrderModel orderModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("AddOrder", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@UserId",UserId);
                sqlCommand.Parameters.AddWithValue("@BookId",orderModel.BookId);
                sqlCommand.Parameters.AddWithValue("@AddressId",orderModel.AddressId);
                int result=sqlCommand.ExecuteNonQuery();
                if(result != 0)
                {
                    return orderModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OrderModel> GetAllOrders(int UserId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetAllOrders", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if(sqlDataReader.HasRows)
                {
                    List<OrderModel> orderModel = new List<OrderModel>();
                    while(sqlDataReader.Read())
                    {
                        OrderModel order= new OrderModel();
                        BookModel book= new BookModel();
                        order.OrderId = Convert.ToInt32(sqlDataReader["OrderId"]);
                        order.UserId= Convert.ToInt32(sqlDataReader["UserId"]);
                        order.AddressId = Convert.ToInt32(sqlDataReader["AddressId"]);
                        order.OrderDate = Convert.ToDateTime(sqlDataReader["OrderDate"]);
                        order.BookQuantity= Convert.ToInt32(sqlDataReader["BookQuantity"]);
                        order.TotalPrice = Convert.ToInt32(sqlDataReader["TotalPrice"]);
                        book.BookName = sqlDataReader["BookName"].ToString();
                        book.AuthorName= sqlDataReader["AuthorName"].ToString();
                        book.BookImage= sqlDataReader["BookImage"].ToString();
                        book.DiscountPrice= Convert.ToInt32(sqlDataReader["DiscountPrice"]);
                        book.OriginalPrice= Convert.ToInt32(sqlDataReader["OriginalPrice"]);
                        order.bookModel = book;
                        orderModel.Add(order);
                    }
                    return orderModel;
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
