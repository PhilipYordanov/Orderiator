using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Orderiator.Web.Helpers;
using Orderiator.Web.Infrastructure;
using Orderiator.Web.Models;
using Orderiator.Web.ViewModels.CustomerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Orderiatorr.Controllers
{
    public class CustomerController : Controller
    {
        private IHttpClientInitializer _httpClientInitializer;

        public CustomerController(IHttpClientInitializer httpClientInitializer)
        {
            this._httpClientInitializer = httpClientInitializer;
        }
        
        public async Task<IActionResult> Index(string searchString, int? page, string currentFilter)
        {
            List<CustomerDTO> customers = new List<CustomerDTO>();

            HttpResponseMessage response = await _httpClientInitializer
                .InitializeClient()
                .GetAsync("api/customers");

            if (response.IsSuccessStatusCode)
            { 
                customers = JsonConvert.DeserializeObject<List<CustomerDTO>>(await response.Content.ReadAsStringAsync());
            }
            var viewCustomers = Mapper.Map<IEnumerable<CustomersViewModel>>(customers);

            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                viewCustomers = viewCustomers.Where(s => s.ContactName.Contains(searchString));
            }
            if (searchString != null)
            {
                page = GlobalConstants.DefaultPageSize;
            }
            else
            {
                searchString = currentFilter;
            }
            int pageSize = GlobalConstants.PageSize;
            return View( PaginatedList<CustomersViewModel>.Create(viewCustomers, page ?? GlobalConstants.DefaultPageSize, pageSize));
        }

        [HttpGet("{customerId}")]
        public async Task <IActionResult> Contact(string customerId)
        {
            HttpResponseMessage response = await _httpClientInitializer
               .InitializeClient()
               .GetAsync($"api/customers/{customerId}");

            CustomerDTO customer = new CustomerDTO();
            if (response.IsSuccessStatusCode)
            {
                customer = JsonConvert.DeserializeObject<CustomerDTO>(await response.Content.ReadAsStringAsync());
            }
            return View(customer);
        }

        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> About(string customerId)
        {
            HttpResponseMessage response = await _httpClientInitializer
               .InitializeClient()
               .GetAsync($"api/customers/{customerId}/orders");

            object customer = null;
            if (response.IsSuccessStatusCode)
            {
                var responsee = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<CustomerDTO>(await response.Content.ReadAsStringAsync());
            }

            return View(customer);
        }
    }
}
