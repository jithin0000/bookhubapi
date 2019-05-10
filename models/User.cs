using System;
using System.Collections.Generic;

namespace angu.models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}