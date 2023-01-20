using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRepo
    {
        public OrderModel AddOrder(int UserId, OrderModel orderModel);
        public List<OrderModel> GetAllOrders(int UserId);
    }
}
