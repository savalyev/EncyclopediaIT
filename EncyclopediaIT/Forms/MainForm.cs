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
using EncyclopediaIT.Forms;
using Guna.UI2.WinForms;
using System.Diagnostics;
using System.IO;

namespace EncyclopediaIT
{
    public partial class MainForm : Form
    {

        Guna2Panel headerPanel = new Guna2Panel();
        Label lblTitle = new Label();
        Guna2Panel navPanel = new Guna2Panel();
        Label navHeader = new Label();
        Guna2Button btnHome = new Guna2Button();
        Guna2Button btnLogin = new Guna2Button();
        Guna2Button btnRegister = new Guna2Button();
        Guna2Button btnCatalog = new Guna2Button();
        Guna2Button btnBookmarks = new Guna2Button();
        Guna2Button btnExit = new Guna2Button();
        Guna2Button btnAdminPanel = new Guna2Button();
        Guna2Button btnProfile = new Guna2Button();
        Guna2Panel contentPanel = new Guna2Panel();

        Label welcomeLabel = new Label();
        Label accentLabel = new Label();


        public MainForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            UpdateMenuItems();

            // главное окно
            this.Text = "IT ENCYCLOPEDIA";
            this.Size = new Size(1200, 800);
            this.BackColor = Color.FromArgb(15, 15, 26);
            this.Font = new Font("Segoe UI", 9);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.HelpButton = true;
            this.HelpRequested += MainForm_HelpRequested;



            headerPanel = new Guna2Panel()
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.Transparent
            };

