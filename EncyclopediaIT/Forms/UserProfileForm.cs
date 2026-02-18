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
using System.IO;
using System.Reflection;

namespace EncyclopediaIT.Forms
{
    public partial class UserProfileForm : Form
    {
        // Основные панели
        private Guna2Panel mainPanel;
        private Guna2Panel sidebarPanel;
        private Guna2Panel contentPanel;

        // Элементы UI
        private Guna2CircleButton profileButton;
        private Guna2Button bookmarksButton;
        private Guna2Button historyButton;
        private Guna2HtmlLabel profileTitle;
        private Guna2Button editButton;
        private Guna2Panel statsPanel;
        private Guna2Panel bookmarksPanel;
        private Guna2Panel commentsPanel;

        public UserProfileForm()
        {
            InitializeComponent();
            InitializeUI();
            LoadUserData();
            LoadBookmarks();
            LoadComments();
            LoadStats();
            LoadProfileImage();
        }

        private void InitializeUI()
        {
            ConfigureMainForm();

            InitializeMainPanels();

            ConfigureSidebar();


            ConfigureContentArea();

            AssembleUI();
        }

        private void ConfigureMainForm()
        {
            this.Text = "Личный кабинет";
            this.ClientSize = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(13, 17, 23);
            this.DoubleBuffered = true;
        }

        private void InitializeMainPanels()
        {
            mainPanel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            sidebarPanel = new Guna2Panel
            {
                Dock = DockStyle.Left,
                Width = 280,
                BackColor = Color.FromArgb(13, 17, 23),
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderThickness = 1,
                Padding = new Padding(20)
            };

            contentPanel = new Guna2Panel
            {
                Location = new Point(280, 0),
                Size = new Size(920, 800),
                BackColor = Color.FromArgb(13, 17, 23),
                AutoScroll = true,
                Padding = new Padding(30)
            };
        }

        private void ConfigureSidebar()
        {
            // Аватар пользователя
            profileButton = new Guna2CircleButton
            {
                Size = new Size(80, 80),
                Location = new Point(15, 20),
                FillColor = Color.Purple,
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderThickness = 2,
                Cursor = Cursors.Hand
            };
            profileButton.Click += ProfileButton_Click;

            // Информация о пользователе
            var userInfoPanel = new Panel
            {
                Location = new Point(110, 20),
                Size = new Size(150, 80),
                BackColor = Color.Transparent
            };

            var userNameLabel = new Label
            {
                Text = CurrentUser.Username,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(0, 10)
            };

            var userRoleLabel = new Label
            {
                Text = CurrentUser.IsAdmin ? "Администратор" : "Пользователь",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(139, 148, 158),
                AutoSize = true,
                Location = new Point(0, 35)
            };

            userInfoPanel.Controls.Add(userNameLabel);
            userInfoPanel.Controls.Add(userRoleLabel);

            // Меню навигации
            var menuTitle = new Label
            {
                Text = "ЛИЧНЫЙ КАБИНЕТ",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.FromArgb(110, 0, 255),
                AutoSize = true,
                Location = new Point(15, 120)
            };

            var profileNavButton = CreateNavButton("Профиль", 150, true);
            profileNavButton.Click += (s, e) => ShowProfileSection();

            bookmarksButton = CreateNavButton("Закладки", 200, false);
            bookmarksButton.Click += (s, e) => ShowBookmarksSection();

            historyButton = CreateNavButton("История", 250, false);
            historyButton.Click += (s, e) => ShowHistorySection();

            // Добавление элементов на боковую панель
            sidebarPanel.Controls.AddRange(new Control[] {
                profileButton, userInfoPanel, menuTitle,
                profileNavButton, bookmarksButton, historyButton
            });
        }

