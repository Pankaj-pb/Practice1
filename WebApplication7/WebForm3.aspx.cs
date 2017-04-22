using System;
using System.Collections.Generic;

namespace WebApplication7
{
    public partial class WebForm3 : System.Web.UI.Page
    {




        protected void Page_Load(object sender, EventArgs e)
        {
            Customer1 c = new Customer1();
            foreach (var order in c.Orders)
            {

            }
        }



    }

    public class Customer1
    {
        private Lazy<List<Order>> _orders = null;

        public List<Order> Orders
        {
            get
            {
                return _orders.Value;
            }
        }
        public Customer1()
        {
            this.Name = "TestCustomer";
            _orders = new Lazy<List<Order>>(() => LoadOrders());
        }
        public string Name { get; set; }
        public List<Order> LoadOrders()
        {
            return new List<Order>() { new Order() { Id = 1, Price = 100 }, new Order { Id = 2, Price = 200 } };
        }


    }

    public class Order
    {
        public Order()
        {

        }
        public int Id { get; set; }
        public int Price { get; set; }
    }
}