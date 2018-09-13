using System;

namespace MyStore.Core.Domain
{
    public class User
    {
        public AggregateId Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}