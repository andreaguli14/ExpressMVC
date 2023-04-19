using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using AgenziaSpedizioni.Models;
using AgenziaSpedizioni.Data;

namespace AgenziaSpedizioni.Controllers
{

    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {

        private Data.ApplicationDbContext _db;
        public DashboardController(Data.ApplicationDbContext context)
        {
            _db = context;

        }

        public IActionResult Index()
        {
            var model = new Dashboard();
            //CurrentDayShipment
            model.currentday  = new List<Shipment>();
            foreach(var shipment in _db.Shipments)
            {
               var receipt = DateTime.ParseExact(shipment.Receipt_Date,"yyyy-MM-dd",null).ToString("dd/MM/yyyy");
                var arrive = DateTime.ParseExact(shipment.Shipment_Date, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");
                if (receipt.Equals((DateTime.Now).ToString("dd/MM/yyyy")) || arrive.Equals((DateTime.Now).ToString("dd/MM/yyyy")))
                {

                    model.currentday.Add(shipment);
                }
            }
            //---------------------
            //InProgress
            model.inprogress = new List<Shipment>();
             foreach(var shipment in _db.Shipments)
             {

                if (shipment.Status == "Accepted" || shipment.Status == "Transit")
                {
                    model.inprogress.Add(shipment);
                }
             }
            //---------------------
            //GroupBy Country

            model.byCountry = _db.Shipments.GroupBy(x => x.Country).ToList();
           

            return View(model);

        }
    }

}
