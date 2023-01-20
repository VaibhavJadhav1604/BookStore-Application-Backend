using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishlistBusiness
    {
        public bool AddToWishlist(int UserId, int BookId);
        public bool DeleteFromWishlist(int UserId, int WishlistId);
        public List<WishlistModel> GetWishlisDetails(int UserId);
    }
}
