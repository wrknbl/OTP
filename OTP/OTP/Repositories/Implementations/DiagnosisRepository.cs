using OTP.Entities;
using OTP.Repositories;
using System.Collections.Generic;

namespace OTP.Repositories.Implementations
{
    public class DiagnosisRepository : IDiagnosisRepository
    {
        public IEnumerable<Diagnosis> ReadDiagnoses() => new List<Diagnosis>();

        public Diagnosis ReadDiagnosisById(int id) => Diagnosis.CreateEntity(0, "Тестовый диагноз");

        public void CreateDiagnosis(Diagnosis diagnosis) { }
        public void UpdateDiagnosis(Diagnosis diagnosis) { }
        public void DeleteDiagnosis(int id) { }
    }
}