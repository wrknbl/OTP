using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OTP.Entities;

namespace OTP.Repositories
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> ReadPatients();
        Patient ReadPatientById(int id);
        void CreatePatient(Patient patient);
        void UpdatePatient(Patient patient);
        void DeletePatient(int id);
    }
}
