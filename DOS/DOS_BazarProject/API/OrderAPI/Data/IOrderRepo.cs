using System;
using System.Collections.Generic;
using OrderAPI.Model;

namespace OrderAPI.Data
{
    public interface IOrderRepo
    {

        public void Purchase(Order order);

        public bool SaveChanges();

        public IEnumerable<Order> GetAllOrders();

      //  public Order GetOrderById(Guid id);



    }
}
