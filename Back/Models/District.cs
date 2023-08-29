using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quejapp.Models;

public class District
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int District_ID { get; set; }
    public string Name { get; set; } = string.Empty;
    [ForeignKey(name: "Locality")]
    public int Locality_ID { get; set; }
    public Locality Locality { get; set; } = null!;
}
