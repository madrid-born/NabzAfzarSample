using System.ComponentModel.DataAnnotations;

namespace NabzAfzarSample.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public bool IsPrimary { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}