namespace Lab2;

static class Program
{
    /// <summary>
    ///  Точка входа в приложение
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}