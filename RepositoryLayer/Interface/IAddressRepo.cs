using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRepo
    {
        public bool AddAddress(int UserId,AddressModel addressModel);
        public bool UpdateAddress(int UserId, AddressModel addressModel);
        public bool DeleteAddress(int UserId, int AddressId);
        public List<AddressModel> GetAllAddress(int UserId);
    }
}
