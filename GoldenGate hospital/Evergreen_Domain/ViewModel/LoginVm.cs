using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain.ViewModel
{
    public class LoginVm
    {
        public string EmailorUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemembeMe { get; set; }
    }
}
