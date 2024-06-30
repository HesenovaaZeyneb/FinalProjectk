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
    public class DoctorsVm
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? ImgUrl { get; set; }
        public int? DepartmentId { get; set; }
        [Required]
        [MinLength(3)]
   
        public string Details { get; set; }
        public Department?  Department { get; set; }
        public TimeSpan Date { get; set; }
        public double Payment { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public double Phone { get; set; }
        public string Adres { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public string UserId {  get; set; }


    }
}
