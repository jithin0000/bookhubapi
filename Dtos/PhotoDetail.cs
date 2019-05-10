using System;
using angu.models;

namespace angu.Dtos
{
    public class PhotoDetail
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public DateTime UploadedOn { get; set; }
        public bool isMain { get; set; }

        public User user { get; set; }
    }
}