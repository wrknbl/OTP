using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OTP.Entities;

namespace OTP.Repositories
{
    public interface IDiagnosisRepository
    {
        IEnumerable<Diagnosis> ReadDiagnoses();
        Diagnosis ReadDiagnosisById(int id);
        void CreateDiagnosis(Diagnosis diagnosis);
        void DeleteDiagnosis(int id);
        void UpdateDiagnosis(Diagnosis diagnosis);
    }
}
