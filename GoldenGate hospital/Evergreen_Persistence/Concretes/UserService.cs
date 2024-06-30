using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Application.Abstractions;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Evergreen_Persistence.Concretes
{
	public class UserService : IUserService
	{
		private readonly AppDbContext _context;

		public UserService(AppDbContext context)
		{
			_context = context;
		}

		public List<User> GetAllUsers()
		{

			return _context.Users.ToList();
		}

        
    }
}
