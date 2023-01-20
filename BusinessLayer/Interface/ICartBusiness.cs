using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBusiness
    {
        public bool AddCart(int UserId, int BookId, int Quantity);
        public bool UpdateCart(int UserId, int CartId, int Quantity);
        public bool DeleteCart(int UserId, int CartId);
        public List<CartModel> GetCartDetails(int Userid);
    }
}
