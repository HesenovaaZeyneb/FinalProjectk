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
    public class DepartmentVm
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Appointment>? Appointments { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