        private Guna2Button CreateNavButton(string text, int yPos, bool isActive)
        {
            return new Guna2Button
            {
                Text = text,
                Size = new Size(240, 40),
                Location = new Point(15, yPos),
                BorderRadius = 6,
                FillColor = isActive ? Color.FromArgb(20, 110, 0, 255) : Color.Transparent,
                BorderColor = isActive ? Color.FromArgb(110, 0, 255) : Color.FromArgb(48, 54, 61),
                BorderThickness = isActive ? 2 : 1,
                TextAlign = HorizontalAlignment.Left,
                Padding = new Padding(15, 0, 0, 0),
                ForeColor = isActive ? Color.FromArgb(110, 0, 255) : Color.White,
                Cursor = Cursors.Hand,
                Tag = text
            };
        }

        private void ConfigureContentArea()
        {
            // Заголовок профиля
            profileTitle = new Guna2HtmlLabel
            {
                Text = "<span style='color:#ffffff; font-size:24px; font-weight:300'>Мой <span style='color:#6e00ff'>профиль</span></span>",
                Location = new Point(10, 10),
                AutoSize = true
            };

            // Кнопка редактирования
            editButton = new Guna2Button
            {
                Text = "Кхм",
                Size = new Size(150, 35),
                Location = new Point(contentPanel.Width - 180, 10),
                BorderRadius = 6,
                FillColor = Color.FromArgb(20, 110, 0, 255),
                BorderColor = Color.FromArgb(110, 0, 255),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Visible = false
            };

            // Панель статистики
            statsPanel = new Guna2Panel
            {
                Size = new Size(contentPanel.Width - 60, 120),
                Location = new Point(0, 60),
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderThickness = 1,
                BorderRadius = 10
            };

            // Панель закладок
            bookmarksPanel = new Guna2Panel
            {
                Size = new Size(contentPanel.Width - 60, 300),
                Location = new Point(0, 200),
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderThickness = 1,
                BorderRadius = 10
            };

            // Панель комментариев (изначально скрыта)
            commentsPanel = new Guna2Panel
            {
                Size = new Size(contentPanel.Width - 60, 300),
                Location = new Point(0, 520),
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderThickness = 1,
                BorderRadius = 10,
                Visible = false
            };

            contentPanel.Controls.AddRange(new Control[] {
                profileTitle, editButton, statsPanel,
                bookmarksPanel, commentsPanel
            });
        }

        private void AssembleUI()
        {
            var closeButton = new Guna2Button()
            {
                Text = "×",
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.White,
                FillColor = Color.Transparent,
                BorderColor = Color.Transparent,
                Size = new Size(40, 40),
                Location = new Point(850, 10),
                Animated = true,
                Cursor = Cursors.Hand
            };
            closeButton.Click += (s, e) => this.Close();

            mainPanel.Controls.Add(contentPanel);
            mainPanel.Controls.Add(sidebarPanel);
            this.Controls.Add(mainPanel);
            contentPanel.Controls.Add(closeButton);
            new Guna2ShadowForm(this);
        }

        private void LoadProfileImage()
        {
            try
            {
                // Попытка загрузить из файла
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "profile.jpg");
                if (File.Exists(imagePath))
                {
                    profileButton.Image = Image.FromFile(imagePath);
                    profileButton.ImageSize = new Size(80, 80);
                }
                else
                {
                    // Альтернатива - инициалы пользователя
                    profileButton.Text = GetUserInitials();
                    profileButton.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения профиля: {ex.Message}");
                profileButton.Text = GetUserInitials();
                profileButton.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            }
        }

        private string GetUserInitials()
        {
            if (string.IsNullOrWhiteSpace(CurrentUser.Username))
                return "?";

            var parts = CurrentUser.Username.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return "?";

            return parts.Length == 1 ?
                parts[0].Substring(0, 1).ToUpper() :
                $"{parts[0].Substring(0, 1)}{parts[parts.Length - 1].Substring(0, 1)}".ToUpper();
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Загрузка и масштабирование изображения
                        using (var originalImage = Image.FromFile(openFileDialog.FileName))
                        {
                            var resizedImage = new Bitmap(80, 80);
                            using (var graphics = Graphics.FromImage(resizedImage))
                            {
                                graphics.DrawImage(originalImage, 0, 0, 80, 80);
                            }
                            profileButton.Image = resizedImage;
                            profileButton.Text = "";

                            // Сохранение в папку приложения
                            string destPath = Path.Combine(Application.StartupPath, "Resources", "profile.jpg");
                            Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                            resizedImage.Save(destPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                    }
                }
            }
        }

