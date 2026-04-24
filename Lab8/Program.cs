using System.Text;

namespace Lab8;

/// <summary>
/// Точка входа Windows Forms приложения Lab8.
/// </summary>
static class Program
{
    /// <summary>
    ///  Точка входа в приложение
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Установка кодировки в консоли для событий
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        ApplicationConfiguration.Initialize();
        Application.Run(new FormMain());
    }
}


