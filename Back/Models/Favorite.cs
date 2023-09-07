using quejapp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s10.Back.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Complaint_ID))]
        public Queja Complaint { get; set; }

        public int Complaint_ID { get; set; }

        public DateTime Favorited { get;set; }
        [Required]
        public string FavoritedBy { get; set; } 

        public bool Enabled { get; set; }
    }
}
