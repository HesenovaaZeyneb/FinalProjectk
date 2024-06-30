using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain.ViewModel
{
    public class QuestionVm:BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
