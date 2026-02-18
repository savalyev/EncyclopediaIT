using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EncyclopediaIT.Models;
using Guna.UI2.WinForms;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EncyclopediaIT.Forms
{
    public partial class CatalogForm : Form
    {
        private Guna2Panel mainPanel;
        private Guna2Panel headerPanel;
        private Guna2Panel sidebarPanel;
        private Guna2Panel contentPanel;
        private Guna2TextBox searchTextBox;
        private FlowLayoutPanel cardsFlowPanel;
        private bool isDragging;
        private Point lastCursorPos;
        private string currentFilter = "Все";
        private Dictionary<string, Category> categoryFilters;

        public CatalogForm()
        {
            InitializeComponent();
            InitializeUI();
            LoadData();
            this.DoubleBuffered = true;
        }

        private void InitializeUI()
        {
            // Настройка основной формы
            this.Text = "Каталог технологий";
            this.ClientSize = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(18, 18, 18);

            // Основная панель
            mainPanel = new Guna2Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.Transparent;

            // Хедер с возможностью перемещения
            headerPanel = new Guna2Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 75;
            headerPanel.FillColor = Color.FromArgb(30, 30, 30);
            headerPanel.BorderColor = Color.FromArgb(51, 51, 51);
            headerPanel.BorderThickness = 1;

            // Обработчики перемещения
            headerPanel.MouseDown += (s, e) =>
            {
                isDragging = true;
                lastCursorPos = e.Location;
                Cursor = Cursors.SizeAll;
            };

            headerPanel.MouseMove += HeaderPanel_MouseMove;

            headerPanel.MouseUp += (s, e) =>
            {
                isDragging = false;
                Cursor = Cursors.Default;
            };

            // Заголовок
            var titleLabel = new Guna2HtmlLabel();
            titleLabel.Text = "<span style='color:#00d8ff; font-size:24px; font-weight:300'>КАТАЛОГ <span style='font-weight:700'>ТЕХНОЛОГИЙ</span></span>";
            titleLabel.Location = new Point(20, 20);
            titleLabel.AutoSize = true;

            // Кнопка закрытия
            var closeButton = new Guna2Button()
            {
                Text = "×",
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.White,
                FillColor = Color.Transparent,
                BorderColor = Color.Transparent,
                Size = new Size(40, 40),
                Location = new Point(1150, 10),
                Animated = true,
                Cursor = Cursors.Hand
            };
            closeButton.Click += (s, e) => this.Close();

            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(closeButton);

            // Боковая панель с фильтрами
            sidebarPanel = new Guna2Panel();
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Width = 300;
            sidebarPanel.BackColor = Color.FromArgb(30, 30, 30);
            sidebarPanel.BorderColor = Color.FromArgb(51, 51, 51);
            sidebarPanel.BorderThickness = 1;
            sidebarPanel.Padding = new Padding(20);

            // Поиск
            searchTextBox = new Guna2TextBox();
            searchTextBox.PlaceholderText = "Поиск...";
            searchTextBox.Size = new Size(260, 40);
            searchTextBox.BorderRadius = 6;
            searchTextBox.FillColor = Color.FromArgb(34, 34, 34);
            searchTextBox.BorderColor = Color.FromArgb(68, 68, 68);
            searchTextBox.ForeColor = Color.White;
            //searchTextBox.Margin = new Padding(0, 0, 0, 20);
            searchTextBox.Location = new Point(15, 15);
            searchTextBox.TextChanged += (s, e) => LoadTechnologies();

            // Заголовок фильтров
            var filterLabel = new Guna2HtmlLabel();
            filterLabel.Text = "<span style='color:#00d8ff; font-size:14px; font-weight:bold'>ФИЛЬТРЫ</span>";
            //filterLabel.Margin = new Padding(0, 0, 0, 15);
            filterLabel.Location = new Point(15, 65);
            filterLabel.AutoSize = true;

            sidebarPanel.Controls.Add(searchTextBox);
            sidebarPanel.Controls.Add(filterLabel);

            // Основная область контента
            contentPanel = new Guna2Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.FromArgb(18, 18, 18);
            contentPanel.AutoScroll = true;
            contentPanel.Padding = new Padding(20);

            cardsFlowPanel = new FlowLayoutPanel();
            cardsFlowPanel.Dock = DockStyle.Fill;
            cardsFlowPanel.AutoScroll = true;
            cardsFlowPanel.WrapContents = true;
            cardsFlowPanel.BackColor = Color.Transparent;

            contentPanel.Controls.Add(cardsFlowPanel);

            // Собираем интерфейс
            mainPanel.Controls.Add(contentPanel);
            mainPanel.Controls.Add(sidebarPanel);
            mainPanel.Controls.Add(headerPanel);
            this.Controls.Add(mainPanel);

            // Тень для формы
            new Guna2ShadowForm(this);
        }

        private void LoadData()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    categoryFilters = db.Categories.ToDictionary(c => c.Name, c => c);
                    CreateFilterButtons();
                    LoadTechnologies();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateFilterButtons()
        {
            // Кнопка "Все технологии"
            var allTechButton = new Guna2Button();
            allTechButton.Text = "Все технологии";
            allTechButton.Size = new Size(260, 40);
            allTechButton.Margin = new Padding(0, 0, 0, 10);
            allTechButton.BorderRadius = 6;
            allTechButton.TextAlign = HorizontalAlignment.Left;
            allTechButton.Padding = new Padding(10, 0, 0, 0);
            allTechButton.ForeColor = Color.White;
            allTechButton.FillColor = Color.FromArgb(0, 216, 255, 20);
            allTechButton.BorderColor = Color.FromArgb(0, 216, 255);
            allTechButton.BorderThickness = 2;
            allTechButton.Location = new Point(15, 115);
            allTechButton.Click += (s, e) => ApplyFilter("Все");

            sidebarPanel.Controls.Add(allTechButton);

            // Кнопки для каждой категории
            int i = 165;
            foreach (var category in categoryFilters.Values)
            {
                var categoryButton = new Guna2Button();
                categoryButton.Text = category.Name;
                categoryButton.Size = new Size(260, 40);
                categoryButton.Margin = new Padding(0, 0, 0, 10);
                categoryButton.BorderRadius = 6;
                categoryButton.TextAlign = HorizontalAlignment.Left;
                categoryButton.Padding = new Padding(10, 0, 0, 0);
                categoryButton.ForeColor = Color.White;
                categoryButton.FillColor = Color.Transparent;
                categoryButton.BorderColor = Color.FromArgb(68, 68, 68);
                categoryButton.BorderThickness = 1;
                categoryButton.Tag = category.Id;
                categoryButton.Location = new Point(15, i);
                i += 50;
                categoryButton.Click += (s, e) => ApplyFilter(category.Name);

                sidebarPanel.Controls.Add(categoryButton);
            }
        }

        private void ApplyFilter(string filterName)
        {
            // Сбрасываем стиль у всех кнопок
            foreach (var control in sidebarPanel.Controls)
            {
                if (control is Guna2Button button)
                {
                    button.FillColor = Color.Transparent;
                    button.BorderColor = Color.FromArgb(68, 68, 68);
                    button.BorderThickness = 1;
                }
            }

            // Устанавливаем стиль для активной кнопки
            foreach (var control in sidebarPanel.Controls)
            {
                if (control is Guna2Button button && button.Text == filterName)
                {
                    button.FillColor = Color.FromArgb(0, 216, 255, 20);
                    button.BorderColor = Color.FromArgb(0, 216, 255);
                    button.BorderThickness = 2;
                    break;
                }
            }

            currentFilter = filterName;
            LoadTechnologies();
        }

        private void HeaderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int xDiff = e.X - lastCursorPos.X;
                int yDiff = e.Y - lastCursorPos.Y;

                this.Location = new Point(
                    this.Location.X + xDiff,
                    this.Location.Y + yDiff);
            }
        }

        private void LoadTechnologies()
        {
            cardsFlowPanel.Controls.Clear();
            cardsFlowPanel.SuspendLayout();

            try
            {
                using (var db = new AppDbContext())
                {
                    IQueryable<Technology> query = db.Technologies
                        .Include(t => t.Category)
                        .Include(t => t.Comments);

                    if (currentFilter != "Все")
                    {
                        if (categoryFilters.TryGetValue(currentFilter, out Category category))
                        {
                            query = query.Where(t => t.CategoryId == category.Id);
                        }
                    }

                    // Применяем поиск
                    if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
                    {
                        string searchText = searchTextBox.Text.ToLower();
                        query = query.Where(t =>
                            t.Name.ToLower().Contains(searchText) ||
                            t.Description.ToLower().Contains(searchText));
                    }

                    // Получаем технологии
                    var technologies = query.ToList();

                    // Создаем карточки
                    foreach (var tech in technologies)
                    {
                        var card = CreateTechnologyCard(tech);
                        cardsFlowPanel.Controls.Add(card);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки технологий: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cardsFlowPanel.ResumeLayout();
            }
        }

        private Guna2Panel CreateTechnologyCard(Technology tech)
        {
            var card = new Guna2Panel();
            card.Size = new Size(300, 250);
            card.Margin = new Padding(15);
            card.BorderRadius = 10;
            card.BorderColor = Color.FromArgb(51, 51, 51);
            card.BorderThickness = 1;
            card.FillColor = Color.FromArgb(30, 30, 30);
            card.Cursor = Cursors.Hand;
            card.Tag = tech.Id;

            // Рассчитываем рейтинг безопасно
            double rating = 0;
            using (var db = new AppDbContext())
            {
                var approvedComments = db.Comments
                    .Where(c => c.TechnologyId == tech.Id && c.IsApproved)
                    .ToList();

                if (approvedComments.Any())
                {
                    rating = approvedComments.Average(c => c.Rating);
                }
            }

            // Анимация при наведении
            card.MouseEnter += (s, e) =>
            {
                card.FillColor = Color.FromArgb(40, 40, 40);
                card.BorderColor = Color.FromArgb(0, 216, 255);
            };

            card.MouseLeave += (s, e) =>
            {
                card.FillColor = Color.FromArgb(30, 30, 30);
                card.BorderColor = Color.FromArgb(51, 51, 51);
            };

            // Верхняя часть с градиентом
            var headerPanel = new Guna2Panel();
            headerPanel.Size = new Size(300, 100);
            headerPanel.BorderRadius = 10;
            headerPanel.BorderThickness = 0;
            headerPanel.Paint += (s, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(58, 12, 163),
                    Color.FromArgb(76, 201, 240),
                    135f))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }
            };

            // Иконка (первые две буквы)
            var iconLabel = new Label();
            iconLabel.Text = tech.Name.Length > 1 ?
                tech.Name.Substring(0, 2).ToUpper() : tech.Name.ToUpper();
            iconLabel.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            iconLabel.ForeColor = Color.White;
            iconLabel.AutoSize = false;
            iconLabel.Size = new Size(60, 60);
            iconLabel.TextAlign = ContentAlignment.MiddleCenter;
            iconLabel.Location = new Point(120, 20);

            headerPanel.Controls.Add(iconLabel);

            // Основное содержимое карточки
            var contentPanel = new Guna2Panel();
            contentPanel.Size = new Size(300, 150);
            contentPanel.Location = new Point(0, 100);
            contentPanel.BackColor = Color.Transparent;
            contentPanel.Padding = new Padding(15);

            var nameLabel = new Label();
            nameLabel.Text = tech.Name;
            nameLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            nameLabel.ForeColor = Color.FromArgb(0, 216, 255);
            nameLabel.AutoSize = false;
            nameLabel.Size = new Size(270, 30);

            var descLabel = new Label();
            descLabel.Text = tech.Description.Length > 100 ?
                tech.Description.Substring(0, 100) + "..." : tech.Description;
            descLabel.Font = new Font("Segoe UI", 9);
            descLabel.ForeColor = Color.FromArgb(170, 170, 170);
            descLabel.AutoSize = false;
            descLabel.Size = new Size(270, 40);
            descLabel.Location = new Point(0, 35);

            var footerPanel = new Panel();
            footerPanel.Size = new Size(270, 30);
            footerPanel.Location = new Point(0, 85);
            footerPanel.BackColor = Color.Transparent;

            var categoryLabel = new Label();
            categoryLabel.Text = tech.Category?.Name ?? "Без категории";
            categoryLabel.Font = new Font("Segoe UI", 9);
            categoryLabel.ForeColor = Color.FromArgb(170, 170, 170);
            categoryLabel.AutoSize = true;

            var ratingLabel = new Label();
            ratingLabel.Text = GetRatingStars(rating);
            ratingLabel.Font = new Font("Segoe UI", 11);
            ratingLabel.ForeColor = Color.FromArgb(255, 215, 0);
            ratingLabel.AutoSize = true;
            ratingLabel.Location = new Point(170, 0);

            footerPanel.Controls.Add(categoryLabel);
            footerPanel.Controls.Add(ratingLabel);
            contentPanel.Controls.Add(nameLabel);
            contentPanel.Controls.Add(descLabel);
            contentPanel.Controls.Add(footerPanel);

            card.Controls.Add(headerPanel);
            card.Controls.Add(contentPanel);

            card.Click += (s, e) => OpenTechnologyCard(tech.Id);

            foreach (Control control in card.Controls)
            {
                control.Click += (s, e) => OpenTechnologyCard(tech.Id);
                control.Cursor = Cursors.Hand;
            }

            return card;
        }

        private void OpenTechnologyCard(int techId)
        {
            try
            {
                var techCard = new TechnologyCardForm(techId);
                if (techCard.ShowDialog() == DialogResult.OK)
                {
                    LoadTechnologies(); // Обновляем список если нужно
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть карточку: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string GetRatingStars(double rating)
        {
            if (rating <= 0) return "☆☆☆☆☆";

            int fullStars = (int)rating;
            bool hasHalfStar = rating - fullStars >= 0.5;

            string stars = new string('★', fullStars);
            if (hasHalfStar) stars += "½";
            stars += new string('☆', 5 - fullStars - (hasHalfStar ? 1 : 0));

            return stars;
        }
    }
}