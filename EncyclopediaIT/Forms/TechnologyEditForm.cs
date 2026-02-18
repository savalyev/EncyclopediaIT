using System;
using System.Drawing;
using System.Windows.Forms;
using EncyclopediaIT.Models;
using System.Data.Entity;
using Guna.UI2.WinForms;
using System.IO;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Drawing.Imaging;

namespace EncyclopediaIT.Forms
{
    public partial class TechnologyEditForm : Form
    {
        private Technology _technology;
        private bool _isNewTechnology;

        // Основные элементы управления
        private Guna2Panel mainPanel;
        private Guna2Panel headerPanel;
        private Guna2Panel contentPanel;

        // Элементы формы
        private Guna2HtmlLabel titleLabel;
        private Guna2Button saveButton;

        private Guna2HtmlLabel nameLabel;
        private Guna2TextBox nameTextBox;

        private Guna2HtmlLabel categoryLabel;
        private Guna2ComboBox categoryComboBox;

        private Guna2HtmlLabel descriptionLabel;
        private Guna2TextBox descriptionTextBox;

        private Guna2HtmlLabel imageLabel;
        private Guna2Panel imageContainer;
        private Guna2PictureBox imagePictureBox;
        private Guna2Button selectImageButton;

        public TechnologyEditForm(Technology technology = null)
        {
            InitializeComponent();
            _technology = technology ?? new Technology();
            _isNewTechnology = technology == null;

            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            // Настройка основной формы
            this.Text = _isNewTechnology ? "Добавить технологию" : "Редактировать технологию";
            this.ClientSize = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(13, 17, 23);

            // Основная панель
            mainPanel = new Guna2Panel
            {
                Location = new Point(30, 30),
                Size = new Size(740, 640),
                BackColor = Color.FromArgb(13, 17, 23, 180),
                BorderRadius = 15,
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderThickness = 1
            };

            // Хедер с градиентом
            headerPanel = new Guna2Panel
            {
                Location = new Point(0, 0),
                Size = new Size(740, 70),
                FillColor = Color.Transparent,
                BorderRadius = 15,
                BorderThickness = 0
            };
            headerPanel.Paint += (sender, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(58, 12, 163),
                    Color.FromArgb(76, 201, 240),
                    0f))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }
            };

            // Заголовок
            titleLabel = new Guna2HtmlLabel
            {
                Location = new Point(20, 20),
                Size = new Size(300, 30),
                Text = "<span style='color:white; font-size:18px; font-weight:600'>РЕДАКТОР ТЕХНОЛОГИИ</span>"
            };

            // Кнопка сохранения
            saveButton = new Guna2Button
            {
                Location = new Point(600, 17),
                Size = new Size(120, 35),
                Text = "Сохранить",
                BorderRadius = 6,
                FillColor = Color.FromArgb(16, 185, 129),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            saveButton.Click += SaveButton_Click;

            var closeButton = new Guna2Button()
            {
                Text = "×",
                Font = new Font("Segoe UI", 16),
                ForeColor = Color.White,
                FillColor = Color.Transparent,
                BorderColor = Color.Transparent,
                Size = new Size(40, 40),
                Location = new Point(540, 17),
                Animated = true,
                Cursor = Cursors.Hand
            };
            closeButton.Click += (s, e) => this.Close();

            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(saveButton);
            headerPanel.Controls.Add(closeButton);

            // Контентная область
            contentPanel = new Guna2Panel
            {
                Location = new Point(0, 70),
                Size = new Size(740, 640),
                BackColor = Color.Transparent,
                AutoScroll = true
            };

            // Поле названия
            nameLabel = new Guna2HtmlLabel
            {
                Location = new Point(20, 20),
                Size = new Size(200, 20),
                Text = "<span style='color:#8b949e; font-size:12px'>Название технологии</span>"
            };

            nameTextBox = new Guna2TextBox
            {
                Location = new Point(20, 45),
                Size = new Size(700, 40),
                BorderRadius = 6,
                FillColor = Color.FromArgb(22, 27, 34),
                BorderColor = Color.FromArgb(48, 54, 61),
                ForeColor = Color.White,
                Cursor = Cursors.IBeam
            };

            // Поле категории
            categoryLabel = new Guna2HtmlLabel
            {
                Location = new Point(20, 100),
                Size = new Size(200, 20),
                Text = "<span style='color:#8b949e; font-size:12px'>Категория</span>"
            };

            categoryComboBox = new Guna2ComboBox
            {
                Location = new Point(20, 125),
                Size = new Size(700, 40),
                BorderRadius = 6,
                FillColor = Color.FromArgb(22, 27, 34),
                BorderColor = Color.FromArgb(48, 54, 61),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Поле описания
            descriptionLabel = new Guna2HtmlLabel
            {
                Location = new Point(20, 180),
                Size = new Size(200, 20),
                Text = "<span style='color:#8b949e; font-size:12px'>Описание</span>"
            };

            descriptionTextBox = new Guna2TextBox
            {
                Location = new Point(20, 205),
                Size = new Size(700, 150),
                BorderRadius = 6,
                FillColor = Color.FromArgb(22, 27, 34),
                BorderColor = Color.FromArgb(48, 54, 61),
                ForeColor = Color.White,
                Cursor = Cursors.IBeam,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // Поле изображения
            imageLabel = new Guna2HtmlLabel
            {
                Location = new Point(20, 370),
                Size = new Size(200, 20),
                Text = "<span style='color:#8b949e; font-size:12px'>Изображение</span>"
            };
            // Контейнер для изображения
            imageContainer = new Guna2Panel
            {
                Location = new Point(20, 395),
                Size = new Size(700, 150),  // Увеличил высоту для лучшего отображения
                BorderRadius = 8,
                BorderColor = Color.FromArgb(48, 54, 61),
                BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash,
                BorderThickness = 2,
                FillColor = Color.Transparent,
                Padding = new Padding(10)  // Отступы внутри контейнера
            };

            // PictureBox для отображения изображения (изначально скрыт)
            imagePictureBox = new Guna2PictureBox
            {
                Dock = DockStyle.Fill,  // Занимает всё доступное пространство
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderRadius = 6,
                BackColor = Color.Transparent,
                Visible = false  // Показывается только когда есть изображение
            };

            // Кнопка для выбора изображения (центрирована)
            selectImageButton = new Guna2Button
            {
                Size = new Size(160, 45),
                Text = "Выбрать изображение",
                BorderRadius = 6,
                FillColor = Color.FromArgb(22, 27, 34),
                BorderColor = Color.FromArgb(48, 54, 61),
                ForeColor = Color.FromArgb(88, 166, 255),
                Anchor = AnchorStyles.None  // Позволяет центрировать
            };

            // Центрируем кнопку
            selectImageButton.Location = new Point(
                (imageContainer.Width - selectImageButton.Width) / 2,
                (imageContainer.Height - selectImageButton.Height) / 2
            );

            selectImageButton.Click += SelectImageButton_Click;

            // Добавляем элементы в контейнер
            imageContainer.Controls.Add(imagePictureBox);
            imageContainer.Controls.Add(selectImageButton);

            // Добавляем все элементы на contentPanel
            contentPanel.Controls.AddRange(new Control[] {
    nameLabel,
    nameTextBox,
    categoryLabel,
    categoryComboBox,
    descriptionLabel,
    descriptionTextBox,
    imageLabel,
    imageContainer
});

            // Сборка интерфейса
            mainPanel.Controls.Add(headerPanel);
            mainPanel.Controls.Add(contentPanel);
            this.Controls.Add(mainPanel);

            // Тень для формы
            new Guna2ShadowForm(this);
        }

        private void LoadData()
        {
            using (var db = new AppDbContext())
            {
                categoryComboBox.DataSource = db.Categories.ToList();
                categoryComboBox.DisplayMember = "Name";
                categoryComboBox.ValueMember = "Id";
            }

            if (!_isNewTechnology)
            {
                nameTextBox.Text = _technology.Name;
                descriptionTextBox.Text = _technology.Description;
                categoryComboBox.SelectedValue = _technology.CategoryId;

                if (!string.IsNullOrEmpty(_technology.ImagePath) && File.Exists(_technology.ImagePath))
                {
                    imagePictureBox.Image = Image.FromFile(_technology.ImagePath);
                    selectImageButton.Visible = false;
                }
            }
        }

        private void SelectImageButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        imagePictureBox.Image = Image.FromFile(openFileDialog.FileName);
                        _technology.ImagePath = openFileDialog.FileName;
                        selectImageButton.Visible = false;
                        imagePictureBox.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Введите название технологии");
                return;
            }

            if (categoryComboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите категорию");
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    _technology.Name = nameTextBox.Text;
                    _technology.Description = descriptionTextBox.Text;
                    _technology.CategoryId = (int)categoryComboBox.SelectedValue;

                    if (imagePictureBox.Image != null)
                    {
                        string imageFolder = Path.Combine(Application.StartupPath, "TechImages");
                        Directory.CreateDirectory(imageFolder);
                        string fileName = $"{Guid.NewGuid()}.jpg";
                        string fullPath = Path.Combine(imageFolder, fileName);
                        imagePictureBox.Image.Save(fullPath, ImageFormat.Jpeg);
                        _technology.ImagePath = fileName;
                    }




                    if (_isNewTechnology)
                    {
                        _technology.ViewCount = 0;
                        _technology.CreatedDate = DateTime.Now;
                        db.Technologies.Add(_technology);
                    }
                    else
                    {
                        db.Entry(_technology).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (DbUpdateException ex)
            {
                string errorDetails = ex.InnerException?.InnerException?.Message ?? ex.Message;
                MessageBox.Show($"Ошибка сохранения: {errorDetails}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string SaveImageToFolder()
        {
            string appPath = Application.StartupPath;
            string imagesFolder = Path.Combine(appPath, "TechImages");

            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            string fileName = Guid.NewGuid().ToString() + ".png";
            string fullPath = Path.Combine(imagesFolder, fileName);

            imagePictureBox.Image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);

            return fileName;
        }

    }
}