using System;
using System.ComponentModel.DataAnnotations;

namespace NabzAfzarSample.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}