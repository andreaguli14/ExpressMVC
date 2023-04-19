using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using AgenziaSpedizioni.Models;
using Fare;

namespace AgenziaSpedizioni.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShipmentsController : Controller
    {
        
        private Data.ApplicationDbContext _db;
        public ShipmentsController(Data.ApplicationDbContext context)
        {
            _db = context;

        }

        public IActionResult Index()
        {
            var model = _db.Shipments.ToList();
            return View(model);

        }

        public IActionResult AddShipment()
        {
            Shipment model = new Shipment();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddShipment(Shipment model)
        {
            var tracking = new Xeger(@"\b1Z[A-Z0-9]{16}\b");
            model.Traking =  tracking.Generate();
            _db.Shipments.Add(model);
            _db.Users.FirstOrDefault(x => x.Id.Equals(Int32.Parse(model.user))).Shipments.Add(model.Traking);
            _db.SaveChanges();
            var status = new Status();
            status.Shipment_Id = _db.Shipments.OrderBy(x => x.Id).Last().Id;
            _db.Statuses.Add(status);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateStatus(string status,int id, string location)
        {
            var ship =  _db.Shipments.FirstOrDefault(x => x.Id.Equals(id));
            var state = _db.Statuses.FirstOrDefault(x => x.Shipment_Id.Equals(id));
            ship.Status = status;

            if(status == "Accepted")
            {
                state.Accepted = true;
                state.Accepted_Time = DateTime.Now.ToString();
                state.Accepted_Location = location;
            }
            else if(status == "Transit")
            {
                state.Transit = true;
                state.Transit_Time = DateTime.Now.ToString();
                state.Transit_Location = location;
            }
            else if (status == "Delivered")
            {
                state.Delivered = true;
                state.Delivered_Time = DateTime.Now.ToString();
                state.Delivered_Location = location;
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult EditShipment(int id)
        {
            var model = _db.Shipments.FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditShipmentPost(Shipment model, int id)
        {
            var ship = _db.Shipments.FirstOrDefault(x => x.Id.Equals(id));
            ship.user = model.user;
            ship.Shipment_Date = model.Shipment_Date;
            ship.Receipt_Date = model.Receipt_Date;
            ship.Country = model.Country;
            ship.Address = model.Address;
            ship.Destination = model.Destination;
            ship.Weight = model.Weight;
            ship.Price = model.Price;

            _db.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult DeleteShipment(int id)
        {
            try
            {
                var ship = _db.Shipments.FirstOrDefault(x => x.Id.Equals(id));
                var status = _db.Statuses.FirstOrDefault(x => x.Shipment_Id.Equals(id));
                _db.Shipments.Remove(ship);
                _db.Statuses.Remove(status);
                _db.SaveChanges();

                    _db.Users.FirstOrDefault(x => x.Shipments.Contains(ship.Traking)).Shipments.Remove(ship.Traking);
                    _db.SaveChanges();
                }
                catch (Exception e) { return RedirectToAction("Index"); }

           
            return RedirectToAction("Index");
        }

    }
}


