using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace quejapp.Models;

[Index("Email", IsUnique = true)]
public class AppUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int User_ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ProfilePicAddress { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    [JsonIgnore]
    public ICollection<Comment> Comments { get; } = new List<Comment>();
    [JsonIgnore]
    public ICollection<Queja> Complaints { get; } = new List<Queja>();
}
