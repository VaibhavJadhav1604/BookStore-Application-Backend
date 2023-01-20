using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBusiness
    {
        public OrderModel AddOrder(int UserId, OrderModel orderModel);
        public List<OrderModel> GetAllOrders(int UserId);
    }
}
