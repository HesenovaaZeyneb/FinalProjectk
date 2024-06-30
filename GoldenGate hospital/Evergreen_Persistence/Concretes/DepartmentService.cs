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
    public class DepartmentService : IDepartmentService
    {
        AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public void Create(DepartmentVm department)
        {
            Department departmentt = new Department()
            {
                Name = department.Name,
                Description = department.Description,
                ImgUrl = department.ImgUrl,
                
            };
            _context.Departments.Add(departmentt);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
           var department= _context.Departments.FirstOrDefault(x=>x.Id==id);
            _context.Departments.Remove(department);
            _context.SaveChanges();
        }

        public DepartmentVm Get(int id)
        {
            var department=_context.Departments.FirstOrDefault(x=> x.Id==id);
            DepartmentVm department1 = new DepartmentVm()
            {
                Id=department.Id,
                Name = department.Name,
                Description = department.Description,
                ImgUrl = department.ImgUrl,
            };
            return department1;
        }

        public List<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public void Update(DepartmentVm newdepartment)
        {
            var olddepartment = _context.Departments.FirstOrDefault(x=>x.Id == newdepartment.Id);
            olddepartment.Id = newdepartment.Id;
            olddepartment.Name= newdepartment.Name;
            olddepartment.Description= newdepartment.Description;
            olddepartment.ImgUrl= newdepartment.ImgUrl;
           _context.SaveChanges();
        }
    }
}
