using System.Drawing;
using System.Windows.Forms;

namespace Lab8;

internal enum StartupMode
{
    None = 0,
    WinForms = 1,
    Console = 2
}

internal sealed class StartupModeDialog : Form
{
    public StartupMode SelectedMode { get; private set; } = StartupMode.None;

    public StartupModeDialog()
    {
        Text = "Lab8: выбор режима";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(420, 170);

        var label = new Label
        {
            AutoSize = false,
            Dock = DockStyle.Top,
            Height = 70,
            TextAlign = ContentAlignment.MiddleCenter,
            Text = "Выберите режим запуска приложения"
        };

        var buttonsPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Padding = new Padding(16),
            AutoSize = false
        };

        var formButton = new Button
        {
            Text = "Форма",
            AutoSize = true,
            MinimumSize = new Size(110, 36)
        };
        formButton.Click += (_, _) =>
        {
            SelectedMode = StartupMode.WinForms;
            DialogResult = DialogResult.OK;
            Close();
        };

        var consoleButton = new Button
        {
            Text = "Консоль",
            AutoSize = true,
            MinimumSize = new Size(110, 36)
        };
        consoleButton.Click += (_, _) =>
        {
            SelectedMode = StartupMode.Console;
            DialogResult = DialogResult.OK;
            Close();
        };

        var exitButton = new Button
        {
            Text = "Выход",
            AutoSize = true,
            MinimumSize = new Size(110, 36)
        };
        exitButton.Click += (_, _) =>
        {
            SelectedMode = StartupMode.None;
            DialogResult = DialogResult.Cancel;
            Close();
        };

        buttonsPanel.Controls.Add(formButton);
        buttonsPanel.Controls.Add(consoleButton);
        buttonsPanel.Controls.Add(exitButton);

        Controls.Add(buttonsPanel);
        Controls.Add(label);

        AcceptButton = formButton;
        CancelButton = exitButton;
    }
}
