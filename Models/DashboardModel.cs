using System;
using System.ComponentModel.DataAnnotations;


namespace AgenziaSpedizioni.Models
{
	public class Dashboard
    {
        public List<Shipment> currentday { get; set; }

        public List<Shipment> inprogress { get; set; }

        public List<IGrouping<string, Shipment>> byCountry { get; set; }
    }
}

