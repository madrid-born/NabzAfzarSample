using NabzAfzarSample.Models;

namespace NabzAfzarSample.ViewModels.Shop;

public class ShopIndexVm
{
    public List<Product> Products { get; set; } = new();
    public List<Category> Categories { get; set; } = new();

    public string? Q { get; set; }
    public int? CategoryId { get; set; }
    public string Sort { get; set; } = "new";

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 9;
    public int TotalCount { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}