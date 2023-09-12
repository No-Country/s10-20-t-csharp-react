using System.ComponentModel.DataAnnotations;

namespace s10.Back.DTO
{
    public class QuejaPostDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;
        public IFormFile media { get; set; } = null!;
        //public int District_ID { get; set; }
        public Location? Location { get; set; }
       
        [Required]
        public int Category_ID { get; set; }
        public bool IsAnonymous { get; set; }
        //public int User_ID { get; set; }
    }

    public class Location
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

      
    }
}
