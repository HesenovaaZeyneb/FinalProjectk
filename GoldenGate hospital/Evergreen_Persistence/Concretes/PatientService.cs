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
	public class PatientService : IPatientService
	{
		AppDbContext _context;
        public PatientService(AppDbContext context)
        {
            _context = context;
        }
        public void Create(PatientVm patientVm)
		{
			Patient patient = new Patient()
			{
				FullName = patientVm.FullName,
				Adress = patientVm.Adress,
				Phone = patientVm.Phone,
				ImgUrl= patientVm.ImgUrl,
				Email = patientVm.Email,
				BirthDay = patientVm.BirthDay,
				Age = patientVm.Age,
				Height = patientVm.Height,
			    Weight=patientVm.Weight,
				LastVisit=patientVm.LastVisit,
			};
			_context.Patients.Add(patient);
			_context.SaveChanges();


		}

		public void Delete(int id)
		{
			var patient= _context.Patients.FirstOrDefault(p => p.Id == id);
			_context.Patients.Remove(patient);
			_context.SaveChanges();
		}

		public PatientVm Get(int id)
		{
			var patient=_context.Patients.FirstOrDefault(_p => _p.Id == id);
			PatientVm patientVm = new PatientVm()
			{
				Id=patient.Id,
				FullName = patient.FullName,
				Adress = patient.Adress,
				Phone = patient.Phone,
				ImgUrl = patient.ImgUrl,
				Email = patient.Email,
				BirthDay = patient.BirthDay,
				Age = patient.Age,
				Height = patient.Height,
				Weight = patient.Weight,
				LastVisit= patient.LastVisit,
			};
			return patientVm;
		}

		public List<Patient> GetAll()
		{
			return _context.Patients.ToList();
		}

		public void Update(PatientVm newpatient)
		{
			var oldpatient = _context.Patients.FirstOrDefault(x=>x.Id == newpatient.Id);
			oldpatient.FullName= newpatient.FullName;
			oldpatient.Adress= newpatient.Adress;
			oldpatient.Phone= newpatient.Phone;
			oldpatient.BirthDay= newpatient.BirthDay;
			oldpatient.Age= newpatient.Age;
			oldpatient.Weight= newpatient.Weight;
			oldpatient.Height= newpatient.Height;
			oldpatient.ImgUrl= newpatient.ImgUrl;
			oldpatient.Email= newpatient.Email;
			oldpatient.LastVisit= newpatient.LastVisit;
			_context.SaveChanges();
		}
	}
}
