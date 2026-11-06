using OTP.Entities;
using OTP.Repositories;
using System.Collections.Generic;

namespace OTP.Repositories.Implementations
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public IEnumerable<Appointment> ReadAppointments() => new List<Appointment>();

        public void CreateAppointment(Appointment appointment) { }
    }
}