using Microsoft.EntityFrameworkCore;
using Orderiator.DataModel;
using Orderiator.DataModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace Orderiator.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderiatorDbContext _ctx;

        public CustomerRepository(OrderiatorDbContext ctx)
        {
            this._ctx = ctx;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _ctx.Customers
                .Include(c => c.Orders)
                .ToList();
        }

        public Customer GetCustomer(string customerId)
        {
            return _ctx.Customers
                .Include(c => c.Orders)
                .ThenInclude(x => x.OrderDetails)
                .FirstOrDefault(c => c.CustomerId == customerId.ToUpper());
        }

        public IEnumerable<OrderDto> GetOrders(string customerId)
        {
            var customerOrders = from customer in _ctx.Customers
                                 where customer.CustomerId.Equals(customerId.ToUpper())
                                 join order in _ctx.Orders
                                 on customer.CustomerId equals order.CustomerId
                                 select order;

            var result = new List<OrderDto>();

            foreach (var order in customerOrders)
            {
                var orderDetailsDto = from orderDetail in _ctx.OrderDetails.Where(x => x.OrderId.Equals(order.OrderId))
                                      join product in _ctx.Products
                                      on orderDetail.ProductId equals product.ProductId
                                      select new OrderDetailsDto
                                      {
                                          Quantity = orderDetail.Quantity,
                                          Product = new ProductDto
                                          {
                                              IsDiscontinued = product.Discontinued,
                                              ProductName = product.ProductName,
                                              Quantity = product.UnitsInStock,
                                              UnitPrice = product.UnitPrice
                                          }
                                      };

                result.Add(new OrderDto
                {
                    OrderDetails = orderDetailsDto
                });
            }

            return result;
        }
    }

    public class OrderDto
    {
        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; } = new List<OrderDetailsDto>();
    }

    public class OrderDetailsDto
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal OrderPrice => Quantity * Product.UnitPrice;
    }

    public class ProductDto
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}
