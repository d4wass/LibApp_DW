using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET /api/customers
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _context.Customers
                                        .Include(c => c.MembershipType)
                                        .ToList()
                                        .Select(_mapper.Map<Customer, CustomerDto>);
            return Ok(customers);
        }

        // GET /api/customers/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            Console.WriteLine("START REQUEST");
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            await Task.Delay(2000);
            if (customer == null)
            {
                return NotFound();
            }
            
            Console.WriteLine("END REQUEST");
            return Ok(_mapper.Map<CustomerDto>(customer));
        }


        [HttpGet("details/{id:int}")]
        public IActionResult GetCustomerDetail(int id)
        {
            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);
            return Ok(customer);
        }
        
        // POST /api/customers
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customer = _mapper.Map<Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;

            return customerDto;
        }

        // PUT /api/customers/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Owner")]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _mapper.Map(customerDto, customerInDb);
            _context.SaveChanges();
        }

        // DELETE /api/customers
        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
    }
}
