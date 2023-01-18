using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRepo:IAdminRepo
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public AdminRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AdminLogin(string EmailId, string Password)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Select * From Admin Where EmailId='{EmailId}' And Password='{Password}'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var AdminId = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (AdminId != 0)
                {
                    var token = GenerateToken(EmailId, AdminId);
                    return token;
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

        public string GenerateToken(string Email, long AdminId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration[("JWT:Key")]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.Email, Email),
                        new Claim("Admin", AdminId.ToString())
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
    }
}
