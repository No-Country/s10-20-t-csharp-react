using quejapp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace s10.Back.DTO
{
    public class QuejaResponseDTO
    {
        public int Complaint_ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? VideoAddress { get; set; }
        public string? PhotoAdress { get; set; }
        //public int? District_ID { get; set; } // se requiere joints
        public string? District_Name { get; set; }
        //public int? Category_ID { get; set; } // se requiere joints
        public string? Category_Name { get; set; }
        //public int? User_ID { get; set; } // se requiere joints
        public string? UserName { get; set; }
        public string? UserPhoto { get; set; }
        public DateTime CreatedAt { get; set; }       
    }
}
