using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using EncyclopediaIT;

static class Program
{
    public static string HelpFilePath { get; private set; }

    [STAThread]
    static void Main()
    {
        // 1. Инициализация приложения
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // 2. Установка пути к справке
        HelpFilePath = Path.Combine(Application.StartupPath, "Help.chm");

        // 3. Проверка существования файла
        if (!File.Exists(HelpFilePath))
        {
            MessageBox.Show($"Файл справки не найден:\n{HelpFilePath}",
                          "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // 4. Инициализация БД
        using (var db = new AppDbContext())
        {
            db.Database.Initialize(true);
        }

        // 5. Запуск главной формы
        Application.Run(new MainForm());
    }
}