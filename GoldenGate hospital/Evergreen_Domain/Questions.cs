using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain
{
    public class Questions:BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
