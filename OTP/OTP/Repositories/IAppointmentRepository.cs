using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OTP.Entities;

namespace OTP.Repositories
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> ReadAppointments();
        void CreateAppointment(Appointment appointment);
    }
}
