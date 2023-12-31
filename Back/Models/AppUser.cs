﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace quejapp.Models;

[Index("Email", IsUnique = true)]
public class AppUser :IdentityUser
{
    
    public string Address { get; set; } = string.Empty;
    public string ProfilePicAddress { get; set; } = string.Empty;
    [JsonIgnore]
    public string Name { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public ICollection<Comment> Comments { get; } = new List<Comment>();
    public ICollection<Queja> Complaints { get; } = new List<Queja>();
}
