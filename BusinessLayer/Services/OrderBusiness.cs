using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBusiness:IOrderBusiness
    {
        private readonly IOrderRepo orderRepo;
        public OrderBusiness(IOrderRepo orderRepo)
        {
            this.orderRepo = orderRepo;
        }
        public OrderModel AddOrder(int UserId, OrderModel orderModel)
        {
            try
            {
                return orderRepo.AddOrder(UserId, orderModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<OrderModel> GetAllOrders(int UserId)
        {
            try
            {
                return orderRepo.GetAllOrders(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
