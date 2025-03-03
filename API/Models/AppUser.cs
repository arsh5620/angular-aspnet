﻿namespace API.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];

        public required string Gender { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string KnownAs { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime LastActive { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }

        public string? Introduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Interests { get; set; }

        public List<Photo> Photos { get; set; } = [];

        public int GetAge()
        {
            return DateTime.UtcNow.Year - DateOfBirth.Year;
        }
    }
}
