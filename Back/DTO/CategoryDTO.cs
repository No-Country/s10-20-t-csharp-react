using System.ComponentModel.DataAnnotations;

namespace quejapp.DTO
{
    public class CategoryDTO
    {
        [Required]
        public int Category_ID { get; set; }
        public string Name { get; set; } = null!;
    }
}
