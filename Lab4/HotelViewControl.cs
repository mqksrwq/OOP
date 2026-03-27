using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab4;

/// <summary>
/// Контрол для создания одной гостиницы.
/// </summary>
public class HotelViewControl : UserControl
{
    private readonly TextBox _tbSearch;
    private readonly TextBox _tbGroup;
    private readonly TextBox _tbName;
    private readonly TextBox _tbAddress;
    private readonly TextBox _tbTotal;
    private readonly TextBox _tbOccupied;
    private readonly TextBox _tbPrice;
    private readonly TextBox _tbRating;
    private readonly CheckBox _cbWifi;
    private readonly Button _btnCreate;
    private readonly Button _btnSave;
    private readonly Button _btnClear;

    private HotelsHashtableCollection? _currentGroup;
    private Hotel? _currentHotel;

    public HotelViewControl()
    {
        var lblSearch = new Label { Text = "Поиск (имя):", AutoSize = true };
        _tbSearch = new TextBox { Width = 220 };
        var btnFind = new Button { Text = "Найти", AutoSize = true };
        btnFind.Click += (_, _) => FindHotel();

        var lblGroup = new Label { Text = "Группа:", AutoSize = true };
        _tbGroup = new TextBox { Width = 220 };

        var lblName = new Label { Text = "Название:", AutoSize = true };
        _tbName = new TextBox { Width = 220 };

        var lblAddress = new Label { Text = "Адрес:", AutoSize = true };
        _tbAddress = new TextBox { Width = 220 };

        var lblTotal = new Label { Text = "Всего мест:", AutoSize = true };
        _tbTotal = new TextBox { Width = 120 };

        var lblOccupied = new Label { Text = "Заселено:", AutoSize = true };
        _tbOccupied = new TextBox { Width = 120 };

        var lblPrice = new Label { Text = "Цена/день:", AutoSize = true };
        _tbPrice = new TextBox { Width = 120 };

        var lblRating = new Label { Text = "Рейтинг:", AutoSize = true };
        _tbRating = new TextBox { Width = 120 };

        var lblWifi = new Label { Text = "Wi‑Fi:", AutoSize = true };
        _cbWifi = new CheckBox();

        _btnCreate = new Button { Text = "Создать гостиницу", AutoSize = true };
        _btnCreate.Click += (_, _) => CreateHotel();

        _btnSave = new Button { Text = "Сохранить изменения", AutoSize = true, Enabled = false };
        _btnSave.Click += (_, _) => SaveHotel();

        _btnClear = new Button { Text = "Очистить", AutoSize = true };
        _btnClear.Click += (_, _) => ClearForm();

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(16),
            ColumnCount = 2,
            RowCount = 12,
            AutoSize = true
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        int row = 0;
        layout.Controls.Add(lblSearch, 0, row);
        layout.Controls.Add(_tbSearch, 1, row++);
        layout.Controls.Add(btnFind, 1, row++);

        layout.Controls.Add(lblGroup, 0, row);
        layout.Controls.Add(_tbGroup, 1, row++);
        layout.Controls.Add(lblName, 0, row);
        layout.Controls.Add(_tbName, 1, row++);
        layout.Controls.Add(lblAddress, 0, row);
        layout.Controls.Add(_tbAddress, 1, row++);
        layout.Controls.Add(lblTotal, 0, row);
        layout.Controls.Add(_tbTotal, 1, row++);
        layout.Controls.Add(lblOccupied, 0, row);
        layout.Controls.Add(_tbOccupied, 1, row++);
        layout.Controls.Add(lblPrice, 0, row);
        layout.Controls.Add(_tbPrice, 1, row++);
        layout.Controls.Add(lblRating, 0, row);
        layout.Controls.Add(_tbRating, 1, row++);
        layout.Controls.Add(lblWifi, 0, row);
        layout.Controls.Add(_cbWifi, 1, row++);
        layout.Controls.Add(_btnCreate, 0, row);
        layout.Controls.Add(_btnSave, 1, row++);
        layout.Controls.Add(_btnClear, 1, row);

        Controls.Add(layout);
    }

