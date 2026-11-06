using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OTP.Entities;

namespace OTP.Repositories
{
    public interface IDoctorSpecializationRepository
    {
        IEnumerable<DoctorSpecialization> ReadLinks();
        void CreateLink(DoctorSpecialization link);
        void DeleteLink(int doctorId, int specializationId);
    }
}
