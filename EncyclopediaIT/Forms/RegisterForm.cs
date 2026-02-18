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

namespace EncyclopediaIT.Forms
{
    public partial class RegisterForm : Form
    {

        private Guna2Panel mainPanel;
        private Guna2Panel headerPanel;
        private Guna2TextBox txtUsername;
        private Guna2TextBox txtEmail;
        private Guna2TextBox txtPassword;
        private Guna2GradientButton btnRegister;
        private Guna2HtmlLabel lblHaveAccount;
        private Guna2HtmlLabel lblLoginLink;
        private Guna2CircleButton btnClose;
        private Guna2CircleButton btnMinimize;
        public Action<string> OnLogin { get; set; }
        public RegisterForm()
        {
            InitializeComponent();
            // Настройка основной формы
            this.Text = "Регистрация";
            this.ClientSize = new Size(450, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(13, 17, 23);

            // Основная панель
            mainPanel = new Guna2Panel();
            mainPanel.Size = new Size(400, 450);
            mainPanel.Location = new Point(25, 25);
            mainPanel.BackColor = Color.FromArgb(13, 17, 23, 204);
            mainPanel.BorderRadius = 15;
            mainPanel.BorderColor = Color.FromArgb(48, 54, 61);
            mainPanel.BorderThickness = 1;
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Shadow = new Padding(0, 0, 10, 10);
            mainPanel.ShadowDecoration.Color = Color.FromArgb(100, 0, 0, 0);

            // Панель заголовка
            headerPanel = new Guna2Panel();
            headerPanel.Size = new Size(400, 70);
            headerPanel.Location = new Point(0, 0);
            headerPanel.BackColor = Color.Transparent;
            headerPanel.FillColor = Color.Transparent;
            headerPanel.BorderRadius = 15;
            headerPanel.BorderThickness = 0;

            // Градиент для заголовка
            headerPanel.Paint += (sender, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, headerPanel.Width, headerPanel.Height),
                    Color.FromArgb(58, 12, 163), // #3a0ca3
                    Color.FromArgb(76, 201, 240), // #4cc9f0
                    0f)) // Горизонтальный градиент
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }
            };

            // Заголовок
            var lblTitle = new Guna2HtmlLabel();
            lblTitle.Text = "<span style='color:white; font-size:20px; font-weight:600'>Регистрация</span>";
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(400, 70);
            lblTitle.Location = new Point(0, 0);
            lblTitle.TextAlignment = ContentAlignment.MiddleCenter;

            // Поле имени пользователя
            txtUsername = new Guna2TextBox();
            txtUsername.PlaceholderText = "Имя пользователя";
            txtUsername.Size = new Size(350, 40);
            txtUsername.Location = new Point(25, 100);
            txtUsername.BorderRadius = 6;
            txtUsername.FillColor = Color.FromArgb(22, 27, 34);
            txtUsername.BorderColor = Color.FromArgb(48, 54, 61);
            txtUsername.BorderThickness = 1;
            txtUsername.ForeColor = Color.White;
            txtUsername.Font = new Font("Segoe UI", 10);
            txtUsername.HoverState.BorderColor = Color.FromArgb(88, 166, 255);

            // Поле email
            txtEmail = new Guna2TextBox();
            txtEmail.PlaceholderText = "Email";
            txtEmail.Size = new Size(350, 40);
            txtEmail.Location = new Point(25, 160);
            txtEmail.BorderRadius = 6;
            txtEmail.FillColor = Color.FromArgb(22, 27, 34);
            txtEmail.BorderColor = Color.FromArgb(48, 54, 61);
            txtEmail.BorderThickness = 1;
            txtEmail.ForeColor = Color.White;
            txtEmail.Font = new Font("Segoe UI", 10);
            txtEmail.HoverState.BorderColor = Color.FromArgb(88, 166, 255);

            // Поле пароля
            txtPassword = new Guna2TextBox();
            txtPassword.PlaceholderText = "Пароль";
            txtPassword.Size = new Size(350, 40);
            txtPassword.Location = new Point(25, 220);
            txtPassword.BorderRadius = 6;
            txtPassword.FillColor = Color.FromArgb(22, 27, 34);
            txtPassword.BorderColor = Color.FromArgb(48, 54, 61);
            txtPassword.BorderThickness = 1;
            txtPassword.ForeColor = Color.White;
            txtPassword.Font = new Font("Segoe UI", 10);
            txtPassword.HoverState.BorderColor = Color.FromArgb(88, 166, 255);
            txtPassword.PasswordChar = '•';
            txtPassword.UseSystemPasswordChar = true;

            // Кнопка регистрации
            btnRegister = new Guna2GradientButton();
            btnRegister.Text = "Зарегистрироваться";
            btnRegister.Size = new Size(350, 40);
            btnRegister.Location = new Point(25, 310);
            btnRegister.BorderRadius = 6;
            btnRegister.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.FillColor = Color.FromArgb(35, 134, 54); // #238636
            btnRegister.FillColor2 = Color.FromArgb(35, 134, 54);
            btnRegister.Animated = true;
            btnRegister.ShadowDecoration.Enabled = true;
            btnRegister.ShadowDecoration.Shadow = new Padding(0, 0, 5, 5);
            btnRegister.ShadowDecoration.Color = Color.FromArgb(50, 0, 0, 0);
            btnRegister.Click += registerButton_Click;

            // Текст "Уже есть аккаунт?"
            lblHaveAccount = new Guna2HtmlLabel();
            lblHaveAccount.Text = "<span style='color:#8b949e; font-size:11px'>Уже есть аккаунт?</span>";
            lblHaveAccount.AutoSize = false;
            lblHaveAccount.Size = new Size(90, 30);
            lblHaveAccount.Location = new Point(140, 360);

            // Ссылка "Войти"
            lblLoginLink = new Guna2HtmlLabel();
            lblLoginLink.Text = "<span style='color:#58a6ff; font-size:11px; cursor:pointer; font-weight:600'>Войти</span>";
            lblLoginLink.AutoSize = false;
            lblLoginLink.Size = new Size(60, 30);
            lblLoginLink.Location = new Point(230, 360);
            lblLoginLink.Cursor = Cursors.Hand;
            lblLoginLink.Click += LoginLinkLabel;

            // Кнопка закрытия
            btnClose = new Guna2CircleButton();
            btnClose.Size = new Size(30, 30);
            btnClose.Location = new Point(360, 20);
            btnClose.FillColor = Color.Transparent;
            btnClose.ForeColor = Color.White;
            btnClose.Text = "✕";
            btnClose.Font = new Font("Segoe UI", 10);
            btnClose.Animated = true;
            btnClose.HoverState.FillColor = Color.FromArgb(255, 50, 50);

            // Добавление элементов
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(btnClose);
            mainPanel.Controls.Add(headerPanel);
            mainPanel.Controls.Add(txtUsername);
            mainPanel.Controls.Add(txtEmail);
            mainPanel.Controls.Add(txtPassword);
            mainPanel.Controls.Add(btnRegister);
            mainPanel.Controls.Add(lblHaveAccount);
            mainPanel.Controls.Add(lblLoginLink);
            this.Controls.Add(mainPanel);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                using (var db = new AppDbContext())
                {
                    if (db.Users.Any(u => u.Username == txtUsername.Text))
                    {
                        new Guna2MessageDialog()
                        {
                            Text = "Имя пользователя уже занято",
                            Caption = "Ошибка",
                            Buttons = MessageDialogButtons.OK,
                            Icon = MessageDialogIcon.Warning,
                            Style = MessageDialogStyle.Dark
                        }.Show();
                        return;
                    }

                    var user = new User
                    {
                        Username = txtUsername.Text,
                        Email = txtEmail.Text,
                        Password = HashPassword(txtPassword.Text),
                        RegistrationDate = DateTime.Now,
                        IsAdmin = false
                    };

                    db.Users.Add(user);
                    db.SaveChanges();
                    new Guna2MessageDialog()
                    {
                        Text = "Регистрация успешно завершена! Войдите в аккаунт!",
                        Caption = "Успех!",
                        Buttons = MessageDialogButtons.OK,
                        Icon = MessageDialogIcon.Information,
                        Style = MessageDialogStyle.Light
                    }.Show();
                    Close();
                }
            }
        }

        private bool ValidateInput()
        {
            bool isValid = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text.Length < 4)
            {
                new Guna2MessageDialog()
                {
                    Text = "Имя пользователя должно содержать минимум 4 символа",
                    Caption = "Ошибка",
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Warning,
                    Style = MessageDialogStyle.Dark
                }.Show();
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                new Guna2MessageDialog()
                {
                    Text = "Введите корректный email",
                    Caption = "Ошибка",
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Warning,
                    Style = MessageDialogStyle.Dark
                }.Show();
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length < 6)
            {
                new Guna2MessageDialog()
                {
                    Text = "Пароль должен содержать минимум 6 символов",
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

        private void LoginLinkLabel(object sender, EventArgs e)
        {
            OnLogin?.Invoke(txtUsername.Text);
            this.Hide();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
