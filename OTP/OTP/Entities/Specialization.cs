using System;

namespace OTP.Entities
{
    public class Specialization
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;

        public static Specialization CreateEntity(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название специализации не может быть пустым.", nameof(name));

            return new Specialization
            {
                Id = id,
                Name = name ?? string.Empty
            };
        }
    }
}