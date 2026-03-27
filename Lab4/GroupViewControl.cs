using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab4;

/// <summary>
/// Контрол для создания группы гостиниц.
/// </summary>
public class GroupViewControl : UserControl
{
    private readonly TextBox _tbName;

    public GroupViewControl()
    {
        var lblName = new Label { Text = "Название группы:", AutoSize = true };
        _tbName = new TextBox { Width = 200 };

        var btnCreate = new Button { Text = "Создать группу", AutoSize = true };
        btnCreate.Click += (_, _) => CreateGroup();

        var btnClear = new Button { Text = "Очистить", AutoSize = true };
        btnClear.Click += (_, _) => _tbName.Clear();

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(16),
            ColumnCount = 2,
            RowCount = 4,
            AutoSize = true
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        int row = 0;
        layout.Controls.Add(lblName, 0, row);
        layout.Controls.Add(_tbName, 1, row++);
        layout.Controls.Add(btnCreate, 0, row);
        layout.Controls.Add(btnClear, 1, row);

        Controls.Add(layout);
    }

    private void CreateGroup()
    {
        try
        {
            var name = _tbName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите название группы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var root = HotelAppState.Instance.Hotels;
            if (root.Find(name) != null)
            {
                MessageBox.Show("Компонент с таким именем уже существует.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            root.Add(new HotelsHashtableCollection(name));
            MessageBox.Show("Группа создана.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _tbName.Clear();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