        private void LoadUserData()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.Find(CurrentUser.Id);
                    if (user != null)
                    {
                        UpdateUserInfoUI(user);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных пользователя: {ex.Message}");
            }
        }

        private void UpdateUserInfoUI(User user)
        {
            profileTitle.Text = $"<span style='color:#ffffff; font-size:24px; font-weight:300'>Мой <span style='color:#6e00ff'>профиль</span></span>";
        }

        private void LoadBookmarks()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var bookmarks = db.Bookmarks
                        .Include(b => b.Technology)
                        .Include(b => b.Technology.Category)
                        .Where(b => b.UserId == CurrentUser.Id)
                        .OrderByDescending(b => b.DateAdded)
                        .Take(6)
                        .ToList();

                    bookmarksPanel.Controls.Clear();

                    var bookmarksTitle = new Label
                    {
                        Text = "Последние закладки",
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        ForeColor = Color.White,
                        AutoSize = true,
                        Location = new Point(20, 20)
                    };

                    bookmarksPanel.Controls.Add(bookmarksTitle);

                    int x = 20, y = 60;
                    foreach (var bookmark in bookmarks)
                    {
                        if (bookmark?.Technology == null) continue;

                        var card = CreateBookmarkCard(bookmark);
                        card.Location = new Point(x, y);
                        bookmarksPanel.Controls.Add(card);

                        x += 220;
                        if (x + 220 > bookmarksPanel.Width)
                        {
                            x = 20;
                            y += 180;
                        }
                    }

                    // Если нет закладок
                    if (bookmarks.Count == 0)
                    {
                        var noBookmarksLabel = new Label
                        {
                            Text = "У вас пока нет закладок",
                            Font = new Font("Segoe UI", 10),
                            ForeColor = Color.FromArgb(139, 148, 158),
                            AutoSize = true,
                            Location = new Point(20, 60)
                        };
                        bookmarksPanel.Controls.Add(noBookmarksLabel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки закладок: {ex.Message}");
            }
        }

        private Guna2Panel CreateBookmarkCard(Bookmark bookmark)
        {
            var card = new Guna2Panel
            {
                Size = new Size(200, 160),
                BorderRadius = 8,
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderThickness = 1,
                FillColor = Color.FromArgb(22, 27, 34),
                Cursor = Cursors.Hand,
                Tag = bookmark.TechnologyId
            };

            // Градиентный заголовок
            var header = new Guna2Panel
            {
                Size = new Size(200, 80),
                BorderRadius = 8,
                BorderThickness = 0,
                FillColor = Color.Transparent
            };
            header.Paint += (s, e) => PaintGradientHeader(e.Graphics, header.ClientRectangle);

            // Иконка технологии
            var icon = new Label
            {
                Text = GetTechnologyIcon(bookmark.Technology.Name),
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(50, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(75, 15)
            };

            header.Controls.Add(icon);
            card.Controls.Add(header);

            // Основное содержимое
            var content = new Panel
            {
                Size = new Size(200, 80),
                Location = new Point(0, 80),
                BackColor = Color.Transparent,
                Padding = new Padding(15)
            };

            var nameLabel = new Label
            {
                Text = bookmark.Technology.Name,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(170, 20)
            };

            var dateLabel = new Label
            {
                Text = $"Добавлено: {bookmark.DateAdded:dd.MM.yyyy}",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(139, 148, 158),
                AutoSize = false,
                Size = new Size(170, 20),
                Location = new Point(0, 40)
            };

            content.Controls.Add(nameLabel);
            content.Controls.Add(dateLabel);
            card.Controls.Add(content);

            // Обработчики событий
            card.Click += (s, e) => OpenTechnologyCard(bookmark.TechnologyId);
            foreach (Control control in card.Controls)
            {
                control.Click += (s, e) => OpenTechnologyCard(bookmark.TechnologyId);
                control.Cursor = Cursors.Hand;
            }

            // Эффект при наведении
            card.MouseEnter += (s, e) => card.FillColor = Color.FromArgb(30, 35, 40);
            card.MouseLeave += (s, e) => card.FillColor = Color.FromArgb(22, 27, 34);

            return card;
        }

        private string GetTechnologyIcon(string techName)
        {
            if (string.IsNullOrWhiteSpace(techName))
                return "?";

            var parts = techName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return "?";

            return parts.Length == 1 ?
                parts[0].Substring(0, 1).ToUpper() :
                $"{parts[0].Substring(0, 1)}{parts[parts.Length - 1].Substring(0, 1)}".ToUpper();
        }

        private void PaintGradientHeader(Graphics g, Rectangle bounds)
        {
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                bounds,
                Color.FromArgb(110, 0, 255),
                Color.FromArgb(255, 0, 166),
                135f))
            {
                g.FillRectangle(brush, bounds);
            }
        }

        private void OpenTechnologyCard(int techId)
        {
            try
            {
                var techCard = new TechnologyCardForm(techId);
                if (techCard.ShowDialog() == DialogResult.OK)
                {
                    LoadBookmarks();
                    LoadStats();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть карточку: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComments()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var comments = db.Comments
                        .Include(c => c.Technology)
                        .Where(c => c.UserId == CurrentUser.Id)
                        .OrderByDescending(c => c.DatePosted)
                        .Take(5)
                        .ToList();

                    commentsPanel.Controls.Clear();

                    var commentsTitle = new Label
                    {
                        Text = "Последние комментарии",
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        ForeColor = Color.White,
                        AutoSize = true,
                        Location = new Point(20, 20)
                    };

                    commentsPanel.Controls.Add(commentsTitle);

                    int yPos = 60;
                    foreach (var comment in comments)
                    {
                        var commentControl = CreateCommentControl(comment, yPos);
                        commentsPanel.Controls.Add(commentControl);
                        yPos += 100;
                    }

                    if (comments.Count == 0)
                    {
                        var noCommentsLabel = new Label
                        {
                            Text = "У вас пока нет комментариев",
                            Font = new Font("Segoe UI", 10),
                            ForeColor = Color.FromArgb(139, 148, 158),
                            AutoSize = true,
                            Location = new Point(20, 60)
                        };
                        commentsPanel.Controls.Add(noCommentsLabel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки комментариев: {ex.Message}");
            }
        }

        private Control CreateCommentControl(Comment comment, int yPos)
        {
            var panel = new Guna2Panel
            {
                Size = new Size(commentsPanel.Width - 40, 90),
                Location = new Point(20, yPos),
                BorderRadius = 8,
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderThickness = 1,
                FillColor = Color.FromArgb(22, 27, 34),
                Tag = comment.Id
            };

            var techLabel = new Label
            {
                Text = comment.Technology?.Name ?? "Неизвестная технология",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(110, 0, 255),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            var commentLabel = new Label
            {
                Text = comment.Text.Length > 50 ?
                    comment.Text.Substring(0, 50) + "..." : comment.Text,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(200, 200, 200),
                AutoSize = false,
                Size = new Size(panel.Width - 20, 40),
                Location = new Point(10, 30)
            };

            var dateLabel = new Label
            {
                Text = comment.DatePosted.ToString("dd.MM.yyyy"),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(139, 148, 158),
                AutoSize = true,
                Location = new Point(10, 70)
            };

            var ratingLabel = new Label
            {
                Text = $"Оценка: {comment.Rating}/5",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gold,
                AutoSize = true,
                Location = new Point(panel.Width - 100, 70)
            };

            panel.Controls.AddRange(new Control[] { techLabel, commentLabel, dateLabel, ratingLabel });
            return panel;
        }

        private void LoadStats()
        {
            statsPanel.Controls.Clear();

            using (var db = new AppDbContext())
            {
                int bookmarksCount = db.Bookmarks.Count(b => b.UserId == CurrentUser.Id);
                int commentsCount = db.Comments.Count(c => c.UserId == CurrentUser.Id);
                double activityPercent = CalculateActivityPercent();

                CreateStatCard("Закладки", bookmarksCount.ToString(), Color.FromArgb(110, 0, 255), 20);
                CreateStatCard("Комментарии", commentsCount.ToString(), Color.FromArgb(255, 0, 166), 220);
                CreateStatCard("Активность", $"{activityPercent}%", Color.FromArgb(0, 255, 170), 420);
            }
        }

        private void CreateStatCard(string title, string value, Color color, int xPos)
        {
            var card = new Guna2Panel
            {
                Size = new Size(180, 80),
                Location = new Point(xPos, 20),
                BorderRadius = 10,
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderThickness = 1,
                FillColor = Color.FromArgb(22, 27, 34)
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(139, 148, 158),
                AutoSize = true,
                Location = new Point(15, 15)
            };

            var valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = color,
                AutoSize = true,
                Location = new Point(15, 35)
            };

            card.Controls.Add(titleLabel);
            card.Controls.Add(valueLabel);
            statsPanel.Controls.Add(card);
        }

        private double CalculateActivityPercent()
        {
            using (var db = new AppDbContext())
            {
                int daysRegistered = (DateTime.Now - CurrentUser.RegistrationDate).Days;
                if (daysRegistered < 1) daysRegistered = 1;

                int totalActions = db.Bookmarks.Count(b => b.UserId == CurrentUser.Id) +
                                 db.Comments.Count(c => c.UserId == CurrentUser.Id);

                double percent = (totalActions * 100.0) / (daysRegistered * 2);
                return Math.Min(Math.Round(percent), 100);
            }
        }

        private void ShowProfileSection()
        {
            profileTitle.Text = "<span style='color:#ffffff; font-size:24px; font-weight:300'>Мой <span style='color:#6e00ff'>профиль</span></span>";
            editButton.Text = "Редактировать";
            editButton.Click -= saveButton_Click;
            editButton.Click += saveButton_Click;
            editButton.Visible = false ;

            bookmarksPanel.Visible = true;
            commentsPanel.Visible = false;

            UpdateNavButtons("Профиль");
        }

        private void ShowBookmarksSection()
        {
            profileTitle.Text = "<span style='color:#ffffff; font-size:24px; font-weight:300'>Мои <span style='color:#6e00ff'>закладки</span></span>";
            editButton.Text = "Добавить";
            editButton.Click -= saveButton_Click;
            editButton.Visible = false;


            bookmarksPanel.Visible = true;
            commentsPanel.Visible = false;

            UpdateNavButtons("Закладки");
        }

        private void ShowHistorySection()
        {
            profileTitle.Text = "<span style='color:#ffffff; font-size:24px; font-weight:300'>Моя <span style='color:#6e00ff'>история</span></span>";
            editButton.Text = "Очистить";
            editButton.Click -= saveButton_Click;
            editButton.Click += ClearHistory_Click;

            bookmarksPanel.Visible = false;
            commentsPanel.Visible = true;

            UpdateNavButtons("История");
        }

        private void UpdateNavButtons(string activeButton)
        {
            foreach (Control control in sidebarPanel.Controls)
            {
                if (control is Guna2Button button)
                {
                    bool isActive = button.Tag.ToString() == activeButton;
                    button.FillColor = isActive ? Color.FromArgb(20, 110, 0, 255) : Color.Transparent;
                    button.BorderColor = isActive ? Color.FromArgb(110, 0, 255) : Color.FromArgb(48, 54, 61);
                    button.BorderThickness = isActive ? 2 : 1;
                    button.ForeColor = isActive ? Color.FromArgb(110, 0, 255) : Color.White;
                }
            }
        }

        private void ClearHistory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Очистить историю просмотров?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Реализация очистки истории
                MessageBox.Show("История очищена", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //var editForm = new EditProfileForm();
            //if (editForm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadUserData();
            //    LoadProfileImage();
            //}
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Дополнительное оформление формы
            using (var pen = new Pen(Color.FromArgb(48, 54, 61), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }
    }
}