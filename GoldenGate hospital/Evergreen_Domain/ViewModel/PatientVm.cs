using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Evergreen_Domain.ViewModel
{
	public class PatientVm:BaseEntity
	{
		  [Required]
        [MinLength(3)]
        [MaxLength(100)]
		public string FullName { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
		public DateTime BirthDay { get; set; }
		public int Age { get; set; }
		public string Adress { get; set; }
		public string ImgUrl { get; set; }
		[Required]
		[DataType(DataType.PhoneNumber)]
		public double Phone { get; set; }
		public double Height { get; set; }
		public double Weight { get; set; }
		public DateTime LastVisit { get; set; }
		[NotMapped]
		public IFormFile ImgFile { get; set; }

	}
}
