using Orderiator.DataModel.Models;
using System.Collections.Generic;

namespace Orderiator.Services
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();

        Customer GetCustomer(string customerId);
       
        IEnumerable<OrderDto> GetOrders(string customerId);
    }
}
