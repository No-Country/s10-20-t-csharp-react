using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace s10.Back.DTO
{
    public class QuejaPostDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;
        [FromForm(Name = "media[]")]
        public IFormFile media { get; set; } = null!;
        //public int District_ID { get; set; }
        public Location? Location { get; set; }
       
        [Required]
        public int Category_ID { get; set; }
        public bool IsAnonymous { get; set; }
        //public int User_ID { get; set; }
    }

    public class QuejaPostDTO2
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;

        [FromForm(Name = "media[]")]
        public IFormFileCollection? media { get; set; } = null!;
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
