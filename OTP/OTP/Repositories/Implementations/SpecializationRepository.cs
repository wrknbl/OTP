using OTP.Entities;
using OTP.Repositories;
using System.Collections.Generic;

namespace OTP.Repositories.Implementations
{
    public class SpecializationRepository : ISpecializationRepository
    {
        public IEnumerable<Specialization> ReadSpecializations() => new List<Specialization>();

        public Specialization ReadSpecializationById(int id) => Specialization.CreateEntity(0, "Тестовая специализация");

        public void CreateSpecialization(Specialization specialization) { }
        public void UpdateSpecialization(Specialization specialization) { }
        public void DeleteSpecialization(int id) { }
    }
}