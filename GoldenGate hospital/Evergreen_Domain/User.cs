using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain.OrderandBasket;
using Microsoft.AspNetCore.Identity;

namespace Evergreen_Domain
{
    public  class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDay { get; set; }
        //public byte? BloodGroup { get; set; }
		public string? Adress { get; set; }
		public bool IsRemained { get; set; }
		public List<BasketItem> BasketItems { get; set; }
		public List<Order> Orders { get; set; }	
        public string DoctorId { get; set; }
		
	}
}
