using System;
using System.Collections.Generic;
using angu.models;

namespace angu.Dtos
{
    public class UserDetail
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }

        public string PhotoUrl { get; set; }
        public ICollection<PhotoDetail> Photos { get; set; }

    }
}