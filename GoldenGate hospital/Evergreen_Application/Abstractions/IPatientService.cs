using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain.ViewModel;
using Evergreen_Domain;

namespace Evergreen_Application.Abstractions
{
	public interface IPatientService
	{
		List<Patient> GetAll();
		void Create(PatientVm patientVm);
		void Update(PatientVm newpatient);
		void Delete(int id);
		PatientVm Get(int id);
	}
}
