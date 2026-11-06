using OTP.Entities;
using OTP.Repositories;
using System.Collections.Generic;

namespace OTP.Repositories.Implementations
{
    public class DoctorSpecializationRepository : IDoctorSpecializationRepository
    {
        public IEnumerable<DoctorSpecialization> ReadLinks() => new List<DoctorSpecialization>();

        public void CreateLink(DoctorSpecialization link) { }
        public void DeleteLink(int doctorId, int specializationId) { }
    }
}