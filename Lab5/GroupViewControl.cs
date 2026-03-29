using System.Drawing;
using System.Windows.Forms;
using Lab5.Operations;

namespace Lab5;

/// <summary>
/// Контрол для создания группы гостиниц.
/// </summary>
public class GroupViewControl : UserControl
{
    /// <summary>
    /// Текстовое поле для ввода названия группы.
    /// </summary>
    private readonly TextBox _tbName;

    /// <summary>
    /// Текстовое поле для ввода родительской группы.
    /// </summary>
    private readonly TextBox _tbParent;

    /// <summary>
    /// Инициализирует элементы управления.
    /// </summary>
    public GroupViewControl()
    {
        var lblName = new Label { Text = "Название группы:", AutoSize = true };
        _tbName = new TextBox { Width = 200 };

        var lblParent = new Label { Text = "Родительская группа:", AutoSize = true };
        _tbParent = new TextBox { Width = 200 };

        var btnCreate = new Button { Text = "Создать группу", AutoSize = true };
        btnCreate.Click += (_, _) => CreateGroup();

        var btnClear = new Button { Text = "Очистить", AutoSize = true };
        btnClear.Click += (_, _) => {
            _tbName.Clear();
            _tbParent.Clear();
        };

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(16),
            ColumnCount = 2,
            RowCount = 6,
            AutoSize = true
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        int row = 0;
        layout.Controls.Add(lblName, 0, row);
        layout.Controls.Add(_tbName, 1, row++);
        layout.Controls.Add(lblParent, 0, row);
        layout.Controls.Add(_tbParent, 1, row++);
        layout.Controls.Add(btnCreate, 0, row);
        layout.Controls.Add(btnClear, 1, row);

        Controls.Add(layout);
    }

    /// <summary>
    /// Создает группу гостиниц.
    /// </summary>
    private void CreateGroup()
    {
        new CreateGroupOperation(_tbName.Text, _tbParent.Text, () => {
            _tbName.Clear();
            _tbParent.Clear();
        }).Execute();
    }
}
