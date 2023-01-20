using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRepo
    {
        public bool AddCart(int UserId,int BookId,int Quantity);
        public bool UpdateCart(int UserId,int CartId,int Quantity);
        public bool DeleteCart(int UserId, int CartId);
        public List<CartModel> GetCartDetails(int Userid);
    }
}
