using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EncyclopediaIT.Models;
using Guna.UI2.WinForms;
using OfficeOpenXml;
using System.Data.Entity;
using System.Text;
using System.Web.UI.WebControls;
using Label = System.Windows.Forms.Label;
using BorderStyle = System.Windows.Forms.BorderStyle;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EncyclopediaIT.Forms
{
    public partial class AdminDashboardForm : Form
    {
        private Guna2Panel mainPanel;
        private Guna2Panel headerPanel;
        private Guna2Panel navPanel;
        private Guna2Panel contentPanel;
        private Guna2DataGridView usersDataGridView;
        private Guna2DataGridView technologiesDataGridView;
        private Guna2DataGridView commentsDataGridView;
        private Guna2DataGridView reportDataGridView;
        private Guna2ComboBox reportTypeComboBox;
        private Guna2DateTimePicker fromDatePicker;
        private Guna2DateTimePicker toDatePicker;
        private Guna2Button btnUsers;
        private Guna2Button btnContent;
        private Guna2Button btnModeration;
        private Guna2Button btnReport;
        private List<dynamic> _originalUsersData;
        private List<dynamic> _originalTechData;
        private List<dynamic> _originalCommentsData;


        public AdminDashboardForm()
        {
            InitializeComponents();
            LoadData();
            this.DoubleBuffered = true;
        }

        private void InitializeComponents()
        {
            // Настройка основной формы
            this.Text = "ADMIN CONSOLE v1.0";
            this.ClientSize = new Size(1200, 800); // 1200
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(10, 10, 18);

            // Основная панель
            mainPanel = new Guna2Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.Transparent;

            // Хедер
            headerPanel = new Guna2Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            headerPanel.FillColor = Color.Transparent;

            // Градиент для хедера
            headerPanel.Paint += (sender, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(58, 12, 163), // #3a0ca3
                    Color.FromArgb(76, 201, 240), // #4cc9f0
                    0f))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }
            };

            // Заголовок и статус
            var headerContent = new Guna2Panel();
            headerContent.Dock = DockStyle.Fill;
            headerContent.BackColor = Color.Transparent;
            headerContent.Padding = new Padding(25, 0, 25, 0);

            var titlePanel = new Guna2Panel();
            titlePanel.AutoSize = true;
            titlePanel.BackColor = Color.Transparent;
            titlePanel.Location = new Point(0, 0);

            var iconLabel = new Label();
            iconLabel.Text = "⚙️";
            iconLabel.Font = new Font("Segoe UI", 24);
            iconLabel.AutoSize = true;
            iconLabel.Location = new Point(0, 10);

            var titleLabel = new Label();
            titleLabel.Text = "ADMIN CONSOLE v1.0";
            titleLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(58, 15);

            var statusLabel = new Guna2HtmlLabel();
            statusLabel.Text = "<span style='background:rgba(0,0,0,0.3); padding:5px 15px; border-radius:20px; font-size:14px'>Супер-администратор</span>";
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(900, 15);

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

            titlePanel.Controls.Add(iconLabel);
            titlePanel.Controls.Add(titleLabel);
            titlePanel.Controls.Add(closeButton);
            headerContent.Controls.Add(titlePanel);
            headerContent.Controls.Add(statusLabel);
            headerPanel.Controls.Add(headerContent);

            // Навигационная панель
            navPanel = new Guna2Panel();
            navPanel.Dock = DockStyle.Left;
            navPanel.Width = 250;
            navPanel.BackColor = Color.FromArgb(18, 18, 26);
            navPanel.BorderColor = Color.FromArgb(37, 37, 53);
            navPanel.BorderThickness = 1;

            var navHeader = new Guna2HtmlLabel();
            navHeader.Text = "<span style='color:#4cc9f0; font-size:12px; margin-bottom:15px'>УПРАВЛЕНИЕ</span>";
            navHeader.AutoSize = false;
            navHeader.Size = new Size(210, 30);
            navHeader.Location = new Point(20, 20);

            btnUsers = new Guna2Button();
            btnUsers.Text = "Пользователи";
            btnUsers.FillColor = Color.FromArgb(76, 201, 240, 50);
            btnUsers.ForeColor = Color.FromArgb(76, 201, 240);
            btnUsers.BorderThickness = 1;
            btnUsers.BorderRadius = 6;
            btnUsers.Size = new Size(210, 45);
            btnUsers.Location = new Point(20, 60);
            btnUsers.Click += (s, e) => ShowUsersTab();

            btnContent = new Guna2Button();
            btnContent.Text = "Контент";
            btnContent.FillColor = Color.Transparent;
            btnContent.ForeColor = Color.White;
            btnContent.BorderRadius = 6;
            btnContent.Size = new Size(210, 45);
            btnContent.Location = new Point(20, 115);
            btnContent.Click += (s, e) => ShowTechnologiesTab();

            btnModeration = new Guna2Button();
            btnModeration.Text = "Модерация комментариев";
            btnModeration.FillColor = Color.Transparent;
            btnModeration.ForeColor = Color.White;
            btnModeration.BorderRadius = 6;
            btnModeration.Size = new Size(210, 45);
            btnModeration.Location = new Point(20, 170);
            btnModeration.Click += (s, e) => ShowCommentsTab();

            btnReport = new Guna2Button();
            btnReport.Text = "Отчетность";
            btnReport.FillColor = Color.Transparent;
            btnReport.ForeColor = Color.White;
            btnReport.BorderRadius = 6;
            btnReport.Size = new Size(210, 45);
            btnReport.Location = new Point(20, 225);
            btnReport.Click += (s, e) => ShowReportsTab();

            navPanel.Controls.Add(navHeader);
            navPanel.Controls.Add(btnUsers);
            navPanel.Controls.Add(btnContent);
            navPanel.Controls.Add(btnReport);
            navPanel.Controls.Add(btnModeration);

            // Контентная панель
            contentPanel = new Guna2Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.FromArgb(10, 10, 18);
            contentPanel.AutoScroll = true;

            // Инициализация вкладок
            InitializeUsersTab();
            InitializeTechnologiesTab();
            InitializeCommentsTab();
            InitializeReportsTab();

            // Собираем интерфейс
            mainPanel.Controls.Add(contentPanel);
            mainPanel.Controls.Add(navPanel);
            mainPanel.Controls.Add(headerPanel);
            this.Controls.Add(mainPanel);

            // Тень для формы
            new Guna2ShadowForm(this);
        }

        private void InitializeUsersTab()
        {
            var usersTab = new Guna2Panel();
            usersTab.Name = "usersTab";
            usersTab.Dock = DockStyle.Fill;
            usersTab.BackColor = Color.Transparent;
            usersTab.Visible = false;

            var header = new Guna2Panel();
            header.Dock = DockStyle.Top;
            header.Height = 60;
            header.BackColor = Color.Transparent;

            var title = new Label();
            title.Text = "Управление пользователями";
            title.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            title.ForeColor = Color.White;
            title.AutoSize = true;
            title.Location = new Point(20, 20);

            var searchBox = new Guna2TextBox();
            searchBox.Name = "usersSearchBox";
            searchBox.PlaceholderText = "Поиск пользователей...";
            searchBox.Size = new Size(300, 35);
            searchBox.Location = new Point(650, 12);
            searchBox.BorderRadius = 6;
            searchBox.FillColor = Color.FromArgb(18, 18, 26);
            searchBox.BorderColor = Color.FromArgb(37, 37, 53);
            searchBox.ForeColor = Color.White;
            searchBox.TextChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(searchBox.Text))
                {
                    usersDataGridView.DataSource = _originalUsersData;
                }
                else
                {
                    FilterUsers();
                }
            };

            usersDataGridView = new Guna2DataGridView();
            ConfigureDataGridView(usersDataGridView);
            //usersDataGridView.Dock = DockStyle.Fill;
            //usersDataGridView.BackgroundColor = Color.FromArgb(18, 18, 26);
            //usersDataGridView.BorderStyle = BorderStyle.None;
            //usersDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //usersDataGridView.DefaultCellStyle.BackColor = Color.FromArgb(18, 18, 26);
            //usersDataGridView.DefaultCellStyle.ForeColor = Color.White;
            //usersDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(58, 12, 163);
            //usersDataGridView.EnableHeadersVisualStyles = false;
            //usersDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 26, 42);
            //usersDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //usersDataGridView.ColumnHeadersHeight = 40;
            //usersDataGridView.RowHeadersVisible = false;
            //usersDataGridView.RowTemplate.Height = 40;


            var blockButton = new Guna2Button();
            blockButton.Text = "Блокировать/Разблокировать";
            blockButton.Size = new Size(200, 40);
            blockButton.Location = new Point(20, 680);
            blockButton.BorderRadius = 6;
            blockButton.Click += blockUserButton_Click;

            var deleteButton = new Guna2Button();
            deleteButton.Text = "Удалить пользователя";
            deleteButton.Size = new Size(200, 40);
            deleteButton.Location = new Point(240, 680);
            deleteButton.BorderRadius = 6;
            deleteButton.Click += deleteUserButton_Click;

            var refreshButton = new Guna2Button();
            refreshButton.Text = "Обновить";
            refreshButton.Size = new Size(200, 40);
            refreshButton.Location = new Point(460, 680);
            refreshButton.BorderRadius = 6;
            refreshButton.Click += refreshUsersButton_Click;

            header.Controls.Add(title);
            header.Controls.Add(searchBox);
            usersTab.Controls.Add(usersDataGridView);
            usersTab.Controls.Add(blockButton);
            usersTab.Controls.Add(deleteButton);
            usersTab.Controls.Add(refreshButton);
            usersTab.Controls.Add(header);

            contentPanel.Controls.Add(usersTab);
        }

        private void InitializeTechnologiesTab()
        {
            var techTab = new Guna2Panel();
            techTab.Name = "techTab";
            techTab.Dock = DockStyle.Fill;
            techTab.BackColor = Color.Transparent;
            techTab.Visible = false;

            var header = new Guna2Panel();
            header.Dock = DockStyle.Top;
            header.Height = 60;
            header.BackColor = Color.Transparent;

            var title = new Label();
            title.Text = "Управление технологиями";
            title.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            title.ForeColor = Color.White;
            title.AutoSize = true;
            title.Location = new Point(20, 20);

            var searchBox = new Guna2TextBox();
            searchBox.Name = "techSearchBox";
            searchBox.PlaceholderText = "Поиск технологий...";
            searchBox.Size = new Size(300, 35);
            searchBox.Location = new Point(650, 12);
            searchBox.BorderRadius = 6;
            searchBox.FillColor = Color.FromArgb(18, 18, 26);
            searchBox.BorderColor = Color.FromArgb(37, 37, 53);
            searchBox.ForeColor = Color.White;
            searchBox.TextChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(searchBox.Text))
                {
                    technologiesDataGridView.DataSource = _originalTechData;
                }
                else
                {
                    FilterTechnologies();
                }
            };

            technologiesDataGridView = new Guna2DataGridView();
            ConfigureDataGridView(technologiesDataGridView);
            //technologiesDataGridView.Dock = DockStyle.Fill;
            //technologiesDataGridView.BackgroundColor = Color.FromArgb(18, 18, 26);
            //technologiesDataGridView.BorderStyle = BorderStyle.None;
            //technologiesDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //technologiesDataGridView.DefaultCellStyle.BackColor = Color.FromArgb(18, 18, 26);
            //technologiesDataGridView.DefaultCellStyle.ForeColor = Color.White;
            //technologiesDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(58, 12, 163);
            //technologiesDataGridView.EnableHeadersVisualStyles = false;
            //technologiesDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 26, 42);
            //technologiesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //technologiesDataGridView.ColumnHeadersHeight = 40;
            //technologiesDataGridView.RowHeadersVisible = false;
            //technologiesDataGridView.RowTemplate.Height = 40;

            var addButton = new Guna2Button();
            addButton.Text = "Добавить технологию";
            addButton.Size = new Size(200, 40);
            addButton.Location = new Point(20, 680);
            addButton.BorderRadius = 6;
            addButton.Click += addTechButton_Click;

            var editButton = new Guna2Button();
            editButton.Text = "Редактировать";
            editButton.Size = new Size(200, 40);
            editButton.Location = new Point(240, 680);
            editButton.BorderRadius = 6;
            editButton.Click += editTechButton_Click;

            var deleteButton = new Guna2Button();
            deleteButton.Text = "Удалить технологию";
            deleteButton.Size = new Size(200, 40);
            deleteButton.Location = new Point(460, 680);
            deleteButton.BorderRadius = 6;
            deleteButton.Click += deleteTechButton_Click;

            var refreshButton = new Guna2Button();
            refreshButton.Text = "Обновить";
            refreshButton.Size = new Size(200, 40);
            refreshButton.Location = new Point(680, 680);
            refreshButton.BorderRadius = 6;
            refreshButton.Click += refreshTechButton_Click;

            header.Controls.Add(title);
            header.Controls.Add(searchBox);
            techTab.Controls.Add(technologiesDataGridView);
            techTab.Controls.Add(addButton);
            techTab.Controls.Add(editButton);
            techTab.Controls.Add(deleteButton);
            techTab.Controls.Add(refreshButton);
            techTab.Controls.Add(header);

            contentPanel.Controls.Add(techTab);
        }

        private void InitializeCommentsTab()
        {
            var commentsTab = new Guna2Panel();
            commentsTab.Name = "commentsTab";
            commentsTab.Dock = DockStyle.Fill;
            commentsTab.BackColor = Color.Transparent;
            commentsTab.Visible = false;

            var header = new Guna2Panel();
            header.Dock = DockStyle.Top;
            header.Height = 80;
            header.BackColor = Color.Transparent;

            var title = new Label();
            title.Text = "Модерация комментариев";
            title.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            title.ForeColor = Color.White;
            title.AutoSize = true;
            title.Location = new Point(20, 5);

            // Поле поиска
            var searchBox = new Guna2TextBox();
            searchBox.Name = "commentsSearchBox";
            searchBox.PlaceholderText = "Поиск комментариев...";
            searchBox.Size = new Size(400, 35);
            searchBox.Location = new Point(20, 30);
            searchBox.BorderRadius = 6;
            searchBox.FillColor = Color.FromArgb(18, 18, 26);
            searchBox.BorderColor = Color.FromArgb(37, 37, 53);
            searchBox.ForeColor = Color.White;
            searchBox.TextChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(searchBox.Text))
                {
                    commentsDataGridView.DataSource = _originalCommentsData;
                }
                else
                {
                    FilterComments();
                }
            };

            commentsDataGridView = new Guna2DataGridView();
            ConfigureCommentsDataGridView(commentsDataGridView);

            // Кнопка удаления
            var deleteButton = new Guna2Button();
            deleteButton.Text = "Удалить выбранные";
            deleteButton.Size = new Size(200, 40);
            deleteButton.Location = new Point(20, 680);
            deleteButton.BorderRadius = 6;
            deleteButton.FillColor = Color.FromArgb(220, 53, 69);
            deleteButton.ForeColor = Color.White;
            deleteButton.Click += DeleteSelectedComments;

            // Кнопка обновления
            var refreshButton = new Guna2Button();
            refreshButton.Text = "Обновить список";
            refreshButton.Size = new Size(200, 40);
            refreshButton.Location = new Point(240, 680);
            refreshButton.BorderRadius = 6;
            refreshButton.Click += (s, e) => LoadComments();

            header.Controls.Add(title);
            header.Controls.Add(searchBox);
            commentsTab.Controls.Add(commentsDataGridView);
            commentsTab.Controls.Add(deleteButton);
            commentsTab.Controls.Add(refreshButton);
            commentsTab.Controls.Add(header);

            contentPanel.Controls.Add(commentsTab);
        }

        private void InitializeReportsTab()
        {
            var reportsTab = new Guna2Panel();
            reportsTab.Name = "reportsTab";
            reportsTab.Dock = DockStyle.Fill;
            reportsTab.BackColor = Color.Transparent;
            reportsTab.Visible = false;

            var header = new Guna2Panel();
            header.Dock = DockStyle.Top;
            header.Height = 100;
            header.BackColor = Color.Transparent;

            var title = new Label();
            title.Text = "Отчеты";
            title.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            title.ForeColor = Color.White;
            title.AutoSize = true;
            title.Location = new Point(20, 20);

            reportTypeComboBox = new Guna2ComboBox();
            reportTypeComboBox.Items.Add("Самые просматриваемые технологии");
reportTypeComboBox.Items.Add("Рейтинг технологий по оценкам");
            reportTypeComboBox.Size = new Size(300, 35);
            reportTypeComboBox.Location = new Point(20, 50);
            reportTypeComboBox.BorderRadius = 6;
            reportTypeComboBox.FillColor = Color.FromArgb(18, 18, 26);
            reportTypeComboBox.BorderColor = Color.FromArgb(37, 37, 53);
            reportTypeComboBox.ForeColor = Color.White;
            reportTypeComboBox.SelectedIndex = 0;

            fromDatePicker = new Guna2DateTimePicker();
            fromDatePicker.Size = new Size(150, 35);
            fromDatePicker.Location = new Point(350, 50);
            fromDatePicker.BorderRadius = 6;
            fromDatePicker.FillColor = Color.FromArgb(18, 18, 26);
            fromDatePicker.BorderColor = Color.FromArgb(37, 37, 53);
            fromDatePicker.ForeColor = Color.White;
            fromDatePicker.Format = DateTimePickerFormat.Short;
            fromDatePicker.Value = DateTime.Now.AddMonths(-1);

            toDatePicker = new Guna2DateTimePicker();
            toDatePicker.Size = new Size(150, 35);
            toDatePicker.Location = new Point(520, 50);
            toDatePicker.BorderRadius = 6;
            toDatePicker.FillColor = Color.FromArgb(18, 18, 26);
            toDatePicker.BorderColor = Color.FromArgb(37, 37, 53);
            toDatePicker.ForeColor = Color.White;
            toDatePicker.Format = DateTimePickerFormat.Short;
            toDatePicker.Value = DateTime.Now;

            var generateButton = new Guna2Button();
            generateButton.Text = "Сформировать отчет";
            generateButton.Size = new Size(200, 35);
            generateButton.Location = new Point(700, 50);
            generateButton.BorderRadius = 6;
            generateButton.Click += generateReportButton_Click;

            reportDataGridView = new Guna2DataGridView();
            ConfigureDataGridView(reportDataGridView);
            reportDataGridView.AutoGenerateColumns = true;
            //reportDataGridView.BackgroundColor = Color.FromArgb(18, 18, 26);
            //reportDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //reportDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //reportDataGridView.DefaultCellStyle.BackColor = Color.FromArgb(18, 18, 26);
            //reportDataGridView.DefaultCellStyle.ForeColor = Color.White;
            //reportDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(58, 12, 163);
            //reportDataGridView.EnableHeadersVisualStyles = false;
            //reportDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 26, 42);
            //reportDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //reportDataGridView.ColumnHeadersHeight = 40;
            //reportDataGridView.RowHeadersVisible = false;
            //reportDataGridView.RowTemplate.Height = 40;

            var exportExcelButton = new Guna2Button();
            exportExcelButton.Text = "Экспорт в Excel1";
            exportExcelButton.Size = new Size(150, 40);
            exportExcelButton.Location = new Point(120, 650);
            exportExcelButton.BorderRadius = 6;
            exportExcelButton.Click += (s, e) =>
            {
                if (reportDataGridView.DataSource != null)
                {
                    using (var saveDialog = new SaveFileDialog())
                    {
                        saveDialog.Filter = "Excel Files|*.xlsx";
                        saveDialog.Title = "Экспорт в Excel";
                        if (saveDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel(reportDataGridView.DataSource as IEnumerable<dynamic>, saveDialog.FileName);
                        }
                    }
                }
            };

            var exportCsvButton = new Guna2Button();
            exportCsvButton.Text = "Экспорт в CSV";
            exportCsvButton.Size = new Size(150, 40);
            exportCsvButton.Location = new Point(290, 650);
            exportCsvButton.BorderRadius = 6;
            exportCsvButton.Click += (s, e) =>
            {
                if (reportDataGridView.DataSource != null)
                {
                    using (var saveDialog = new SaveFileDialog())
                    {
                        saveDialog.Filter = "CSV Files|*.csv";
                        saveDialog.Title = "Экспорт в CSV";
                        if (saveDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToCsv(reportDataGridView.DataSource as IEnumerable<dynamic>, saveDialog.FileName);
                        }
                    }
                }
            };

            header.Controls.Add(title);
            header.Controls.Add(reportTypeComboBox);
            header.Controls.Add(fromDatePicker);
            header.Controls.Add(toDatePicker);
            header.Controls.Add(generateButton);
            reportsTab.Controls.Add(reportDataGridView);
            reportsTab.Controls.Add(exportExcelButton);
            reportsTab.Controls.Add(exportCsvButton);
            reportsTab.Controls.Add(header);

            contentPanel.Controls.Add(reportsTab);
        }

        private void ShowUsersTab()
        {
            foreach (Control ctrl in contentPanel.Controls)
            {
                ctrl.Visible = ctrl.Name == "usersTab";
            }

            ChangeSelect(btnUsers, btnContent);
            ChangeSelect(btnUsers, btnReport);
            ChangeSelect(btnUsers, btnModeration);

        }

        private void ShowTechnologiesTab()
        {
            foreach (Control ctrl in contentPanel.Controls)
            {
                ctrl.Visible = ctrl.Name == "techTab";
            }

            ChangeSelect(btnContent, btnUsers);
            ChangeSelect(btnContent, btnReport);
            ChangeSelect(btnContent, btnModeration);
        }

        private void ShowCommentsTab()
        {
            foreach (Control ctrl in contentPanel.Controls)
            {
                ctrl.Visible = ctrl.Name == "commentsTab";
            }

            ChangeSelect(btnModeration, btnUsers);
            ChangeSelect(btnModeration, btnReport);
            ChangeSelect(btnModeration, btnContent);
        }

        private void ShowReportsTab()
        {
            foreach (Control ctrl in contentPanel.Controls)
            {
                ctrl.Visible = ctrl.Name == "reportsTab";
            }

            ChangeSelect(btnReport, btnUsers);
            ChangeSelect(btnReport, btnModeration);
            ChangeSelect(btnReport, btnContent);
        }

        private void LoadData()
        {
            LoadUsers();
            LoadTechnologies();
            LoadComments();
        }

        private void LoadUsers()
        {
            using (var db = new AppDbContext())
            {
                _originalUsersData = db.Users
                    .Select(u => new
                    {
                        u.Id,
                        u.Username,
                        u.Email,
                        u.RegistrationDate,
                        u.IsAdmin,
                        IsBlocked = u.IsBlocked
                    })
                    .ToList<dynamic>();
                usersDataGridView.DataSource = _originalUsersData;
                // Настройка столбцов
                usersDataGridView.Columns["Id"].HeaderText = "ID";
                usersDataGridView.Columns["Username"].HeaderText = "Пользователь";
                usersDataGridView.Columns["Email"].HeaderText = "Email";
                usersDataGridView.Columns["RegistrationDate"].HeaderText = "Дата регистрации";
                usersDataGridView.Columns["IsAdmin"].HeaderText = "Админ";
                usersDataGridView.Columns["IsBlocked"].HeaderText = "Заблокирован";
            }
        }

        private string GetSearchText(string searchBoxName)
        {
            // Ищем в текущей активной вкладке
            var activeTab = contentPanel.Controls.Cast<Control>()
                .FirstOrDefault(c => c.Visible);

            if (activeTab != null)
            {
                var searchBox = activeTab.Controls.Find(searchBoxName, true)
                    .FirstOrDefault() as Guna2TextBox;
                return searchBox?.Text?.ToLower() ?? string.Empty;
            }
            return string.Empty;
        }

        private void FilterData<T>(Guna2DataGridView dataGridView, Func<T, bool> filterCondition)
        {
            if (dataGridView.DataSource is List<T> data)
            {
                var filteredData = data.Where(filterCondition).ToList();
                dataGridView.DataSource = filteredData;
            }
        }

        private void FilterUsers()
        {
            string searchText = GetSearchText("usersSearchBox");

            FilterData<dynamic>(usersDataGridView, u =>
                (u.Username as string)?.ToLower().Contains(searchText) ?? false);
        }

        private void FilterTechnologies()
        {
            string searchText = GetSearchText("techSearchBox");

            FilterData<dynamic>(technologiesDataGridView, t =>
                (t.Name as string)?.ToLower().Contains(searchText) ?? false);
        }

        private void FilterComments()
        {
            string searchText = GetSearchText("commentsSearchBox");

            FilterData<dynamic>(commentsDataGridView, c =>
                (c.Technology as string)?.ToLower().Contains(searchText) ?? false);
        }

        private void ChangeSelect(Guna2Button New, Guna2Button old)
        {
            old.FillColor = Color.Transparent;
            old.ForeColor = Color.White;

            New.FillColor = Color.FromArgb(76, 201, 240, 50);
            New.ForeColor = Color.FromArgb(76, 201, 240);
        }

        private void LoadTechnologies()
        {
            using (var db = new AppDbContext())
            {
                _originalTechData = db.Technologies
                    .Include(t => t.Category)
                    .Select(t => new
                    {
                        t.Id,
                        t.Name,
                        Category = t.Category.Name,
                        t.ViewCount,
                        t.CreatedDate
                    })
                    .ToList<dynamic>();

                technologiesDataGridView.DataSource = _originalTechData;
            }
        }

        private void LoadComments()
        {
            using (var db = new AppDbContext())
            {
                // Сначала получаем данные как список
                var comments = db.Comments
                    .Include(c => c.User)
                    .Include(c => c.Technology)
                    .OrderByDescending(c => c.DatePosted)
                    .ToList();

                _originalCommentsData = comments
                    .Select(c => new
                    {
                        c.Id,
                        Technology = c.Technology.Name,
                        User = c.User.Username,
                        Text = c.Text.Length > 100 ? c.Text.Substring(0, 100) + "..." : c.Text,
                        c.Rating,
                        Date = c.DatePosted.ToString("dd.MM.yyyy HH:mm")
                    })
                    .ToList<dynamic>();

                commentsDataGridView.DataSource = _originalCommentsData;
            }
        }

        private void blockUserButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count == 0) return;

            int userId = (int)usersDataGridView.SelectedRows[0].Cells["Id"].Value;

            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(userId);
                user.IsBlocked = !user.IsBlocked;
                db.SaveChanges();

                LoadUsers();

                MessageBox.Show($"Пользователь {(user.IsBlocked ? "заблокирован" : "разблокирован")}",
                              "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void deleteUserButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count == 0) return;

            int userId = (int)usersDataGridView.SelectedRows[0].Cells["Id"].Value;

            if (MessageBox.Show("Удалить этого пользователя?", "Подтверждение",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.Find(userId);
                    db.Users.Remove(user);
                    db.SaveChanges();

                    LoadUsers();
                }
            }
        }

        private void refreshUsersButton_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void addTechButton_Click(object sender, EventArgs e)
        {
            var editForm = new TechnologyEditForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadTechnologies();
            }
        }

        private void editTechButton_Click(object sender, EventArgs e)
        {
            if (technologiesDataGridView.SelectedRows.Count == 0) return;

            int techId = (int)technologiesDataGridView.SelectedRows[0].Cells["Id"].Value;

            using (var db = new AppDbContext())
            {
                var tech = db.Technologies.Find(techId);
                var editForm = new TechnologyEditForm(tech);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadTechnologies();
                }
            }
        }

        private void deleteTechButton_Click(object sender, EventArgs e)
        {
            if (technologiesDataGridView.SelectedRows.Count == 0) return;

            int techId = (int)technologiesDataGridView.SelectedRows[0].Cells["Id"].Value;

            if (MessageBox.Show("Удалить эту технологию?", "Подтверждение",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    var tech = db.Technologies.Find(techId);
                    db.Technologies.Remove(tech);
                    db.SaveChanges();

                    LoadTechnologies();
                }
            }
        }

        private void refreshTechButton_Click(object sender, EventArgs e)
        {
            LoadTechnologies();
        }

        private void approveCommentButton_Click(object sender, EventArgs e)
        {
            if (commentsDataGridView.SelectedRows.Count == 0) return;

            int commentId = (int)commentsDataGridView.SelectedRows[0].Cells["Id"].Value;

            using (var db = new AppDbContext())
            {
                var comment = db.Comments.Find(commentId);
                comment.IsApproved = true;
                db.SaveChanges();

                LoadComments();
            }
        }

        private void DeleteSelectedComments(object sender, EventArgs e)
        {
            if (commentsDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите комментарии для удаления", "Информация",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedIds = commentsDataGridView.SelectedRows
                .Cast<DataGridViewRow>()
                .Select(r => (int)r.Cells["Id"].Value)
                .ToList();

            string message = $"Вы действительно хотите удалить {selectedIds.Count} комментариев?";
            if (MessageBox.Show(message, "Подтверждение удаления",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    foreach (var id in selectedIds)
                    {
                        var comment = db.Comments.Find(id);
                        db.Comments.Remove(comment);
                    }
                    db.SaveChanges();
                    LoadComments();
                }
            }
        }

        private void refreshCommentsButton_Click(object sender, EventArgs e)
        {
            LoadComments();
        }

        private void generateReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = fromDatePicker.Value.Date;
                DateTime toDate = toDatePicker.Value.Date.AddDays(1).AddSeconds(-1);

                if (fromDate > toDate)
                {
                    MessageBox.Show("Дата 'ОТ' не может быть больше даты 'ДО'");
                    return;
                }

                using (var db = new AppDbContext())
                {
                    reportDataGridView.DataSource = null;

                    if (reportTypeComboBox.SelectedIndex == 0)
                    {
                        var reportData = db.Technologies
                            .Include(t => t.Category)
                            .Where(t => t.CreatedDate >= fromDate && t.CreatedDate <= toDate)
                            .OrderByDescending(t => t.ViewCount)
                            .AsEnumerable()
                            .Select(t => new
                            {
                                Technology = t.Name,
                                Views = t.ViewCount,
                                Category = t.Category.Name,
                                CreatedDate = t.CreatedDate.ToString("dd.MM.yyyy")
                            })
                            .ToList();

                        reportDataGridView.DataSource = reportData;
                    }
                    else 
                    {
                        var reportData = db.Technologies
                            .Include(t => t.Category)
                            .Include(t => t.Comments)
                            .Select(t => new
                            {
                                Technology = t.Name,
                                AverageRating = t.Comments
                                    .Where(c => c.IsApproved &&
                                               c.DatePosted >= fromDate &&
                                               c.DatePosted <= toDate)
                                    .Average(c => (double?)c.Rating) ?? 0,
                                RatingCount = t.Comments
                                    .Count(c => c.IsApproved &&
                                               c.DatePosted >= fromDate &&
                                               c.DatePosted <= toDate),
                                Category = t.Category.Name
                            })
                            .Where(x => x.RatingCount > 0)
                            .OrderByDescending(x => x.AverageRating)
                            .ToList();

                        reportDataGridView.DataSource = reportData;
                    }

                    reportDataGridView.AutoGenerateColumns = false;
                    reportDataGridView.AutoGenerateColumns = true;
                    reportDataGridView.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportReportButton_Click(object sender, EventArgs e)
        {
            generateReportButton_Click(sender, e);

            // Затем экспортируем
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files|*.xlsx|CSV Files|*.csv";
                saveDialog.Title = "Экспорт отчета";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var data = reportDataGridView.DataSource as IEnumerable<dynamic>;

                        if (data == null || !data.Any())
                        {
                            MessageBox.Show("Нет данных для экспорта", "Ошибка",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (saveDialog.FileName.EndsWith(".xlsx"))
                        {
                            ExportToExcel(data, saveDialog.FileName);
                        }
                        else
                        {
                            ExportToCsv(data, saveDialog.FileName);
                        }

                        MessageBox.Show("Экспорт завершен успешно!", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ConfigureReportColumns()
        {
            if (reportDataGridView.Columns.Count == 0) return;

            foreach (DataGridViewColumn column in reportDataGridView.Columns)
            {
                column.Visible = false;
            }

            if (reportTypeComboBox.Text == "Самые просматриваемые технологии")
            {
                ShowAndConfigureColumn("Technology", "Технология");
                ShowAndConfigureColumn("Views", "Просмотры", true);
                ShowAndConfigureColumn("Category", "Категория");
                ShowAndConfigureColumn("CreatedDate", "Дата создания");
            }
            else if (reportTypeComboBox.Text == "Рейтинг технологий по оценкам")
            {
                ShowAndConfigureColumn("Technology", "Технология");
                ShowAndConfigureColumn("AverageRating", "Средний рейтинг", true, "N2");
                ShowAndConfigureColumn("RatingCount", "Кол-во оценок", true);
                ShowAndConfigureColumn("Category", "Категория");
            }

            reportDataGridView.Refresh();
        }

        private void ShowAndConfigureColumn(string name, string headerText, bool alignRight = false, string format = null)
        {
            if (reportDataGridView.Columns.Contains(name))
            {
                var column = reportDataGridView.Columns[name];
                column.Visible = true;
                column.HeaderText = headerText;

                if (alignRight)
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (!string.IsNullOrEmpty(format))
                {
                    column.DefaultCellStyle.Format = format;
                }
            }
        }

        private void ExportToCsv(IEnumerable<dynamic> data, string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer.WriteLine(string.Join(";", reportDataGridView.Columns
                        .Cast<DataGridViewColumn>()
                        .Select(c => c.HeaderText)));

                    foreach (var item in data)
                    {
                        var values = new List<string>();
                        foreach (DataGridViewColumn column in reportDataGridView.Columns)
                        {
                            var prop = column.DataPropertyName;
                            if (!string.IsNullOrEmpty(prop))
                            {
                                var value = GetDynamicProperty(item, prop);
                                values.Add(value?.ToString() ?? "");
                            }
                        }
                        writer.WriteLine(string.Join(";", values));
                    }
                }

                MessageBox.Show("Экспорт в CSV выполнен успешно!", "Успех",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте в CSV: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //            ExcelPackage.License.SetNonCommercialPersonal("MyReports");
        private void ExportToExcel(IEnumerable<dynamic> data, string filePath)
        {
            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("MyReports");

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Отчет");

                    for (int i = 0; i < reportDataGridView.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = reportDataGridView.Columns[i].HeaderText;
                    }

                    int row = 2;
                    foreach (var item in data)
                    {
                        for (int col = 0; col < reportDataGridView.Columns.Count; col++)
                        {
                            var prop = reportDataGridView.Columns[col].DataPropertyName;
                            if (!string.IsNullOrEmpty(prop))
                            {
                                var value = GetDynamicProperty(item, prop);
                                worksheet.Cells[row, col + 1].Value = value?.ToString();
                            }
                        }
                        row++;
                    }

                    package.SaveAs(new FileInfo(filePath));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте в Excel: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private object GetDynamicProperty(dynamic obj, string propertyName)
        {
            try
            {
                return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
            }
            catch
            {
                try
                {
                    return ((IDictionary<string, object>)obj)[propertyName];
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Перемещение формы
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void HeaderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void HeaderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void HeaderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void ConfigureDataGridView(Guna2DataGridView dgv)
        {
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(18, 18, 26);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(58, 12, 163);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(22, 22, 34);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            dgv.BackgroundColor = Color.FromArgb(18, 18, 26);
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 26, 42);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;

            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 40;
            dgv.EnableHeadersVisualStyles = false;
            dgv.Size = new Size(950, 540);
            dgv.Location = new Point(0, 80);
        }

        private void ConfigureCommentsDataGridView(Guna2DataGridView dgv)
        {
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(18, 18, 26);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(58, 12, 163);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(22, 22, 34);
            dgv.BackgroundColor = Color.FromArgb(18, 18, 26);
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 26, 42);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 40;
            dgv.Size = new Size(950, 540);
            dgv.Location = new Point(0, 100);
            dgv.MultiSelect = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #endregion
    }
}