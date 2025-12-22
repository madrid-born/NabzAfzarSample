using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NabzAfzarSample.Models.Favorites;

public class Favorite
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public IdentityUser User { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}