    private void CreateHotel()
    {
        try
        {
            var groupName = _tbGroup.Text.Trim();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show("Введите название группы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var root = HotelAppState.Instance.Hotels;
            var groupComponent = root.Find(groupName);
            if (groupComponent is not HotelsHashtableCollection group)
            {
                MessageBox.Show("Группа не найдена. Создайте группу на соответствующей форме.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!TryParseInt(_tbTotal.Text, "Всего мест", out int total)) return;
            if (!TryParseInt(_tbOccupied.Text, "Заселено", out int occupied)) return;
            if (!TryParseDecimal(_tbPrice.Text, "Цена/день", out decimal price)) return;
            if (!TryParseDouble(_tbRating.Text, "Рейтинг", out double rating)) return;

            var hotel = new Hotel(_tbName.Text.Trim(), occupied, total, price,
                _tbAddress.Text.Trim(), rating, _cbWifi.Checked);

            group.Add(hotel);
            MessageBox.Show("Гостиница создана и добавлена в группу.", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearForm()
    {
        _tbSearch.Clear();
        _tbGroup.Clear();
        _tbName.Clear();
        _tbAddress.Clear();
        _tbTotal.Clear();
        _tbOccupied.Clear();
        _tbPrice.Clear();
        _tbRating.Clear();
        _cbWifi.Checked = false;
        _currentGroup = null;
        _currentHotel = null;
        _btnSave.Enabled = false;
    }

    private static bool TryParseInt(string text, string field, out int value)
    {
        if (int.TryParse(text.Trim(), out value)) return true;
        MessageBox.Show($"Некорректное число в поле '{field}'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }

    private static bool TryParseDecimal(string text, string field, out decimal value)
    {
        if (decimal.TryParse(text.Trim(), out value)) return true;
        MessageBox.Show($"Некорректное число в поле '{field}'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }

    private static bool TryParseDouble(string text, string field, out double value)
    {
        if (double.TryParse(text.Trim(), out value)) return true;
        MessageBox.Show($"Некорректное число в поле '{field}'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }

    private void FindHotel()
    {
        var search = _tbSearch.Text.Trim();
        if (string.IsNullOrWhiteSpace(search))
        {
            MessageBox.Show("Введите имя гостиницы для поиска.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var root = HotelAppState.Instance.Hotels;
        if (!TryFindHotelWithParent(root, search, out var group, out var hotel))
        {
            MessageBox.Show("Гостиница не найдена.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _currentGroup = group;
        _currentHotel = hotel;
        FillForm(group, hotel);
        _btnSave.Enabled = true;
    }

    private void FillForm(HotelsHashtableCollection group, Hotel hotel)
    {
        _tbGroup.Text = group.Name;
        _tbName.Text = hotel.Name;
        _tbAddress.Text = hotel.Address;
        _tbTotal.Text = hotel.TotalRooms.ToString();
        _tbOccupied.Text = hotel.OccupiedRooms.ToString();
        _tbPrice.Text = hotel.PricePerDay.ToString();
        _tbRating.Text = hotel.Rating.ToString();
        _cbWifi.Checked = hotel.HasFreeWiFi;
    }

    private void SaveHotel()
    {
        if (_currentGroup == null || _currentHotel == null)
        {
            MessageBox.Show("Сначала найдите гостиницу для редактирования.", "Инфо",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!ValidateInputs(out var newHotel, out var targetGroup)) return;

        // удалить из старой группы
        _currentGroup.Remove(_currentHotel.Name);
        // добавить в новую (может быть та же)
        targetGroup.Add(newHotel);

        _currentGroup = targetGroup;
        _currentHotel = newHotel;
        MessageBox.Show("Изменения сохранены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
    }

    private bool ValidateInputs(out Hotel hotel, out HotelsHashtableCollection targetGroup)
    {
        hotel = null!;
        targetGroup = null!;

        var groupName = _tbGroup.Text.Trim();
        if (string.IsNullOrWhiteSpace(groupName))
        {
            MessageBox.Show("Введите название группы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        var root = HotelAppState.Instance.Hotels;
        var groupComponent = root.Find(groupName);
        if (groupComponent is not HotelsHashtableCollection group)
        {
            MessageBox.Show("Группа не найдена. Создайте группу на соответствующей форме.", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (!TryParseInt(_tbTotal.Text, "Всего мест", out int total)) return false;
        if (!TryParseInt(_tbOccupied.Text, "Заселено", out int occupied)) return false;
        if (!TryParseDecimal(_tbPrice.Text, "Цена/день", out decimal price)) return false;
        if (!TryParseDouble(_tbRating.Text, "Рейтинг", out double rating)) return false;

        hotel = new Hotel(_tbName.Text.Trim(), occupied, total, price,
            _tbAddress.Text.Trim(), rating, _cbWifi.Checked);
        targetGroup = group;
        return true;
    }

    private bool TryFindHotelWithParent(HotelsHashtableCollection root, string name,
        out HotelsHashtableCollection group, out Hotel hotel)
    {
        foreach (var child in root.Children)
        {
            if (child is Hotel h && h.Name == name)
            {
                group = root;
                hotel = h;
                return true;
            }

            if (child is HotelsHashtableCollection nested &&
                TryFindHotelWithParent(nested, name, out group, out hotel))
            {
                return true;
            }
        }

        group = null!;
        hotel = null!;
        return false;
    }
}
