using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quejapp.Models;

public class Locality
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Locality_ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<District> Districts { get; } = new List<District>();
}
