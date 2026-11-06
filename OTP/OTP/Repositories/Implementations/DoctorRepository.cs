using OTP.Entities;
using OTP.Repositories;
using System.Collections.Generic;

namespace OTP.Repositories.Implementations
{
    public class DoctorRepository : IDoctorRepository
    {
        public IEnumerable<Doctor> ReadDoctors()
        {
            return new List<Doctor>();
        }

        public Doctor ReadDoctorById(int id)
        {
            return Doctor.CreateEntity(0, "Тестовый врач", "000-00-00");
        }

        public void CreateDoctor(Doctor doctor)
        {
        }

        public void UpdateDoctor(Doctor doctor)
        {
        }

        public void DeleteDoctor(int id)
        {
        }
    }
}