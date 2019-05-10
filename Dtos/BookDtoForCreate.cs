using System.ComponentModel.DataAnnotations;
using angu.models;

namespace angu.Dtos
{
    public class BookDtoForCreate
    {
        [Required]
        public string BookName { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Writer { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public int Price { get; set; }  

        public User User { get; set; }
        public int UserId { get; set; }
    }
}