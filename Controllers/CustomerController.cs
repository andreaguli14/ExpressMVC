using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore;
using AgenziaSpedizioni.Data;
using AgenziaSpedizioni.Models;

namespace AgenziaSpedizioni.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private Data.ApplicationDbContext _db;
        public CustomerController(Data.ApplicationDbContext context)
        {
            _db = context;

        }

        public IActionResult Index()
        {
           var model = _db.Users.ToList();
            return View(model);

        }

        public IActionResult AddCustomer()
        {
            User model = new User();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddCustomer(User model)
        {
            _db.Users.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditCustomer(int id)
        {
            var model = _db.Users.FirstOrDefault(x => x.Id.Equals(id));
            return View(model);
        }

        [HttpPost]
        public IActionResult EditCustomerPost(User model, int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id.Equals(id));
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Number = model.Number;
            user.Address = model.Address;
            user.City = model.City;
            user.Country = model.Country;
            user.Postal_Code = model.Postal_Code;
            user.Customer_Type = model.Customer_Type;
            user.CF = model.CF;
            user.Vat = model.Vat;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                var customer = _db.Users.FirstOrDefault(x => x.Id.Equals(id));

            if (customer.Shipments.Any())
            {
                foreach (var ship in customer.Shipments)
                {
                    _db.Shipments.FirstOrDefault(x => x.Traking.Equals(ship)).user = "USER DELETED";
                    
                }
            }
            _db.Users.Remove(customer);
            _db.SaveChanges();

            }
            catch (Exception e) { return RedirectToAction("Index"); }
            return RedirectToAction("Index");
        }

    }
}

