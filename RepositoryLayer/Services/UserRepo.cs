using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CommonLayer.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public static string name;
        public UserRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public UserModel Registration(UserModel userModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Insert Into Users Values('{userModel.FullName}','{userModel.EmailId}','{EncryptPassword(userModel.Password)}','{userModel.PhoneNumber}')";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                return userModel;
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
        public string Login(string EmailId,string Password)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Select * From Users Where EmailId='{EmailId}' And Password='{EncryptPassword(Password)}'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var UserId = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (UserId != 0)
                {
                    var token = GenerateToken(EmailId, UserId);
                    return token;
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

        public string ForgotPassword(string EmailId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Select * From Users Where EmailId='{EmailId}'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var UserId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        name = dr.GetString(1);
                    }
                }
                if (UserId != 0)
                {
                    var token = GenerateToken(EmailId, UserId);
                    MSMQ mSMQ = new MSMQ();
                    mSMQ.sendData2Queue(token, EmailId, name);
                    return token;
                }
                return null;

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

        public bool ResetPassword(string EmailId, string Password, string ConfirmPassword)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Select * From Users Where EmailId='{EmailId}'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var UserId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                if (UserId != 0)
                {
                    query = $"Update Users Set Password='{EncryptPassword(Password)}' Where EmailId='{EmailId}'";
                    sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
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
        public string GenerateToken(string Email, long UserId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration[("JWT:Key")]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Email, Email),
                        new Claim("UserId", UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string EncryptPassword(string Password)
        {
            try
            {
                byte[] enData_byte = new byte[Password.Length];
                enData_byte = System.Text.Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(enData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string DecryptPassword(string Password)
        {
            System.Text.UTF8Encoding encoder = new UTF8Encoding();
            System.Text.Decoder decoder = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(Password);
            int charCount = decoder.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decodedchar = new char[charCount];
            decoder.GetChars(todecode_byte, 0, todecode_byte.Length, decodedchar, 0);
            string result = new string(decodedchar);
            return result;
        }
        public UserModel GetUserById(int UserId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Select * From Users Where UserId={UserId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    UserModel model = new UserModel();
                    while (reader.Read())
                    {
                        model.FullName = reader["FullName"].ToString();
                        model.EmailId = reader["EmailId"].ToString();
                        model.Password = DecryptPassword(reader["Password"].ToString());
                        model.PhoneNumber = Convert.ToInt64(reader["PhoneNumber"]);
                    }
                    return model;
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
    }
}
