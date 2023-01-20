using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBusiness:IAddressBusiness
    {
        private readonly IAddressRepo addressRepo;
        public AddressBusiness(IAddressRepo addressRepo)
        {
            this.addressRepo = addressRepo;
        }

        public bool AddAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                return addressRepo.AddAddress(UserId, addressModel);
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
                return addressRepo.UpdateAddress(UserId, addressModel);
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
                return addressRepo.DeleteAddress(UserId, AddressId);
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
                return addressRepo.GetAllAddress(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
