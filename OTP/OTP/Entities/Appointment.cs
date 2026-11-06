using OTP.Entities.Enums;
using System;

namespace OTP.Entities
{
    public class Appointment
    {
        public int Id { get; private set; }
        public int PatientId { get; private set; }
        public int DoctorId { get; private set; }
        public int DiagnosisId { get; private set; }
        public DateTime AppointmentDate { get; private set; }
        public AppointmentResult Result { get; private set; }
        public AppointmentServices Services { get; private set; }

        public static Appointment CreateOperation(
            int id,
            int patientId,
            int doctorId,
            int diagnosisId,
            AppointmentResult result,
            AppointmentServices services)
        {
            return new Appointment
            {
                Id = id,
                PatientId = patientId,
                DoctorId = doctorId,
                DiagnosisId = diagnosisId,
                AppointmentDate = DateTime.Now,
                Result = result,
                Services = services
            };
        }
    }
}