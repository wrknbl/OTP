using System;

namespace OTP.Entities.Enums
{
    [Flags]
    public enum AppointmentServices
    {
        None = 0,
        BloodTest = 1,   
        XRay = 2,         
        ECG = 4,          
        Massage = 8      
    }
}