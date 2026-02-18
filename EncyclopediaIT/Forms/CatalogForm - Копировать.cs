using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncyclopediaIT.Models;
using System.Data.Entity;

namespace EncyclopediaIT.Forms
{
    public partial class CatalogForm : Form
    {
        public CatalogForm()
        {
            InitializeComponent();
            LoadCategories();
            LoadTechnologies();
        }

        private void LoadCategories()
        {
            using (var db = new AppDbContext())
            {
                var categories = db.Categories.ToList();
                categoryComboBox.DataSource = categories;
                categoryComboBox.DisplayMember = "Name";
                categoryComboBox.ValueMember = "Id";
                categoryComboBox.SelectedIndex = -1;
            }
        }

        private void LoadTechnologies()
        {
            using (var db = new AppDbContext())
            {
                var query = db.Technologies.Include(t => t.Category).AsQueryable();

                // Фильтр по категории
                if (categoryComboBox.SelectedValue != null &&
                    int.TryParse(categoryComboBox.SelectedValue.ToString(), out int categoryId))
                {
                    query = query.Where(t => t.CategoryId == categoryId);
                }

                // Поиск
                if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
                {
                    string searchText = searchTextBox.Text.ToLower();
                    query = query.Where(t => t.Name.ToLower().Contains(searchText) ||
                                         t.Description.ToLower().Contains(searchText));
                    query.ToList();
                }

                technologiesDataGridView.DataSource = query
                    .Select(t => new
                    {
                        t.Id,
                        t.Name,
                        Category = t.Category.Name,
                        t.ViewCount,
                        Rating = db.Comments
                            .Where(c => c.TechnologyId == t.Id && c.IsApproved)
                            .Average(c => (double?)c.Rating) ?? 0
                    })
                    .ToList();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            LoadTechnologies();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            categoryComboBox.SelectedIndex = -1;
            searchTextBox.Clear();
            LoadTechnologies();
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            if (technologiesDataGridView.SelectedRows.Count == 0) return;

            int techId = (int)technologiesDataGridView.SelectedRows[0].Cells["Id"].Value;
            var techCard = new TechnologyCardForm(techId);
            techCard.ShowDialog();
            LoadTechnologies(); // Обновляем счетчик просмотров
        }

        private void addBookmarkButton_Click(object sender, EventArgs e)
        {
            if (!CurrentUser.IsAuthenticated)
            {
                MessageBox.Show("Для добавления в закладки необходимо войти в систему",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (technologiesDataGridView.SelectedRows.Count == 0) return;

            int techId = (int)technologiesDataGridView.SelectedRows[0].Cells["Id"].Value;

            using (var db = new AppDbContext())
            {
                bool exists = db.Bookmarks.Any(b => b.UserId == CurrentUser.Id && b.TechnologyId == techId);

                if (exists)
                {
                    MessageBox.Show("Эта технология уже в ваших закладках",
                                  "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var bookmark = new Bookmark
                {
                    UserId = CurrentUser.Id,
                    TechnologyId = techId,
                    DateAdded = DateTime.Now
                };

                db.Bookmarks.Add(bookmark);
                db.SaveChanges();

                MessageBox.Show("Технология добавлена в закладки",
                              "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void searchTextBox_Enter(object sender, EventArgs e)
        {
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
        }
    }
}
