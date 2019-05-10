using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using angu.models;

namespace angu.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }   

        [Required]  
        public string Password { get; set; }    

         public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}