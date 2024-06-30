using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;

namespace Evergreen_Application.Abstractions
{
    public interface IBlogService
    {
        List<Blog> Getall();
        void Create(BlogVm blogvm);
        void Update(BlogVm newblogvm);
        void Delete(int id);
        BlogVm Get(int id);
    }
}
