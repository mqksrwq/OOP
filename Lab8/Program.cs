using System.Text;
using System.Runtime.InteropServices;

namespace Lab8;

/// <summary>
/// Точка входа Windows Forms приложения Lab8.
/// </summary>
static class Program
{
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    private static extern bool FreeConsole();

    /// <summary>
    ///  Точка входа в приложение
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        using var startupDialog = new StartupModeDialog();
        var modeResult = startupDialog.ShowDialog();
        if (modeResult != DialogResult.OK || startupDialog.SelectedMode == StartupMode.None)
            return;

        if (startupDialog.SelectedMode == StartupMode.WinForms)
        {
            Application.Run(new FormMain());
            return;
        }

        AllocConsole();
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        ConsoleMenu.Run();

        Console.WriteLine();
        Console.WriteLine("Нажмите Enter для завершения...");
        Console.ReadLine();
        FreeConsole();
    }
}


