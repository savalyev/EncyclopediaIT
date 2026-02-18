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
using System.Diagnostics;

namespace EncyclopediaIT.Forms
{
    public partial class UserProfileForm : Form
    {
        public UserProfileForm()
        {
            InitializeComponent();
            LoadUserData();
            LoadBookmarks();
            LoadComments();
        }

        private void LoadUserData()
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(CurrentUser.Id);

                usernameTextBox.Text = user.Username;
                emailTextBox.Text = user.Email;
                registrationDateTextBox.Text = user.RegistrationDate.ToString("dd.MM.yyyy");
                
            }
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
                        .ToList();

                    if (bookmarksListView == null)
                    {
                        MessageBox.Show("Ошибка инициализации списка закладок");
                        return;
                    }

                    bookmarksListView.Items.Clear();

                    foreach (var bookmark in bookmarks)
                    {
                        if (bookmark?.Technology == null)
                        {
                            continue;
                        }

                        var item = new ListViewItem(bookmark.Technology.Name ?? "Без названия");

                        string categoryName = bookmark.Technology.Category?.Name ?? "Без категории";
                        item.SubItems.Add(categoryName);

                        item.SubItems.Add(bookmark.DateAdded.ToString("dd.MM.yyyy"));
                        item.Tag = bookmark.TechnologyId;

                        bookmarksListView.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки закладок: {ex.Message}");
            }
            }

        private void LoadComments()
        {
            using (var db = new AppDbContext())
            {
                var comments = db.Comments
                    .Include(c => c.Technology)
                    .Where(c => c.UserId == CurrentUser.Id)
                    .ToList();

                commentsListView.Items.Clear();

                foreach (var comment in comments)
                {
                    var item = new ListViewItem(comment.Technology.Name);
                    item.SubItems.Add(comment.Text);
                    item.SubItems.Add(comment.Rating.ToString());
                    item.SubItems.Add(comment.DatePosted.ToString("dd.MM.yyyy"));
                    item.Tag = comment.Id;

                    commentsListView.Items.Add(item);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailTextBox.Text) || !emailTextBox.Text.Contains("@"))
            {
                MessageBox.Show("Введите корректный email", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(CurrentUser.Id);
                user.Email = emailTextBox.Text;
                db.SaveChanges();

                MessageBox.Show("Данные успешно сохранены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void openBookmarkButton_Click(object sender, EventArgs e)
        {
            if (bookmarksListView.SelectedItems.Count == 0) return;

            int technologyId = (int)bookmarksListView.SelectedItems[0].Tag;
            var technologyCard = new TechnologyCardForm(technologyId);
            technologyCard.ShowDialog();
        }

        private void removeBookmarkButton_Click(object sender, EventArgs e)
        {
            if (bookmarksListView.SelectedItems.Count == 0) return;

            int technologyId = (int)bookmarksListView.SelectedItems[0].Tag;

            using (var db = new AppDbContext())
            {
                var bookmark = db.Bookmarks.FirstOrDefault(b => b.UserId == CurrentUser.Id && b.TechnologyId == technologyId);

                if (bookmark != null)
                {
                    db.Bookmarks.Remove(bookmark);
                    db.SaveChanges();
                    LoadBookmarks();
                }
            }
        }

        private void editCommentButton_Click(object sender, EventArgs e)
        {
            if (commentsListView.SelectedItems.Count == 0) return;

            int commentId = (int)commentsListView.SelectedItems[0].Tag;

            using (var db = new AppDbContext())
            {
                var comment = db.Comments.Find(commentId);

                var editForm = new CommentEditForm(comment);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadComments();
                }
            }
        }

        private void deleteCommentButton_Click(object sender, EventArgs e)
        {
            if (commentsListView.SelectedItems.Count == 0) return;

            int commentId = (int)commentsListView.SelectedItems[0].Tag;

            if (MessageBox.Show("Удалить этот комментарий?", "Подтверждение",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    var comment = db.Comments.Find(commentId);
                    db.Comments.Remove(comment);
                    db.SaveChanges();
                    LoadComments();
                }
            }
        }

        private void commentsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Активируем/деактивируем кнопки в зависимости от выбора
            bool isSelected = commentsListView.SelectedItems.Count > 0;
            editCommentButton.Enabled = isSelected;
            deleteCommentButton.Enabled = isSelected;

            // Если есть выделенный элемент, загружаем данные для предпросмотра
            if (isSelected)
            {
                int commentId = (int)commentsListView.SelectedItems[0].Tag;
                using (var db = new AppDbContext())
                {
                    var comment = db.Comments.Find(commentId);
                    commentPreviewTextBox.Text = comment.Text;
                    commentRatingLabel.Text = $"Оценка: {comment.Rating}/5";
                }
            }
            else
            {
                commentPreviewTextBox.Clear();
                commentRatingLabel.Text = "Оценка: не выбрано";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
