using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Evergreen_Domain
{
	public class Product:BaseEntity
	{
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
    
        public string Description { get; set; }
		public double Price { get; set; }
		public string? ImgUrl { get; set; }
		public int? CategoryId { get; set; }
		public Category? Category { get; set; }
		[NotMapped]
		public IFormFile ImgFile { get; set; }
	}
}
