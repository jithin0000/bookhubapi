using System.ComponentModel.DataAnnotations;

namespace angu.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }   

        [Required]  
        public string Password { get; set; }    
    }
}