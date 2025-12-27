using System;
using System.Linq;
using System.Web.UI.WebControls;
using NabzAfzarSample.App_DataAccess;
using NabzAfzarSample.Models;

namespace NabzAfzarSample.Admin
{
    public partial class Categories : System.Web.UI.Page
    {
        // Rider: declare controls manually
        protected HiddenField CategoryIdHidden;
        protected TextBox NameTextBox;
        protected TextBox DescTextBox;
        protected CheckBox IsActiveCheckBox;
        protected Button SaveButton;
        protected Button ClearButton;
        protected Literal MessageLiteral;
        protected GridView CategoriesGrid;

        private readonly AppDbContext _db = new AppDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid();
        }

        private void BindGrid()
        {
            CategoriesGrid.DataSource = _db.Categories
                .OrderByDescending(c => c.Id)
                .ToList();
            CategoriesGrid.DataBind();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            var name = (NameTextBox.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageLiteral.Text = "Name is required.";
                return;
            }

            int id;
            var isEdit = int.TryParse(CategoryIdHidden.Value, out id) && id > 0;

            if (isEdit)
            {
                var category = _db.Categories.Find(id);
                if (category == null)
                {
                    MessageLiteral.Text = "Category not found.";
                    return;
                }

                category.Name = name;
                category.Description = DescTextBox.Text;
                category.IsActive = IsActiveCheckBox.Checked;
            }
            else
            {
                var category = new Category
                {
                    Name = name,
                    Description = DescTextBox.Text,
                    IsActive = IsActiveCheckBox.Checked,
                    CreatedAt = DateTime.UtcNow
                };
                _db.Categories.Add(category);
            }

            _db.SaveChanges();

            MessageLiteral.Text = isEdit ? "Category updated." : "Category created.";
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
            CategoryIdHidden.Value = "";
            NameTextBox.Text = "";
            DescTextBox.Text = "";
            IsActiveCheckBox.Checked = true;
        }

        protected void CategoriesGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                var category = _db.Categories.Find(id);
                if (category == null) return;

                CategoryIdHidden.Value = category.Id.ToString();
                NameTextBox.Text = category.Name;
                DescTextBox.Text = category.Description;
                IsActiveCheckBox.Checked = category.IsActive;

                MessageLiteral.Text = "Editing category...";
            }
            else if (e.CommandName == "DeleteRow")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                var category = _db.Categories.Find(id);
                if (category == null) return;

                // Optional safety: prevent delete if category has products
                var hasProducts = _db.Products.Any(p => p.CategoryId == id);
                if (hasProducts)
                {
                    MessageLiteral.Text = "Cannot delete category that contains products.";
                    return;
                }

                _db.Categories.Remove(category);
                _db.SaveChanges();

                MessageLiteral.Text = "Category deleted.";
                ClearForm();
                BindGrid();
            }
        }
    }
}
