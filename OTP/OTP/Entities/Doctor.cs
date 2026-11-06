using System;

namespace OTP.Entities
{
    public class Doctor
    {
        public int Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;

        public static Doctor CreateEntity(int id, string fullName, string phone)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("ФИО врача не может быть пустым.", nameof(fullName));

            return new Doctor
            {
                Id = id,
                FullName = fullName ?? string.Empty,
                Phone = phone ?? string.Empty
            };
        }
    }
}