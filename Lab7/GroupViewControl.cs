using System.Drawing;
using System.Windows.Forms;

namespace Lab5;

/// <summary>
/// Представление (View) для работы с группами гостиниц в паттерне MVP.
/// </summary>
public class GroupViewControl : UserControl
    , IGroupView
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
    /// Событие запроса создания группы.
    /// </summary>
    public event EventHandler? CreateGroupRequested;

    /// <summary>
    /// Событие запроса очистки полей формы.
    /// </summary>
    public event EventHandler? ClearRequested;

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
        btnCreate.Click += (_, _) => CreateGroupRequested?.Invoke(this, EventArgs.Empty);

        var btnClear = new Button { Text = "Очистить", AutoSize = true };
        btnClear.Click += (_, _) => ClearRequested?.Invoke(this, EventArgs.Empty);

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
    /// Введённое пользователем название новой группы.
    /// </summary>
    public string GroupName => _tbName.Text;

    /// <summary>
    /// Введённое пользователем название родительской группы.
    /// </summary>
    public string ParentGroupName => _tbParent.Text;

    /// <summary>
    /// Очищает поля ввода формы.
    /// </summary>
    public void ClearForm()
    {
        _tbName.Clear();
        _tbParent.Clear();
    }
}
