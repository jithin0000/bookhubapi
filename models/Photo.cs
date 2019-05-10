using System;

namespace angu.models
{
    public class Photo
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public DateTime UploadedOn { get; set; }
        public bool isMain { get; set; }

        public User user { get; set; }
        public long UserId { get; set; }
    }
}