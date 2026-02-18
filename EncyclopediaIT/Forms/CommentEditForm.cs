using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EncyclopediaIT.Models;
using System.Data.Entity;
using Guna.UI2.WinForms;

namespace EncyclopediaIT.Forms
{
    public partial class CommentsForm : Form
    {
        private readonly int _technologyId;
        private Guna2TextBox commentTextBox;
        private Guna2ComboBox ratingComboBox;
        private Guna2Panel commentsPanel;

        public CommentsForm(int technologyId)
        {
            _technologyId = technologyId;
            InitializeCustomComponents();
            LoadComments();
        }

        private void InitializeCustomComponents()
        {
            // Настройка формы
            this.Text = "Комментарии";
            this.ClientSize = new Size(800, 600);
            this.BackColor = Color.FromArgb(13, 17, 23);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Панель заголовка
            var headerPanel = new Guna2Panel()
            {
                Dock = DockStyle.Top,
                Height = 60,
                FillColor = Color.Transparent,
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderThickness = 0
            };

            var titleLabel = new Guna2HtmlLabel()
            {
                Text = "Комментарии",
                ForeColor = Color.FromArgb(88, 166, 255),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };

            var closeButton = new Guna2Button()
            {
                Text = "×",
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.White,
                FillColor = Color.Transparent,
                BorderColor = Color.Transparent,
                Size = new Size(40, 40),
                Location = new Point(740, 10),
                Animated = true,
                Cursor = Cursors.Hand
            };
            closeButton.Click += (s, e) => this.Close();

            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(closeButton);
            this.Controls.Add(headerPanel);

            // Панель ввода комментария
            var inputPanel = new Guna2Panel()
            {
                Dock = DockStyle.Bottom,
                Height = 150,
                FillColor = Color.Transparent,
                Padding = new Padding(20),
                Margin = new Padding(0)
            };

            commentTextBox = new Guna2TextBox()
            {
                PlaceholderText = "Оставьте комментарий...",
                BackColor = Color.FromArgb(22, 27, 34),
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderRadius = 8,
                FillColor = Color.FromArgb(22, 27, 34),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Multiline = true,
                Size = new Size(760, 80),
                Location = new Point(20, 20),
                Cursor = Cursors.IBeam
            };

            ratingComboBox = new Guna2ComboBox()
            {
                Items = { "★ ★ ★ ★ ★", "★ ★ ★ ★ ☆", "★ ★ ★ ☆ ☆", "★ ★ ☆ ☆ ☆", "★ ☆ ☆ ☆ ☆" },
                BackColor = Color.FromArgb(22, 27, 34),
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderRadius = 4,
                FillColor = Color.FromArgb(22, 27, 34),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Size = new Size(150, 36),
                Location = new Point(20, 110)
            };

            var submitButton = new Guna2Button()
            {
                Text = "Отправить",
                FillColor = Color.FromArgb(35, 134, 54),
                BorderRadius = 6,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Size = new Size(120, 36),
                Location = new Point(650, 110),
                Animated = true,
                Cursor = Cursors.Hand
            };
            submitButton.Click += SubmitButton_Click;

            inputPanel.Controls.Add(commentTextBox);
            inputPanel.Controls.Add(ratingComboBox);
            inputPanel.Controls.Add(submitButton);
            this.Controls.Add(inputPanel);

            // Панель списка комментариев
            commentsPanel = new Guna2Panel()
            {
                Dock = DockStyle.Fill,
                FillColor = Color.Transparent,
                Padding = new Padding(20, 10, 20, 10),
                AutoScroll = true
            };

            this.Controls.Add(commentsPanel);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (!CurrentUser.IsAuthenticated)
            {
                MessageBox.Show("Для добавления комментария необходимо войти в систему",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(commentTextBox.Text))
            {
                MessageBox.Show("Введите текст комментария", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    var rating = 5 - ratingComboBox.SelectedIndex; // Конвертируем звезды в число (5 - индекс)

                    var comment = new Comment
                    {
                        TechnologyId = _technologyId,
                        UserId = CurrentUser.Id,
                        Text = commentTextBox.Text,
                        Rating = rating > 0 ? rating : 5, // По умолчанию 5 звезд
                        DatePosted = DateTime.Now,
                        IsApproved = true // Или false, если требуется модерация
                    };

                    db.Comments.Add(comment);
                    db.SaveChanges();

                    // Очищаем поле ввода
                    commentTextBox.Text = "";
                    ratingComboBox.SelectedIndex = -1;

                    // Обновляем список комментариев
                    LoadComments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении комментария: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComments()
        {
            commentsPanel.Controls.Clear();

            using (var db = new AppDbContext())
            {
                var comments = db.Comments
                    .Include(c => c.User)
                    .Where(c => c.TechnologyId == _technologyId && c.IsApproved)
                    .OrderByDescending(c => c.DatePosted)
                    .ToList();

                if (!comments.Any())
                {
                    var noCommentsLabel = new Guna2HtmlLabel()
                    {
                        Text = "Пока нет комментариев. Будьте первым!",
                        ForeColor = Color.FromArgb(139, 148, 158),
                        Font = new Font("Segoe UI", 12),
                        Location = new Point(20, 20),
                        AutoSize = true
                    };
                    commentsPanel.Controls.Add(noCommentsLabel);
                    return;
                }

                int yPos = 70;
                foreach (var comment in comments)
                {
                    var commentItem = CreateCommentItem(
                        comment.User.Username,
                        comment.Text,
                        ConvertRatingToStars(comment.Rating),
                        comment.DatePosted.ToString("dd.MM.yyyy HH:mm"));

                    commentItem.Location = new Point(20, yPos);
                    yPos += commentItem.Height + 15;

                    commentsPanel.Controls.Add(commentItem);
                }
            }
        }

        private Guna2Panel CreateCommentItem(string username, string commentText, string rating, string date)
        {
            var commentPanel = new Guna2Panel()
            {
                BackColor = Color.FromArgb(22, 27, 34, 150),
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderRadius = 8,
                BorderThickness = 1,
                Size = new Size(760, 100),
                Margin = new Padding(0, 0, 0, 15),
                Padding = new Padding(15)
            };

            // Аватар пользователя
            var avatar = new Guna2CircleButton()
            {
                Text = GetInitials(username),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FillColor = Color.FromArgb(58, 12, 163),
                Size = new Size(32, 32),
                Location = new Point(0, 0),
                Enabled = false
            };

            // Имя пользователя
            var usernameLabel = new Guna2HtmlLabel()
            {
                Text = username,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(42, 0),
                AutoSize = true
            };

            // Дата комментария
            var dateLabel = new Guna2HtmlLabel()
            {
                Text = date,
                ForeColor = Color.FromArgb(139, 148, 158),
                Font = new Font("Segoe UI", 8),
                Location = new Point(42, 20),
                AutoSize = true
            };

            // Текст комментария
            var commentLabel = new Guna2HtmlLabel()
            {
                Text = commentText,
                ForeColor = Color.FromArgb(201, 209, 217),
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, 42),
                MaximumSize = new Size(740, 0),
                AutoSize = true
            };

            // Рейтинг
            var ratingLabel = new Guna2HtmlLabel()
            {
                Text = rating,
                ForeColor = Color.Gold,
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, 70),
                AutoSize = true
            };

            commentPanel.Controls.Add(avatar);
            commentPanel.Controls.Add(usernameLabel);
            commentPanel.Controls.Add(dateLabel);
            commentPanel.Controls.Add(commentLabel);
            commentPanel.Controls.Add(ratingLabel);

            // Динамически подстраиваем высоту под содержимое
            commentPanel.Height = commentLabel.Location.Y + commentLabel.Height + 30;

            return commentPanel;
        }

        private string GetInitials(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return "??";

            var parts = username.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[1][0]}".ToUpper();

            return username.Length >= 2
                ? username.Substring(0, 2).ToUpper()
                : username.ToUpper() + "?";
        }

        private string ConvertRatingToStars(int rating)
        {
            if (rating == 1)
                return "★ ☆ ☆ ☆ ☆";
            else if (rating == 2)
                return "★ ★ ☆ ☆ ☆";
            else if (rating == 3)
                return "★ ★ ★ ☆ ☆";
            else if (rating == 4)
                return "★ ★ ★ ★ ☆";
            else if (rating == 5)
                return "★ ★ ★ ★ ★";
            else
                return "★ ★ ★ ★ ★"; // Значение по умолчанию
        }
    }
}