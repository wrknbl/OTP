using OTP.Entities;
using OTP.Repositories;
using System.Collections.Generic;

namespace OTP.Repositories.Implementations
{
    public class PatientRepository : IPatientRepository
    {
        public IEnumerable<Patient> ReadPatients() => new List<Patient>();

        public Patient ReadPatientById(int id) => Patient.CreateEntity(0, "Тестовый пациент", "111-11-11", 1);

        public void CreatePatient(Patient patient) { }
        public void UpdatePatient(Patient patient) { }
        public void DeletePatient(int id) { }
    }
}