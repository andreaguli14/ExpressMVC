using System;
using System.ComponentModel.DataAnnotations;

namespace AgenziaSpedizioni.Models
{
	public class Status
	{
		[Key]
		public int Id { get; set; }

		public int Shipment_Id { get; set; }
		
		public bool Accepted { get; set; } = false;

        public string? Accepted_Time { get; set; }

		public string? Accepted_Location { get; set; }

        public bool Transit { get; set; } = false;

        public string? Transit_Time { get; set; }

        public string? Transit_Location { get; set; }

		public bool Delivered { get; set; } = false;

        public string? Delivered_Time { get; set; }

        public string? Delivered_Location { get; set; }

    }
}

