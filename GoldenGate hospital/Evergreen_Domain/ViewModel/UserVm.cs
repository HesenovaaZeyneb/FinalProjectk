using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Evergreen_Domain.ViewModel
{
    public class UserVm
    {
        public string Id { get; set; }  
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public byte? BloodGroup { get; set; }
        public DateTime BirthDay { get; set; }
       


    }
}
