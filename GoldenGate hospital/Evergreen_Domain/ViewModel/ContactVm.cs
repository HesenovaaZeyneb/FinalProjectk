using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain.ViewModel
{
	public class ContactVm
	{
		public string UserName { get; set; }
		public string Subject { get; set; }
		public string Comment { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
	}
}
