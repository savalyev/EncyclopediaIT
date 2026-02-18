using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EncyclopediaIT.Models;
using Guna.UI2.WinForms;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using EncyclopediaIT.Properties;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace EncyclopediaIT.Forms
{
    public partial class TechnologyCardForm : Form
    {
        private readonly int _technologyId;
        private Technology _technology;

        // Элементы UI
        private Guna2Panel mainPanel;
        private Guna2Panel headerPanel;
        private Guna2PictureBox logoPictureBox;
        private Guna2HtmlLabel nameLabel;
        private Guna2HtmlLabel categoryLabel;
        private Guna2HtmlLabel ratingLabel;
        private Guna2HtmlLabel viewsLabel;
        private Guna2HtmlLabel descriptionLabel;
        private Guna2Button addBookmarkButton;
        private Guna2Button addCommentButton;
        private Guna2Button closeButton;

        public TechnologyCardForm(int technologyId)
        {
            _technologyId = technologyId;
            InitializeUI();
            LoadTechnologyData();
            UpdateViewCount();
            LoginUserCard();
        }

        private void InitializeUI()
        {
            // Настройка основной формы
            this.Text = "Карточка технологии";
            this.ClientSize = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(10, 10, 20);

            // Основная панель
            mainPanel = new Guna2Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.Transparent;
            mainPanel.BorderRadius = 15;
            mainPanel.FillColor = Color.FromArgb(10, 10, 20);
            mainPanel.BorderColor = Color.FromArgb(74, 222, 128, 50);
            mainPanel.BorderThickness = 1;
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Shadow = new Padding(0, 0, 20, 20);
            mainPanel.ShadowDecoration.Color = Color.FromArgb(50, 74, 222, 128);

            // Хедер с градиентом
            headerPanel = new Guna2Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 120;
            headerPanel.FillColor = Color.Transparent;
            headerPanel.BorderRadius = 15;
            headerPanel.BorderThickness = 0;

            // Градиентный фон хедера
            headerPanel.Paint += (sender, e) =>
            {
                using (var brush = new LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(14, 165, 233),
                    Color.FromArgb(16, 185, 129),
                    0f))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }

                // Декоративный круг
                using (var circleBrush = new SolidBrush(Color.FromArgb(30, 255, 255, 255)))
                {
                    e.Graphics.FillEllipse(circleBrush,
                        headerPanel.Width - 150, -50, 200, 200);
                }
            };

            // Название технологии
            nameLabel = new Guna2HtmlLabel();
            nameLabel.Text = "<span style='color:white; font-size:32px; font-weight:600'>Название</span>";
            nameLabel.AutoSize = false;
            nameLabel.Size = new Size(600, 50);
            nameLabel.Location = new Point(30, 30);
            nameLabel.BackColor = Color.Transparent;

            // Категория
            categoryLabel = new Guna2HtmlLabel();
            categoryLabel.Text = "<span style='background:rgba(0,0,0,0.2); color:white; padding:5px 15px; border-radius:20px; font-size:14px'>Категория</span>";
            categoryLabel.AutoSize = false;
            categoryLabel.Size = new Size(300, 30);
            categoryLabel.Location = new Point(30, 80);
            categoryLabel.BackColor = Color.Transparent;

            // Логотип
            logoPictureBox = new Guna2PictureBox();
            logoPictureBox.Size = new Size(150, 150);
            logoPictureBox.Location = new Point(30, 150);
            logoPictureBox.BorderRadius = 10;
            logoPictureBox.BackColor = Color.Transparent;
            logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            // Рейтинг
            ratingLabel = new Guna2HtmlLabel();
            ratingLabel.Text = "<span style='color:#10b981; font-size:24px'>★★★★☆</span> " +
                              "<span style='background:rgba(16,185,129,0.1); color:#10b981; padding:5px 10px; border-radius:4px; font-size:14px'>4.2/5 (142 оценки)</span>";
            ratingLabel.AutoSize = false;
            ratingLabel.Size = new Size(600, 40);
            ratingLabel.Location = new Point(200, 160);
            ratingLabel.BackColor = Color.Transparent;

            // Описание
            descriptionLabel = new Guna2HtmlLabel();
            descriptionLabel.Text = "<p style='color:#b3b3b3; line-height:1.6'>Описание технологии...</p>";
            descriptionLabel.AutoSize = false;
            descriptionLabel.Size = new Size(600, 100);
            descriptionLabel.Location = new Point(200, 210);
            descriptionLabel.BackColor = Color.Transparent;

            // Панель с полным описанием
            var descPanel = new Guna2Panel();
            descPanel.Size = new Size(840, 200);
            descPanel.Location = new Point(30, 320);
            descPanel.BackColor = Color.FromArgb(17, 17, 26);
            descPanel.BorderRadius = 10;
            descPanel.BorderThickness = 0;

            var descTitle = new Guna2HtmlLabel();
            descTitle.Text = "<span style='color:#0ea5e9; font-size:18px; border-bottom:1px solid #1e293b; padding-bottom:10px'>Описание</span>";
            descTitle.AutoSize = false;
            descTitle.Size = new Size(800, 40);
            descTitle.Location = new Point(20, 10);
            descTitle.BackColor = Color.Transparent;

            var fullDescLabel = new Guna2HtmlLabel();
            fullDescLabel.Text = "<p style='color:#b3b3b3; line-height:1.7'>Полное описание технологии...</p>";
            fullDescLabel.AutoSize = false;
            fullDescLabel.Size = new Size(800, 150);
            fullDescLabel.Location = new Point(20, 50);
            fullDescLabel.BackColor = Color.Transparent;

            descPanel.Controls.Add(descTitle);
            descPanel.Controls.Add(fullDescLabel);

            // Кнопки
            addBookmarkButton = new Guna2Button();
            addBookmarkButton.Text = "Добавить в закладки";
            addBookmarkButton.Size = new Size(400, 45);
            addBookmarkButton.Location = new Point(30, 550);
            addBookmarkButton.BorderRadius = 8;
            addBookmarkButton.FillColor = Color.Transparent;
            addBookmarkButton.ForeColor = Color.FromArgb(14, 165, 233);
            addBookmarkButton.BorderColor = Color.FromArgb(14, 165, 233);
            addBookmarkButton.BorderThickness = 2;
            addBookmarkButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            addBookmarkButton.Click += addBookmarkButton_Click;

            addCommentButton = new Guna2Button();
            addCommentButton.Text = "Оставить комментарий";
            addCommentButton.Size = new Size(400, 45);
            addCommentButton.Location = new Point(470, 550);
            addCommentButton.BorderRadius = 8;
            addCommentButton.FillColor = Color.Transparent;
            addCommentButton.ForeColor = Color.FromArgb(16, 185, 129);
            addCommentButton.BorderColor = Color.FromArgb(16, 185, 129);
            addCommentButton.BorderThickness = 2;
            addCommentButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            addCommentButton.Click += addCommentButton_Click;

            closeButton = new Guna2Button();
            closeButton.Text = "Закрыть";
            closeButton.Size = new Size(150, 40);
            closeButton.Location = new Point(720, 620);
            closeButton.BorderRadius = 8;
            closeButton.FillColor = Color.FromArgb(30, 41, 59);
            closeButton.ForeColor = Color.White;
            closeButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            closeButton.Click += closeButton_Click;

            // Собираем интерфейс
            headerPanel.Controls.Add(nameLabel);
            headerPanel.Controls.Add(categoryLabel);

            mainPanel.Controls.Add(headerPanel);
            mainPanel.Controls.Add(logoPictureBox);
            mainPanel.Controls.Add(ratingLabel);
            mainPanel.Controls.Add(descriptionLabel);
            mainPanel.Controls.Add(descPanel);
            mainPanel.Controls.Add(addBookmarkButton);
            mainPanel.Controls.Add(addCommentButton);
            mainPanel.Controls.Add(closeButton);

            this.Controls.Add(mainPanel);
        }

        private void LoadTechnologyData()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    _technology = db.Technologies
                        .Include(t => t.Category)
                        .Include(t => t.Comments)
                        .FirstOrDefault(t => t.Id == _technologyId);

                    if (_technology == null)
                    {
                        MessageBox.Show("Технология не найдена", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        return;
                    }

                    // Устанавливаем данные
                    nameLabel.Text = $"<span style='color:white; font-size:32px; font-weight:600'>{_technology.Name}</span>";

                    categoryLabel.Text = $"<span style='background:rgba(0,0,0,0.2); color:white; padding:5px 15px; border-radius:20px; font-size:14px'>{_technology.Category?.Name ?? "Без категории"}</span>";

                    // Рейтинг
                    double avgRating = _technology.Comments
                        .Where(c => c.IsApproved)
                        .Average(c => (double?)c.Rating) ?? 0;
                    int ratingCount = _technology.Comments.Count(c => c.IsApproved);

                    ratingLabel.Text = $"<span style='color:#10b981; font-size:24px'>{GetRatingStars(avgRating)}</span> " +
                                      $"<span style='background:rgba(16,185,129,0.1); color:#10b981; padding:5px 10px; border-radius:4px; font-size:14px'>{avgRating:F1}/5 ({ratingCount} оценок)</span>";

                    // Описание
                    descriptionLabel.Text = $"<p style='color:#b3b3b3; line-height:1.6'>{_technology.Description}</p>";

                    // Полное описание
                    var fullDescLabel = (Guna2HtmlLabel)mainPanel.Controls[4].Controls[1];
                    fullDescLabel.Text = $"<p style='color:#b3b3b3; line-height:1.7'>{_technology.Description}</p>";

                    // Загрузка изображения (улучшенная версия)
                    LoadTechnologyImage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTechnologyImage()
        {
            if (string.IsNullOrEmpty(_technology.ImagePath))
            {
                logoPictureBox.Image = null;
                return;
            }

            try
            {
                // Проверяем, является ли ImagePath путем к файлу или base64 строкой
                if (_technology.ImagePath.StartsWith("data:image"))
                {
                    // Обработка base64 изображения
                    var base64Data = _technology.ImagePath.Split(',')[1];
                    var imageBytes = Convert.FromBase64String(base64Data);
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        logoPictureBox.Image = Image.FromStream(ms);
                    }
                }
                else if (File.Exists(_technology.ImagePath))
                {
                    // Обработка абсолютного пути
                    logoPictureBox.Image = Image.FromFile(_technology.ImagePath);
                }
                else
                {
                    // Попытка найти в папке приложения
                    string appImagePath = Path.Combine(
                        Application.StartupPath,
                        "TechImages",
                        Path.GetFileName(_technology.ImagePath));

                    if (File.Exists(appImagePath))
                    {
                        logoPictureBox.Image = Image.FromFile(appImagePath);
                    }
                    else
                    {
                        logoPictureBox.Image = null;
                        Debug.WriteLine($"Изображение не найдено: {_technology.ImagePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                logoPictureBox.Image = null;
                Debug.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
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

        private void LoginUserCard()
        {
                    using (var db = new AppDbContext())
                    {
                        var bookmark = db.Bookmarks
                            .FirstOrDefault(b => b.UserId == CurrentUser.Id && b.TechnologyId == _technologyId);

                        if (bookmark != null)
                        {
                            addBookmarkButton.Text = "Удалить из закладок";
                            addBookmarkButton.Click -= addBookmarkButton_Click;
                            addBookmarkButton.Click += removeCard;
                        }
                    }
        }

        private void removeCard(object sender, EventArgs e)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var bookmark = db.Bookmarks
                        .FirstOrDefault(b => b.UserId == CurrentUser.Id && b.TechnologyId == _technologyId);

                    if (bookmark != null)
                    {
                        db.Bookmarks.Remove(bookmark);
                        db.SaveChanges();

                        MessageBox.Show("Технология удалена из закладок!",
                                      "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        addBookmarkButton.Text = "Добавить в закладки";
                        addBookmarkButton.Click -= removeCard;
                        addBookmarkButton.Click += addBookmarkButton_Click;
                    }
                    else
                    {
                        MessageBox.Show("Закладка не найдена!",
                                      "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении из закладок: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateViewCount()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var tech = db.Technologies.Find(_technologyId);
                    if (tech != null)
                    {
                        tech.ViewCount++;
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
                // Лог бы не забыть добавить...
            }
        }

        private void addCommentButton_Click(object sender, EventArgs e)
        {
            if (!CurrentUser.IsAuthenticated)
            {
                MessageBox.Show("Для добавления комментария необходимо войти в систему",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var commentForm = new CommentsForm(_technologyId);
            if (commentForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadTechnologyData(); // Обновляем данные после добавления комментария
            }
        }

        private void addBookmarkButton_Click(object sender, EventArgs e)
        {
            if (!CurrentUser.IsAuthenticated)
            {
                MessageBox.Show("Для добавления в закладки необходимо войти в систему",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    bool exists = db.Bookmarks.Any(b => b.UserId == CurrentUser.Id && b.TechnologyId == _technologyId);

                    if (exists)
                    {
                        MessageBox.Show("Эта технология уже в ваших закладках",
                                      "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var bookmark = new Bookmark
                    {
                        UserId = CurrentUser.Id,
                        TechnologyId = _technologyId,
                        DateAdded = DateTime.Now
                    };

                    db.Bookmarks.Add(bookmark);
                    db.SaveChanges();

                    MessageBox.Show("Технология добавлена в закладки",
                                  "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении в закладки: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Для перемещения формы
        private bool _dragging;
        private Point _startPoint;

        private void headerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = e.Location;
        }

        private void headerPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - _startPoint.X, p.Y - _startPoint.Y);
            }
        }

        private void headerPanel_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
    }
}