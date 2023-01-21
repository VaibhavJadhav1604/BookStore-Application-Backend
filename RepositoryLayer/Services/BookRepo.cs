using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace RepositoryLayer.Services
{
    public class BookRepo:IBookRepo
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public BookRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Insert Into Books Values('{bookModel.BookName}','{bookModel.AuthorName}','{bookModel.BookDescription}','{bookModel.BookImage}',{bookModel.Rating},{bookModel.TotalPersonRated},{bookModel.Quantity},{bookModel.OriginalPrice},{bookModel.DiscountPrice})";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    return bookModel;
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
            finally
            {
                sqlConnection.Close();
            }
        }
        public BookModel UpdateBook(BookModel bookModel)
        {
            try
            {

                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Update Books Set BookName = '{bookModel.BookName}',AuthorName = '{bookModel.AuthorName}',BookDescription = '{bookModel.BookDescription}', BookImage = '{bookModel.BookImage}',Rating = '{bookModel.Rating}', TotalPersonRated = '{bookModel.TotalPersonRated}', Quantity = '{bookModel.Quantity}',OriginalPrice = '{bookModel.OriginalPrice}',DiscountPrice = '{bookModel.DiscountPrice}' Where BookId = '{bookModel.BookId}'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    return bookModel;
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
        public bool DeleteBook(int BookId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Delete From Books Where BookId={BookId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var result = sqlCommand.ExecuteNonQuery();
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
        public List<BookModel> GetAllBooks()
        {
            List<BookModel> books = new List<BookModel>();
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Select * From Books";
                SqlCommand sqlCommand = new SqlCommand(query,sqlConnection);
                SqlDataReader sqlDataReaderreader = sqlCommand.ExecuteReader();
                if(sqlDataReaderreader.HasRows)
                {
                    while(sqlDataReaderreader.Read())
                    {
                        BookModel bookModel= new BookModel()
                        {
                            BookId= sqlDataReaderreader.GetInt32(0),
                            BookName = sqlDataReaderreader.GetString(1),
                            AuthorName = sqlDataReaderreader.GetString(2),
                            BookDescription = sqlDataReaderreader.GetString(3),
                            BookImage = sqlDataReaderreader.GetString(4),
                            Rating = (float)sqlDataReaderreader.GetDouble(5),
                            TotalPersonRated = sqlDataReaderreader.GetInt32(6),
                            Quantity = sqlDataReaderreader.GetInt32(7),
                            OriginalPrice = sqlDataReaderreader.GetDouble(8),
                            DiscountPrice = sqlDataReaderreader.GetDouble(9),
                        };
                        books.Add(bookModel);
                    }
                    return books;
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
        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Select * From Books Where BookId={BookId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader sqlDataReaderreader = sqlCommand.ExecuteReader();
                if (sqlDataReaderreader.HasRows)
                {
                    BookModel bookModel= new BookModel();
                    while (sqlDataReaderreader.Read())
                    {
                        bookModel.BookName = sqlDataReaderreader.GetString(1);
                        bookModel.AuthorName = sqlDataReaderreader.GetString(2);
                        bookModel.BookDescription = sqlDataReaderreader.GetString(3);
                        bookModel.BookImage = sqlDataReaderreader.GetString(4);
                        bookModel.Rating = (float)sqlDataReaderreader.GetDouble(5);
                        bookModel.TotalPersonRated = sqlDataReaderreader.GetInt32(6);
                        bookModel.Quantity = sqlDataReaderreader.GetInt32(7);
                        bookModel.OriginalPrice = sqlDataReaderreader.GetDouble(8);
                        bookModel.DiscountPrice = sqlDataReaderreader.GetDouble(9);
                    }
                    return bookModel;
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
