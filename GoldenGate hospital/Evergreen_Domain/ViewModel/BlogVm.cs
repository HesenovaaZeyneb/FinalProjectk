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
    public class BlogVm:BaseEntity
    {
        [Required]
        [MinLength(3)]
      
        public string Title { get; set; }
        [Required]
        [MinLength(3)]
        
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime? Date { get; set; }
        [NotMapped]
        public IFormFile? ImgFile { get; set; }
    }
}
