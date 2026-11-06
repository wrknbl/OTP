using System;

namespace OTP.Entities
{
    public class Diagnosis
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;

        public static Diagnosis CreateEntity(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название диагноза не может быть пустым.", nameof(name));

            return new Diagnosis
            {
                Id = id,
                Name = name ?? string.Empty
            };
        }
    }
}