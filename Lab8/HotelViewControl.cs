using System;
using System.Drawing;
using System.Windows.Forms;
using Lab8.Operations;

namespace Lab8;

/// <summary>
/// Представление (View) для работы с одной гостиницей в паттерне MVP.
/// </summary>
public class HotelViewControl : UserControl
    , IHotelView
{
    /// <summary>
    /// Поле ввода для поиска гостиницы по имени.
    /// </summary>
    private readonly TextBox _tbSearch;

    /// <summary>
    /// Поле ввода названия группы.
    /// </summary>
    private readonly TextBox _tbGroup;

    /// <summary>
    /// Поле ввода названия гостиницы.
    /// </summary>
    private readonly TextBox _tbName;

    /// <summary>
    /// Поле ввода адреса.
    /// </summary>
    private readonly TextBox _tbAddress;

    /// <summary>
    /// Поле ввода общего количества мест.
    /// </summary>
    private readonly TextBox _tbTotal;

    /// <summary>
    /// Поле ввода количества занятых мест.
    /// </summary>
    private readonly TextBox _tbOccupied;

    /// <summary>
    /// Поле ввода цены за сутки.
    /// </summary>
    private readonly TextBox _tbPrice;

    /// <summary>
    /// Поле ввода рейтинга.
    /// </summary>
    private readonly TextBox _tbRating;

    /// <summary>
    /// Флаг наличия бесплатного Wi‑Fi.
    /// </summary>
    private readonly CheckBox _cbWifi;

    /// <summary>
    /// Кнопка создания гостиницы.
    /// </summary>
    private readonly Button _btnCreate;

    /// <summary>
    /// Кнопка сохранения изменений гостиницы.
    /// </summary>
    private readonly Button _btnSave;

    /// <summary>
    /// Кнопка очистки формы.
    /// </summary>
    private readonly Button _btnClear;

    /// <summary>
    /// Событие запроса создания гостиницы.
    /// </summary>
    public event EventHandler? CreateHotelRequested;

    /// <summary>
    /// Событие запроса сохранения гостиницы.
    /// </summary>
    public event EventHandler? SaveHotelRequested;

    /// <summary>
    /// Событие запроса поиска гостиницы.
    /// </summary>
    public event EventHandler? FindHotelRequested;

    /// <summary>
    /// Событие запроса очистки формы.
    /// </summary>
    public event EventHandler? ClearRequested;

    /// <summary>
    /// Инициализирует визуальные элементы и связывает UI-события с обработчиками контроллера.
    /// </summary>
    public HotelViewControl()
    {
        var lblSearch = new Label { Text = "Поиск (имя):", AutoSize = true };
        _tbSearch = new TextBox { Width = 220 };
        var btnFind = new Button { Text = "Найти", AutoSize = true };
        btnFind.Click += (_, _) => FindHotelRequested?.Invoke(this, EventArgs.Empty);

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
        _btnCreate.Click += (_, _) => CreateHotelRequested?.Invoke(this, EventArgs.Empty);

        _btnSave = new Button { Text = "Сохранить изменения", AutoSize = true, Enabled = false };
        _btnSave.Click += (_, _) => SaveHotelRequested?.Invoke(this, EventArgs.Empty);

        _btnClear = new Button { Text = "Очистить", AutoSize = true };
        _btnClear.Click += (_, _) => ClearRequested?.Invoke(this, EventArgs.Empty);

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

    /// <summary>
    /// Текст, введённый в поле поиска гостиницы.
    /// </summary>
    public string SearchText => _tbSearch.Text;

    /// <summary>
    /// Очищает все поля формы и сбрасывает состояние кнопки сохранения.
    /// </summary>
    public void ClearForm()
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
        _btnSave.Enabled = false;
    }

    /// <summary>
    /// Считывает текущие значения полей формы в объект данных.
    /// </summary>
    /// <returns>Снимок данных формы гостиницы.</returns>
    public HotelFormData ReadFormData()
    {
        return new HotelFormData(
            _tbGroup.Text,
            _tbName.Text,
            _tbAddress.Text,
            _tbTotal.Text,
            _tbOccupied.Text,
            _tbPrice.Text,
            _tbRating.Text,
            _cbWifi.Checked);
    }

    /// <summary>
    /// Показывает информационное сообщение пользователю.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    public void ShowInfo(string message)
    {
        MessageBox.Show(message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// Заполняет поля формы данными указанной гостиницы.
    /// </summary>
    /// <param name="group">Группа гостиницы.</param>
    /// <param name="hotel">Гостиница для отображения.</param>
    public void FillForm(HotelsHashtableCollection group, Hotel hotel)
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

    /// <summary>
    /// Управляет доступностью кнопки сохранения.
    /// </summary>
    /// <param name="enabled">Нужно ли сделать кнопку активной.</param>
    public void SetSaveEnabled(bool enabled)
    {
        _btnSave.Enabled = enabled;
    }
}



