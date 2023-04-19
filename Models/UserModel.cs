using System;
using System.ComponentModel.DataAnnotations;

namespace AgenziaSpedizioni.Models
{
	public class User
	{
        [Key]
        public int Id { get; set; }

		public string Name { get; set; }

        public string Surname { get; set; }

        public double? Number { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Postal_Code { get; set; }

        public string? Country { get; set; }

        public string? Customer_Type { get; set; }

        public string? CF { get; set; }

        public string? Vat { get; set; }

        public List<string>? Shipments { get; set; } = new List<string>();
      
    }
}

