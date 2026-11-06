namespace OTP.Entities
{
    public class DoctorSpecialization
    {
        public int DoctorId { get; private set; }
        public int SpecializationId { get; private set; }

        public static DoctorSpecialization CreateLink(int doctorId, int specializationId)
        {
            return new DoctorSpecialization
            {
                DoctorId = doctorId,
                SpecializationId = specializationId
            };
        }
    }
}