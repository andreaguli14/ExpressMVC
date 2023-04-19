using System;
using System.ComponentModel.DataAnnotations;


namespace AgenziaSpedizioni.Models
{
	public class Shipment
	{
        [Key]
        public int Id { get; set; }

		public string user { get; set; }

        public string Shipment_Date { get; set; }

        public string Destination { get; set; }

        public string Address { get; set; }

        public double? Price { get; set; }

        public double? Weight { get; set; }

        public string Country { get; set; }

        public string Receipt_Date { get; set; }

        public string? Status { get; set; }

        public string? Traking { get; set; } 

    }
}

