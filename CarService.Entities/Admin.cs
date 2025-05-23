﻿namespace CarService.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        #pragma warning disable CS8618
        public string Username { get; set; }
        #pragma warning restore CS8618
        public bool IsOnline { get; set; } = false;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
