using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;

namespace Evergreen_Application.Abstractions
{
    public interface IDoctorsService
    {
        List<Doctor> GetAll();
        void Create (DoctorsVm doctor);
        void Update (DoctorsVm newdoctor);
        void Delete (int id);
        DoctorsVm Get (int id);


    }
}
