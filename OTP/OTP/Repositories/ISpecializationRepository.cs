using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OTP.Entities;

namespace OTP.Repositories
{
    public interface ISpecializationRepository
    {
        IEnumerable<Specialization> ReadSpecializations();
        Specialization ReadSpecializationById(int id);
        void CreateSpecialization(Specialization newSpecialization);
        void UpdateSpecialization(Specialization newSpecialization);
        void DeleteSpecialization(int id);
    }
}
