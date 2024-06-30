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
	public class CategoryVm
	{
		public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
		public List<Product>? Products { get; set; }
		//public string?  ImgUrl { get; set; }
		//[NotMapped]
		//public IFormFile ImgFile { get; set; }
	}
}
