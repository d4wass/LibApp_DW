using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace LibApp.Controllers
{
    [Authorize(Roles = "Owner, StoreManager")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMembershipRepository _membershipRepository;
        
        public CustomersController(ICustomerRepository customerRepository, IMembershipRepository membershipRepository)
        {
            _customerRepository = customerRepository;
            _membershipRepository = membershipRepository;
        }
        
        public ViewResult Index()
        {          
            return View();
        }
        
        public IActionResult Details(int id)
        {
            var customer = _customerRepository.GetCustomersIncludeMembershipType()
                .SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }
        
        [Authorize(Roles = "Owner")]
        public IActionResult New()
        {
            var membershipTypes = _membershipRepository.GetMembershipTypes().ToList();
            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }
        
        [Authorize(Roles = "Owner")]
        public IActionResult Edit(int id)
        {
            var customer = _customerRepository.GetCustomers().SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _membershipRepository.GetMembershipTypes().ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel(customer)
                {
                    MembershipTypes = _membershipRepository.GetMembershipTypes().ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _customerRepository.AddCustomer(customer);

            }
            else
            {
                var customerInDb = _customerRepository.GetCustomers().Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.HasNewsletterSubscribed = customer.HasNewsletterSubscribed;
            }

            _customerRepository.Save();

            return RedirectToAction("Index", "Customers");
        }
        
    }
}