using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishlistBusiness:IWishlistBusiness
    {
        public IWishlistRepo wishlistRepo;
        public WishlistBusiness(IWishlistRepo wishlistRepo)
        {
            this.wishlistRepo = wishlistRepo;
        }
        public bool AddToWishlist(int UserId, int BookId)
        {
            try
            {
                return wishlistRepo.AddToWishlist(UserId, BookId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteFromWishlist(int UserId, int WishlistId)
        {
            try
            {
                return wishlistRepo.DeleteFromWishlist(UserId,WishlistId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<WishlistModel> GetWishlisDetails(int UserId)
        {
            try
            {
                return wishlistRepo.GetWishlisDetails(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
