using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;

namespace Evergreen_Application.Abstractions
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        void Create (DepartmentVm department);
        void Update (DepartmentVm newdepartment);
        void Delete (int id);
        DepartmentVm Get (int id);
    }
}
