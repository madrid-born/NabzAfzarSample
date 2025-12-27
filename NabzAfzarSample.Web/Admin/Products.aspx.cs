using System;
using System.Linq;
using System.Web.UI.WebControls;
using NabzAfzarSample.App_DataAccess;
using NabzAfzarSample.Models;

namespace NabzAfzarSample.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        // Rider: declare controls manually
        protected HiddenField ProductIdHidden;
        protected TextBox NameTextBox;
        protected TextBox DescTextBox;
        protected DropDownList CategoryDropDown;
        protected TextBox PriceTextBox;
        protected TextBox StockTextBox;
        protected CheckBox IsActiveCheckBox;
        protected Button SaveButton;
        protected Button ClearButton;
        protected Literal MessageLiteral;
        protected GridView ProductsGrid;
        protected TextBox PrimaryImageUrlTextBox;


        private readonly AppDbContext _db = new AppDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
                BindGrid();
            }
        }

        private void BindCategories()
        {
            var categories = _db.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToList();

            CategoryDropDown.DataSource = categories;
            CategoryDropDown.DataTextField = "Name";
            CategoryDropDown.DataValueField = "Id";
            CategoryDropDown.DataBind();
        }

        private void BindGrid()
        {
            var products = _db.Products
                .OrderByDescending(p => p.Id)
                .ToList()
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    CategoryName = _db.Categories.Where(c => c.Id == p.CategoryId).Select(c => c.Name).FirstOrDefault(),
                    p.Price,
                    p.StockQuantity,
                    p.IsActive
                })
                .ToList();

            ProductsGrid.DataSource = products;
            ProductsGrid.DataBind();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            var name = (NameTextBox.Text ?? "").Trim();
            var primaryImageUrl = (PrimaryImageUrlTextBox.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageLiteral.Text = "Name is required.";
                return;
            }

            if (!int.TryParse(CategoryDropDown.SelectedValue, out var categoryId))
            {
                MessageLiteral.Text = "Category is required.";
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out var price) || price < 0)
            {
                MessageLiteral.Text = "Invalid price.";
                return;
            }

            if (!int.TryParse(StockTextBox.Text, out var stock) || stock < 0)
            {
                MessageLiteral.Text = "Invalid stock quantity.";
                return;
            }

            int id;
            var isEdit = int.TryParse(ProductIdHidden.Value, out id) && id > 0;

            if (isEdit)
            {
                var product = _db.Products.Find(id);
                if (product == null)
                {
                    MessageLiteral.Text = "Product not found.";
                    return;
                }

                product.Name = name;
                product.Description = DescTextBox.Text;
                product.CategoryId = categoryId;
                product.Price = price;
                product.StockQuantity = stock;
                product.IsActive = IsActiveCheckBox.Checked;

                var existingPrimary = _db.ProductImages
                    .FirstOrDefault(i => i.ProductId == product.Id && i.IsPrimary);

                if (string.IsNullOrWhiteSpace(primaryImageUrl))
                {
                    if (existingPrimary != null)
                        _db.ProductImages.Remove(existingPrimary);
                }
                else
                {
                    if (existingPrimary == null)
                    {
                        _db.ProductImages.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            ImageUrl = primaryImageUrl,
                            IsPrimary = true
                        });
                    }
                    else
                    {
                        existingPrimary.ImageUrl = primaryImageUrl;
                    }
                }
            }
            else
            {
                var product = new Product
                {
                    Name = name,
                    Description = DescTextBox.Text,
                    CategoryId = categoryId,
                    Price = price,
                    StockQuantity = stock,
                    IsActive = IsActiveCheckBox.Checked,
                    CreatedAt = DateTime.UtcNow
                };

                _db.Products.Add(product);
                
                if (!string.IsNullOrWhiteSpace(primaryImageUrl))
                {
                    product.Images.Add(new ProductImage
                    {
                        ImageUrl = primaryImageUrl,
                        IsPrimary = true
                    });
                }
            }
            
            _db.SaveChanges();

            MessageLiteral.Text = isEdit ? "Product updated." : "Product created.";
            ClearForm();
            BindGrid();
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            ClearForm();
            MessageLiteral.Text = "";
        }

        private void ClearForm()
        {
            ProductIdHidden.Value = "";
            NameTextBox.Text = "";
            DescTextBox.Text = "";
            PriceTextBox.Text = "";
            StockTextBox.Text = "";
            PrimaryImageUrlTextBox.Text = "";
            IsActiveCheckBox.Checked = true;

            if (CategoryDropDown.Items.Count > 0)
                CategoryDropDown.SelectedIndex = 0;
        }

        protected void ProductsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                var product = _db.Products.Find(id);
                if (product == null) return;
                var primary = _db.ProductImages.FirstOrDefault(i => i.ProductId == product.Id && i.IsPrimary);

                ProductIdHidden.Value = product.Id.ToString();
                NameTextBox.Text = product.Name;
                DescTextBox.Text = product.Description;
                PriceTextBox.Text = product.Price.ToString();
                StockTextBox.Text = product.StockQuantity.ToString();
                IsActiveCheckBox.Checked = product.IsActive;
                CategoryDropDown.SelectedValue = product.CategoryId.ToString();
                PrimaryImageUrlTextBox.Text = primary?.ImageUrl ?? "";

                MessageLiteral.Text = "Editing product...";
            }
            else if (e.CommandName == "DeleteRow")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                var product = _db.Products.Find(id);
                if (product == null) return;

                // Optional: prevent delete if used in orders
                var usedInOrders = _db.OrderItems.Any(i => i.ProductId == id);
                if (usedInOrders)
                {
                    MessageLiteral.Text = "Cannot delete product that exists in past orders.";
                    return;
                }

                _db.Products.Remove(product);
                _db.SaveChanges();

                MessageLiteral.Text = "Product deleted.";
                ClearForm();
                BindGrid();
            }
        }
    }
}
