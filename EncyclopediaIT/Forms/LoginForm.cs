using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncyclopediaIT.Models;
using System.Data.Entity;
using Guna.UI2.WinForms;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace EncyclopediaIT.Forms
{
    public partial class LoginForm : Form
    {
        private Guna2Panel mainPanel;
        private Guna2Panel headerPanel;
        private Guna2TextBox txtUsername;
        private Guna2TextBox txtPassword;
        private Guna2GradientButton btnLogin;
        private Guna2HtmlLabel lblNoAccount;
        private Guna2HtmlLabel lblCreateAccount;
        private Guna2CircleButton btnClose;
        private Guna2CircleButton btnMinimize;
        public Action<string> OnSuccessLogin { get; set; }
        public Action<string> OnReg { get; set; }
        public LoginForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Text = "Вход в систему";
            this.ClientSize = new Size(450, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(15, 15, 26);

            // Основная панель
            mainPanel = new Guna2Panel();
            mainPanel.Size = new Size(450, 600);
            mainPanel.BackColor = Color.FromArgb(26, 26, 46);
            mainPanel.BorderRadius = 10;
            mainPanel.BorderColor = Color.FromArgb(42, 42, 74);
            mainPanel.BorderThickness = 1;
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Shadow = new Padding(0, 0, 10, 10);
            mainPanel.ShadowDecoration.Color = Color.FromArgb(100, 0, 0, 0);

            // Панель заголовка
            headerPanel = new Guna2Panel();
            headerPanel.Size = new Size(450, 80);
            headerPanel.Location = new Point(0, 0);
            headerPanel.BackColor = Color.Transparent;
            headerPanel.FillColor = Color.Transparent;
            headerPanel.BorderRadius = 10;
            headerPanel.BorderThickness = 0;

            // Градиент для заголовка (как в MainForm)
            headerPanel.Paint += (sender, e) =>
            {
                using (var brush = new LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(110, 0, 255), // #6e00ff
                    Color.FromArgb(255, 0, 170), // #ff00aa
                    135f)) // Угол как в MainForm
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }
            };

            // Заголовок
            var lblTitle = new Guna2HtmlLabel();
            lblTitle.Text = "<span style='color:white; font-size:20px; font-weight:600'>Вход в систему</span>";
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(450, 80);
            lblTitle.Location = new Point(0, 0);
            lblTitle.TextAlignment = ContentAlignment.MiddleCenter;

            // Поле логина
            txtUsername = new Guna2TextBox();
            txtUsername.PlaceholderText = "Логин";
            txtUsername.Size = new Size(350, 45);
            txtUsername.Location = new Point(50, 120);
            txtUsername.BorderRadius = 6;
            txtUsername.FillColor = Color.FromArgb(36, 36, 64);
            txtUsername.BorderColor = Color.FromArgb(42, 42, 74);
            txtUsername.BorderThickness = 1;
            txtUsername.ForeColor = Color.White;
            txtUsername.Font = new Font("Segoe UI", 10);
            txtUsername.HoverState.BorderColor = Color.FromArgb(110, 0, 255);

            // Поле пароля
            txtPassword = new Guna2TextBox();
            txtPassword.PlaceholderText = "Пароль";
            txtPassword.Size = new Size(350, 45);
            txtPassword.Location = new Point(50, 190);
            txtPassword.BorderRadius = 6;
            txtPassword.FillColor = Color.FromArgb(36, 36, 64);
            txtPassword.BorderColor = Color.FromArgb(42, 42, 74);
            txtPassword.BorderThickness = 1;
            txtPassword.ForeColor = Color.White;
            txtPassword.Font = new Font("Segoe UI", 10);
            txtPassword.HoverState.BorderColor = Color.FromArgb(110, 0, 255);
            txtPassword.PasswordChar = '•';
            txtPassword.UseSystemPasswordChar = true;

            // Кнопка входа
            btnLogin = new Guna2GradientButton();
            btnLogin.Text = "Войти";
            btnLogin.Size = new Size(350, 45);
            btnLogin.Location = new Point(50, 280);
            btnLogin.BorderRadius = 6;
            btnLogin.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.FillColor = Color.FromArgb(110, 0, 255);
            btnLogin.FillColor2 = Color.FromArgb(255, 0, 170);
            btnLogin.Animated = true;
            btnLogin.ShadowDecoration.Enabled = true;
            btnLogin.ShadowDecoration.Shadow = new Padding(0, 0, 5, 5);
            btnLogin.ShadowDecoration.Color = Color.FromArgb(50, 0, 0, 0);
            btnLogin.Click += loginButton_Click;

            // Текст "Нет аккаунта?"
            lblNoAccount = new Guna2HtmlLabel();
            lblNoAccount.Text = "<span style='color:#AAAAAA; font-size:11px'>Нет аккаунта?</span>";
            lblNoAccount.AutoSize = false;
            lblNoAccount.Size = new Size(68, 30);
            lblNoAccount.Location = new Point(170, 350);

            // Ссылка "Создать"
            lblCreateAccount = new Guna2HtmlLabel();
            lblCreateAccount.Text = "<span style='color:#f72585; font-size:11px; cursor:pointer; font-weight:600'>Создать</span>";
            lblCreateAccount.AutoSize = false;
            lblCreateAccount.Size = new Size(80, 30);
            lblCreateAccount.Location = new Point(237, 350);
            lblCreateAccount.Cursor = Cursors.Hand;
            lblCreateAccount.Click += registerLinkLabel;

            // Кнопка закрытия
            btnClose = new Guna2CircleButton();
            btnClose.Size = new Size(30, 30);
            btnClose.Location = new Point(410, 15);
            btnClose.FillColor = Color.Transparent;
            btnClose.ForeColor = Color.White;
            btnClose.Text = "✕";
            btnClose.Font = new Font("Segoe UI", 10);
            btnClose.Animated = true;
            btnClose.HoverState.FillColor = Color.FromArgb(255, 50, 50);

            // Кнопка сворачивания
            btnMinimize = new Guna2CircleButton();
            btnMinimize.Size = new Size(30, 30);
            btnMinimize.Location = new Point(370, 15);
            btnMinimize.FillColor = Color.Transparent;
            btnMinimize.ForeColor = Color.White;
            btnMinimize.Text = "─";
            btnMinimize.Font = new Font("Segoe UI", 10);
            btnMinimize.Animated = true;
            btnMinimize.HoverState.FillColor = Color.FromArgb(70, 70, 100);

            // Добавление элементов
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(btnClose);
            headerPanel.Controls.Add(btnMinimize);
            mainPanel.Controls.Add(headerPanel);
            mainPanel.Controls.Add(txtUsername);
            mainPanel.Controls.Add(txtPassword);
            mainPanel.Controls.Add(btnLogin);
            mainPanel.Controls.Add(lblNoAccount);
            mainPanel.Controls.Add(lblCreateAccount);
            this.Controls.Add(mainPanel);

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == txtUsername.Text);


                    if (user != null && user.Password == HashPassword(txtPassword.Text))
                    {
                        if (user.IsBlocked)
                        {
                            MessageBox.Show("Вас заблокировали :(", "Ошибка входа",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else {
                            CurrentUser.Id = user.Id;
                            CurrentUser.Username = user.Username;
                            CurrentUser.IsAdmin = user.IsAdmin;
                            CurrentUser.IsAuthenticated = true;
                            new Guna2MessageDialog()
                            {
                                Text = "Вы успешно вошли в аккаунт!",
                                Caption = "Успех!",
                                Buttons = MessageDialogButtons.OK,
                                Icon = MessageDialogIcon.Information,
                                Style = MessageDialogStyle.Light
                            }.Show();
                            DialogResult = DialogResult.OK;
                            OnSuccessLogin?.Invoke(txtUsername.Text);
                            Close();
                        }
                    }
                    else
                    {
                        new Guna2MessageDialog()
                        {
                            Text = "Неверное имя пользователя или пароль!",
                            Caption = "Ошибка",
                            Buttons = MessageDialogButtons.OK,
                            Icon = MessageDialogIcon.Warning,
                            Style = MessageDialogStyle.Dark
                        }.Show();
                    }
                }
            }
        }

        private bool ValidateInput()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                new Guna2MessageDialog()
                {
                    Text = "Введите логин!",
                    Caption = "Ошибка",
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Warning,
                    Style = MessageDialogStyle.Dark
                }.Show();
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                new Guna2MessageDialog()
                {
                    Text = "Введите пароль!",
                    Caption = "Ошибка",
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Warning,
                    Style = MessageDialogStyle.Dark
                }.Show();
                isValid = false;
            }

            return isValid;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private void registerLinkLabel(object sender, EventArgs e)
        {
            OnReg?.Invoke(txtUsername.Text);
            this.Hide();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
