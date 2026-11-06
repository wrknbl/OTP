using System;

namespace OTP.Entities
{
    public class Patient
    {
        public int Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public int DoctorId { get; private set; } 

        public static Patient CreateEntity(int id, string fullName, string phone, int doctorId)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("ФИО пациента не может быть пустым.", nameof(fullName));

            return new Patient
            {
                Id = id,
                FullName = fullName ?? string.Empty,
                Phone = phone ?? string.Empty,
                DoctorId = doctorId
            };
        }
    }
}