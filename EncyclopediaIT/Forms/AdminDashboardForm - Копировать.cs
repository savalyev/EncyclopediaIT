using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncyclopediaIT.Models;
using OfficeOpenXml;
using System.Data.Entity;

namespace EncyclopediaIT.Forms
{
    public partial class AdminDashboardForm : Form
    {
        public AdminDashboardForm()
        {
            InitializeComponent();
            LoadUsers();
            LoadTechnologies();
            LoadComments();

            reportTypeComboBox.Items.AddRange(new object[] {
            "Самые просматриваемые технологии",
            "Рейтинг технологий по оценкам"
        });
            reportTypeComboBox.SelectedIndex = 0;
        }

        private void LoadUsers()
        {
            using (var db = new AppDbContext())
            {
                usersDataGridView.DataSource = db.Users
                    .Select(u => new
                    {
                        u.Id,
                        u.Username,
                        u.Email,
                        u.RegistrationDate,
                        u.IsAdmin,
                        IsBlocked = u.IsBlocked
                    })
                    .ToList();
            }
        }

        private void LoadTechnologies()
        {
            using (var db = new AppDbContext())
            {
                technologiesDataGridView.DataSource = db.Technologies
                    .Include(t => t.Category)
                    .Select(t => new
                    {
                        t.Id,
                        t.Name,
                        Category = t.Category.Name,
                        t.ViewCount,
                        t.CreatedDate
                    })
                    .ToList();
            }
        }

        private void LoadComments()
        {
            using (var db = new AppDbContext())
            {
                commentsDataGridView.DataSource = db.Comments
                    .Include(c => c.User)
                    .Include(c => c.Technology)
                    .Where(c => !c.IsApproved)
                    .Select(c => new
                    {
                        c.Id,
                        Technology = c.Technology.Name,
                        User = c.User.Username,
                        c.Text,
                        c.Rating,
                        c.DatePosted
                    })
                    .ToList();
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

        private void deleteCommentButton_Click(object sender, EventArgs e)
        {
            if (commentsDataGridView.SelectedRows.Count == 0) return;

            int commentId = (int)commentsDataGridView.SelectedRows[0].Cells["Id"].Value;

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

        private void refreshCommentsButton_Click(object sender, EventArgs e)
        {
            LoadComments();
        }

        private void generateReportButton_Click(object sender, EventArgs e)
        {
            DateTime fromDate = fromDatePicker.Value;
            DateTime toDate = toDatePicker.Value;

            if (fromDate > toDate)
            {
                MessageBox.Show("Дата 'С' не может быть позже даты 'По'", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                switch (reportTypeComboBox.SelectedIndex)
                {
                    case 0: // Самые просматриваемые
                        reportDataGridView.DataSource = db.Technologies
                            .Where(t => t.CreatedDate >= fromDate && t.CreatedDate <= toDate)
                            .OrderByDescending(t => t.ViewCount)
                            .Select(t => new
                            {
                                t.Name,
                                t.ViewCount,
                                Category = t.Category.Name,
                                t.CreatedDate
                            })
                            .ToList();
                        break;

                    case 1: // Рейтинг по оценкам
                        reportDataGridView.DataSource = db.Technologies
                            .Select(t => new
                            {
                                t.Name,
                                AverageRating = db.Comments
                                    .Where(c => c.TechnologyId == t.Id && c.IsApproved)
                                    .Average(c => (double?)c.Rating) ?? 0,
                                RatingCount = db.Comments
                                    .Count(c => c.TechnologyId == t.Id && c.IsApproved),
                                Category = t.Category.Name
                            })
                            .OrderByDescending(t => t.AverageRating)
                            .ToList();
                        break;
                }
            }
        }

        private void exportReportButton_Click(object sender, EventArgs e)
        {
            if (reportDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV файл (*.csv)|*.csv|Excel файл (*.xlsx)|*.xlsx",
                Title = "Экспорт отчета"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (saveFileDialog.FileName.EndsWith(".csv"))
                    {
                        ExportToCsv(reportDataGridView, saveFileDialog.FileName);
                    }
                    else
                    {
                        ExportToExcel(reportDataGridView, saveFileDialog.FileName);
                    }

                    MessageBox.Show("Экспорт успешно завершен", "Успех",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToCsv(DataGridView dataGridView, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Заголовки столбцов
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    writer.Write(dataGridView.Columns[i].HeaderText);
                    if (i < dataGridView.Columns.Count - 1)
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine();

                // Данные
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        writer.Write(row.Cells[i].Value?.ToString());
                        if (i < dataGridView.Columns.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();
                }
            }
        }

        [Obsolete]
        private void ExportToExcel(DataGridView dataGridView, string filePath)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Отчет");

                // Заголовки
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dataGridView.Columns[i].HeaderText;
                }

                // Данные
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = dataGridView.Rows[i].Cells[j].Value;
                    }
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }
    }
}
