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
    public class AddressRepo:IAddressRepo
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public AddressRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool AddAddress(int UserId,AddressModel addressModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("AddAddress", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@FullAddress", addressModel.FullAddress);
                sqlCommand.Parameters.AddWithValue("@City", addressModel.City);
                sqlCommand.Parameters.AddWithValue("@State", addressModel.State);
                sqlCommand.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                int result = sqlCommand.ExecuteNonQuery();
                if (result!=0)
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
        }

        public bool UpdateAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Update Address Set FullAddress='{addressModel.FullAddress}',City='{addressModel.City}',State='{addressModel.State}',TypeId={addressModel.TypeId} Where AddressId={addressModel.AddressId} And UserId={UserId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result!=0) 
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
        }
        public bool DeleteAddress(int UserId, int AddressId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                string query = $"Delete From Address Where AddressId={AddressId} And UserId={UserId}";
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
        }
        public List<AddressModel> GetAllAddress(int UserId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["Connection:BookStore"]);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetAddressByUserId", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader sqlDataReader=sqlCommand.ExecuteReader();
                if(sqlDataReader.HasRows)
                {
                    List<AddressModel> addressModels= new List<AddressModel>();
                    while(sqlDataReader.Read())
                    {
                        AddressModel address = new AddressModel();
                        AddressTypeModel Type = new AddressTypeModel();
                        address.AddressId = Convert.ToInt32(sqlDataReader["AddressId"]);
                        address.City = sqlDataReader["City"].ToString();
                        address.State = sqlDataReader["State"].ToString();
                        Type.TypeName = sqlDataReader["TypeName"].ToString();
                        address.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        address.addressType= Type;
                        addressModels.Add(address);
                    }
                    return addressModels;
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
