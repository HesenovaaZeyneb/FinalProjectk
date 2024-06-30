using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain
{
    public class Appointment:BaseEntity
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctors { get; set; }
        public string Comment { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
