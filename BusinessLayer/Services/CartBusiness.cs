using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBusiness:ICartBusiness
    {
        public ICartRepo cartRepo;
        public CartBusiness(ICartRepo cartRepo)
        {
            this.cartRepo = cartRepo;
        }
        public bool AddCart(int UserId, int BookId, int Quantity)
        {
            try
            {
                return this.cartRepo.AddCart(UserId, BookId, Quantity);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateCart(int UserId, int CartId, int Quantity)
        {
            try
            {
                return this.cartRepo.UpdateCart(UserId,CartId,Quantity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteCart(int UserId, int CartId)
        {
            try
            {
                return this.cartRepo.DeleteCart(UserId, CartId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CartModel> GetCartDetails(int Userid)
        {
            try
            {
                return this.cartRepo.GetCartDetails(Userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
