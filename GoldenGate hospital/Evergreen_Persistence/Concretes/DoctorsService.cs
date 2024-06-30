using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Application.Abstractions;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Evergreen_Persistence.Concretes
{
    public class DoctorsService : IDoctorsService
    {
        AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public DoctorsService(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void Create(DoctorsVm doctor)
        {
            
            Doctor doctors = new Doctor()
            {
                FullName = doctor.FullName,
                ImgUrl = doctor.ImgUrl,
                DepartmentId = doctor.DepartmentId,
                Details = doctor.Details,
                Phone = doctor.Phone,
                Department = doctor.Department,
                Adres = doctor.Adres,
              Date = doctor.Date,
                Payment=doctor.Payment,
                UserId =doctor.UserId,
            };
            _context.Doctors.Add(doctors);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var doctor= _context.Doctors.FirstOrDefault(x=>x.Id==id);
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
        }

        public DoctorsVm Get(int id)
        {
           var doctor=_context.Doctors.Include(x=>x.Department).FirstOrDefault(y=>y.Id==id);
            DoctorsVm doctorsVm = new DoctorsVm()
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                ImgUrl = doctor.ImgUrl,
                DepartmentId = doctor.DepartmentId,
                Details = doctor.Details,
                Phone = doctor.Phone,
                Department = doctor.Department,
                Adres = doctor.Adres,
                Date = doctor.Date,
                Payment = doctor.Payment,
            };
            return doctorsVm;
        }

        public List<Doctor> GetAll()
        {
			
			return _context.Doctors.Include(x=>x.Department).ToList();
        }

        public void Update(DoctorsVm newdoctor)
        {
            var olddoctor= _context.Doctors.Include(x=>x.Department).FirstOrDefault(x=>x.Id==newdoctor.Id);
            olddoctor.Id= newdoctor.Id; 
            olddoctor.FullName= newdoctor.FullName;
            olddoctor.Details= newdoctor.Details;
            olddoctor.Date= newdoctor.Date;
            olddoctor.DepartmentId= newdoctor.DepartmentId;
            olddoctor.Department = newdoctor.Department;
            olddoctor.ImgUrl= newdoctor.ImgUrl;
            olddoctor.Payment = newdoctor.Payment;
            olddoctor.Phone = newdoctor.Phone;
            _context.SaveChanges();
        }
    }
}
