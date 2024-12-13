﻿namespace ExpenseTracker.Core.Entities
{
    public class ContactEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty ;
        public string Message { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; }
    }
}
