using System;
using Microsoft.AspNetCore.Mvc;
using AgenziaSpedizioni.Models;

namespace AgenziaSpedizioni.Controllers
{

    public class TrackingController : Controller
    {

        private Data.ApplicationDbContext _db;
        public TrackingController(Data.ApplicationDbContext context)
        {
            _db = context;

        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            if(search != "" && search != null) { 
            var tracking = _db.Shipments.FirstOrDefault(x => x.Traking.Equals(search));
            var cf = _db.Users.FirstOrDefault(x => x.CF.Equals(search));
            var vat = _db.Users.FirstOrDefault(x => x.Vat.Equals(search));


            if (tracking != null)
            {
                return RedirectToAction("TrackingStatus",new { tracking = tracking.Traking} );
            }
            else if(cf != null)
            {
                var model = new List<Shipment>();
                foreach (var ship in cf.Shipments)
                {
                    model.Add(_db.Shipments.FirstOrDefault(x => x.Traking.Equals(ship)));
                }

                return View("MultiTracking", model);

            }
            else if (vat != null)
            {
                var model = new List<Shipment>();

                foreach (var ship in vat.Shipments)
                {
                    model.Add(_db.Shipments.FirstOrDefault(x => x.Traking.Equals(ship)));
                }

                return View("MultiTracking", model);
            }
            }
            return View();

        }

        public IActionResult MultiTracking(List<Shipment> list)
        {

            return View(list);
        }

        public IActionResult TrackingStatus(string tracking) {

            var model = _db.Shipments.FirstOrDefault(x => x.Traking.Equals(tracking));

            return View(model);
        }




    }

        
}