            headerPanel.Paint += (sender, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(110, 0, 255), // #6e00ff
                    Color.FromArgb(255, 0, 170), // #ff00aa
                    135f))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }
            };

            lblTitle = new Label()
            {
                Text = "IT ENCYCLOPEDIA",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 10, 0, 0)
            };

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
            headerPanel.Controls.Add(closeButton);

            // Боковая панель навигации
            navPanel = new Guna2Panel()
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Color.FromArgb(26, 26, 46),
                BorderColor = Color.FromArgb(42, 42, 74),
                BorderThickness = 1,
                BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid
            };

            // Заголовок навигации
            navHeader = new Label()
            {
                Text = "НАВИГАЦИЯ",
                TextAlign= ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(110, 0, 255),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 30,
                Margin = new Padding(0, 0, 0, 10)
            };

            // Кнопки навигации
            bool isLoggedIn = CurrentUser.IsAuthenticated;
            bool isAdmin = CurrentUser.IsAdmin;
            btnHome = new Guna2Button()
            {
                Text = "Главная",
                FillColor = Color.FromArgb(110, 0, 255),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45,
                Margin = new Padding(0, 0, 0, 8),
                BorderRadius = 6,
                Font = new Font("Segoe UI", 10)
            };
            btnHome.Click += HomeClick;

            btnLogin = new Guna2Button()
            {
                Text = "Авторизация",
                FillColor = Color.Transparent,
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45,
                BorderRadius = 6,
                Font = new Font("Segoe UI", 10),
                Visible = true,
            };
            btnLogin.Click += loginClick;

            btnRegister = new Guna2Button()
            {
                Text = "Регистрация",
                FillColor = Color.Transparent,
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45,
                BorderRadius = 6,
                Font = new Font("Segoe UI", 10),
                Visible = true
            };
            btnRegister.Click += registerClick;

            btnCatalog = new Guna2Button()
            {
                Text = "Каталог",
                FillColor = Color.Transparent,
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45,
                BorderRadius = 6,
                Font = new Font("Segoe UI", 10),
                Visible = false
            };
            btnCatalog.Click += catalogClick;

            btnAdminPanel = new Guna2Button()
            {
                Text = "Панель администратора",
                FillColor = Color.Transparent,
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45,
                BorderRadius = 6,
                Font = new Font("Segoe UI", 10),
                Visible = false
            };
            btnAdminPanel.Click += AdminClick;

            btnProfile = new Guna2Button()
            {
                Text = "Личный кабинет",
                FillColor = Color.Transparent,
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45,
                BorderRadius = 6,
                Font = new Font("Segoe UI", 10),
                Visible = false
            };
            btnProfile.Click += ProfileClick;

            btnExit = new Guna2Button()
            {
                Text = "Выйти",
                FillColor = Color.Transparent,
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 45,
                BorderRadius = 6,
                Font = new Font("Segoe UI", 10),
                Visible = false
            };
            btnExit.Click += exitClick;

            // Основная контентная область
            contentPanel = new Guna2Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 15, 26),
                AutoScroll = true
            };

            // Приветственный текст
            welcomeLabel = new Label()
            {
                Text = "Добро пожаловать в\r\n\tВсе самое интересное из мира IT",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Regular),
                Location = new Point(30, 30),
                AutoSize = true
            };

            // Акцентная часть текста
            accentLabel = new Label()
            {
                Text = "энциклопедию",
                ForeColor = Color.FromArgb(110, 0, 255),
                Font = new Font("Segoe UI", 18, FontStyle.Regular),
                Location = new Point(270, 30),
                AutoSize = true
            };

            // Собираем интерфейс
            headerPanel.Controls.Add(lblTitle);
            navPanel.Controls.Add(btnExit);
            navPanel.Controls.Add(btnAdminPanel);
            navPanel.Controls.Add(btnProfile);
            navPanel.Controls.Add(btnLogin);
            navPanel.Controls.Add(btnRegister);
            navPanel.Controls.Add(btnCatalog);
            navPanel.Controls.Add(btnHome);
            navPanel.Controls.Add(navHeader);

            contentPanel.Controls.Add(accentLabel);
            contentPanel.Controls.Add(welcomeLabel);

            this.Controls.Add(contentPanel);
            this.Controls.Add(navPanel);
            this.Controls.Add(headerPanel);

            // Тень для формы
            var shadow = new Guna2ShadowForm(this);
            UpdateMenuItems();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1 && File.Exists(Program.HelpFilePath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = @"C:\Windows\hh.exe",
                        Arguments = $"\"{Program.HelpFilePath}\"",
                        UseShellExecute = true
                    });
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка открытия справки:\n{ex.Message}",
                                  "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void loginClick(object sender, EventArgs e)
        {
            btnLogin.FillColor = Color.FromArgb(110, 0, 255);
            btnHome.FillColor = Color.Transparent;
            btnRegister.FillColor = Color.Transparent;

            contentPanel.Controls.Clear();
            var loginForm = new LoginForm();
            loginForm.TopLevel = false;
            contentPanel.Controls.Add(loginForm);
            loginForm.Location = new Point(
        (contentPanel.Width - loginForm.Width) / 2,
        (contentPanel.Height - loginForm.Height) / 2
    );
            loginForm.OnSuccessLogin = (username) =>
            {
                UpdateMenuItems();
            };
            loginForm.OnReg = (username) =>
            {
                registerClick(sender, e);
            };
            loginForm.Show();
        }

        private void registerClick(object sender, EventArgs e)
        {
            btnLogin.FillColor = Color.Transparent;
            btnHome.FillColor = Color.Transparent;
            btnRegister.FillColor = Color.FromArgb(110, 0, 255);
            contentPanel.Controls.Clear();
            var registerForm = new RegisterForm();
            registerForm.TopLevel = false;
            contentPanel.Controls.Add(registerForm);
            registerForm.Location = new Point(
        (contentPanel.Width - registerForm.Width) / 2,
        (contentPanel.Height - registerForm.Height) / 2
    );
            registerForm.OnLogin = (username) =>
            {
                loginClick(sender, e);
            };
            registerForm.Show();

        }
        private void MainForm_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Help.ShowHelp(this, "Help.chm", HelpNavigator.Topic, "MainForm.htm");
        }

        private void HomeClick(object sender, EventArgs e)
        {
            btnLogin.FillColor = Color.Transparent;
            btnHome.FillColor = Color.FromArgb(110, 0, 255);
            btnRegister.FillColor = Color.Transparent;
            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(accentLabel);
            contentPanel.Controls.Add(welcomeLabel);

        }

        private void catalogClick(object sender, EventArgs e)
        {
            var catalogForm = new CatalogForm();
            catalogForm.ShowDialog();
        }


        private void exitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void UpdateMenuItems()
        {
            bool isLoggedIn = CurrentUser.IsAuthenticated;
            bool isAdmin = CurrentUser.IsAdmin;

            btnLogin.Visible = !isLoggedIn;
            btnRegister.Visible = !isLoggedIn;

            btnCatalog.Visible = isLoggedIn;
            btnExit.Visible = isLoggedIn;
            btnProfile.Visible = isLoggedIn;
            btnAdminPanel.Visible = isLoggedIn && isAdmin;

        }

        private void kabClick(object sender, EventArgs e)
        {
            var userPrifileForm = new UserProfileForm();
            userPrifileForm.ShowDialog();
        }

        private void AdminClick(object sender, EventArgs e)
        {
            var adminDashboardForm = new AdminDashboardForm();
            adminDashboardForm.ShowDialog();
        }

        private void ProfileClick(object sender, EventArgs e)
        {
            var profile = new UserProfileForm();
            profile.ShowDialog();
        }

    };
    }
