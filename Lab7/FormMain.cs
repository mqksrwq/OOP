using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Lab7;

public partial class FormMain : Form, IHotelView
{
    private readonly HotelController _controller;

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);

    private const uint MB_OK = 0x00000000;
    private const uint MB_ICONERROR = 0x00000010;
    private const uint MB_ICONWARNING = 0x00000030;

    public event EventHandler? CreateRequested;
    public event EventHandler? ApplyRequested;
    public event EventHandler? CancelRequested;
    public event EventHandler? ExitRequested;
    public event EventHandler<HotelSelectedEventArgs>? EditRequested;

    public FormMain()
    {
        InitializeComponent();
        MessageBox.Show(this,
            "Лабораторная №7 - Вариант 9 (Гостиница, MVC)\n\nГруппа 24ВП1 - Студенты: Бояркин Максим и Мишин Артём",
            "Привет!!");

        MinimumSize = Size;
        MaximumSize = Size;

        _controller = new HotelController(this);
    }

    private void buttonCreate_Click(object sender, EventArgs e)
    {
        CreateRequested?.Invoke(this, EventArgs.Empty);
    }

    private void buttonApply_Click(object sender, EventArgs e)
    {
        ApplyRequested?.Invoke(this, EventArgs.Empty);
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        CancelRequested?.Invoke(this, EventArgs.Empty);
    }

    private void buttonExit_Click(object sender, EventArgs e)
    {
        ExitRequested?.Invoke(this, EventArgs.Empty);
    }

    public void ShowError(string message, string title = "Ошибка")
    {
        MessageBoxW(Handle, message, title, MB_OK | MB_ICONERROR);
    }

    public void ShowWarning(string message, string title = "Предупреждение")
    {
        MessageBoxW(Handle, message, title, MB_OK | MB_ICONWARNING);
    }

    public bool TryGetHotelFromForm(out HotelFormInput input)
    {
        input = default;
        try
        {
            string name = textBoxName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                ShowError("Введите название гостиницы.");
                textBoxName.Select();
                return false;
            }

            if (!SafeParseInt(textBoxOccupiedRooms.Text.Trim(), "Заселено мест", out int occupiedRooms))
            {
                textBoxOccupiedRooms.Select();
                return false;
            }

            if (!SafeParseInt(textBoxTotalRooms.Text.Trim(), "Общее число мест", out int totalRooms))
            {
                textBoxTotalRooms.Select();
                return false;
            }

            if (occupiedRooms > totalRooms)
            {
                ShowError("'Заселено мест' не может быть больше, чем 'Общее число мест'.");
                textBoxOccupiedRooms.Select();
                return false;
            }

            if (!SafeParseDecimal(textBoxPricePerDay.Text.Trim(), "Оплата за день", out decimal pricePerDay))
            {
                textBoxPricePerDay.Select();
                return false;
            }

            string address = textBoxAddress.Text.Trim();
            if (string.IsNullOrWhiteSpace(address))
            {
                ShowError("Введите адрес гостиницы.");
                textBoxAddress.Select();
                return false;
            }

            if (!SafeParseDouble(textBoxRating.Text.Trim(), "Рейтинг", out double rating))
            {
                textBoxRating.Select();
                return false;
            }

            if (rating < 0 || rating > 10)
            {
                ShowError("Неверное значение для 'Рейтинг'. Введите число от 0 до 10.");
                textBoxRating.Select();
                return false;
            }

            input = new HotelFormInput(name, occupiedRooms, totalRooms, pricePerDay, address, rating,
                checkBoxHasFreeWiFi.Checked);
            return true;
        }
        catch (Exception ex)
        {
            ShowError($"Ошибка ввода: {ex.Message}");
            return false;
        }
    }

    public void FillForm(Hotel hotel)
    {
        textBoxName.Text = hotel.Name;
        textBoxOccupiedRooms.Text = hotel.OccupiedRooms.ToString();
        textBoxTotalRooms.Text = hotel.TotalRooms.ToString();
        textBoxPricePerDay.Text = hotel.PricePerDay.ToString();
        textBoxAddress.Text = hotel.Address;
        textBoxRating.Text = hotel.Rating.ToString();
        checkBoxHasFreeWiFi.Checked = hotel.HasFreeWiFi;
    }

    public void RenderHotels()
    {
        flowHotels.Controls.Clear();

        foreach (Hotel hotel in Hotel.Hotels)
        {
            Panel card = new();
            card.Width = 230;
            card.Height = 150;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(0, 0, 0, 10);

            Label info = new();
            info.Text = hotel.ToString();
            info.AutoSize = false;
            info.MaximumSize = new Size(card.ClientSize.Width - 16, 0);
            info.Location = new Point(8, 8);
            info.AutoSize = true;

            Button btnEdit = new();
            btnEdit.Text = "Изменить";
            btnEdit.Width = 100;

            int padding = 8;
            btnEdit.Location = new Point(
                card.ClientSize.Width - btnEdit.Width - padding,
                card.ClientSize.Height - btnEdit.Height - padding
            );

            btnEdit.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            btnEdit.Click += (_, _) => EditRequested?.Invoke(this, new HotelSelectedEventArgs(hotel));

            card.Controls.Add(info);
            card.Controls.Add(btnEdit);
            flowHotels.Controls.Add(card);
        }
    }

    public void SetEditMode(bool enabled)
    {
        buttonCreate.Visible = !enabled;
        buttonApply.Visible = enabled;
        buttonCancel.Visible = enabled;
    }

    public void ClearFormFields()
    {
        textBoxName.Clear();
        textBoxOccupiedRooms.Clear();
        textBoxTotalRooms.Clear();
        textBoxPricePerDay.Clear();
        textBoxAddress.Clear();
        textBoxRating.Clear();
        checkBoxHasFreeWiFi.Checked = false;
    }

    public void CloseView()
    {
        Close();
    }

    private bool SafeParseInt(string text, string fieldName, out int result)
    {
        text = text.Trim();
        try
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                ShowError($"Введите значение для '{fieldName}'.");
                result = 0;
                return false;
            }

            result = checked(int.Parse(text));
            if (result < 0)
            {
                ShowWarning($"Для '{fieldName}' введите число ≥ 0.");
                result = 0;
                return false;
            }

            return true;
        }
        catch (OverflowException)
        {
            throw new HotelOverflowException(fieldName, text);
        }
        catch (FormatException)
        {
            ShowError($"Неверный формат числа в '{fieldName}'.");
            result = 0;
            return false;
        }
    }

    private bool SafeParseDecimal(string text, string fieldName, out decimal result)
    {
        text = text.Trim();
        try
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                ShowError($"Введите значение для '{fieldName}'.");
                result = 0;
                return false;
            }

            result = checked(decimal.Parse(text));
            if (result < 0)
            {
                ShowWarning($"Для '{fieldName}' введите число ≥ 0.");
                result = 0;
                return false;
            }

            return true;
        }
        catch (OverflowException)
        {
            throw new HotelOverflowException(fieldName, text);
        }
        catch (FormatException)
        {
            ShowError($"Неверный формат числа в '{fieldName}'.");
            result = 0;
            return false;
        }
    }

    private bool SafeParseDouble(string text, string fieldName, out double result)
    {
        text = text.Trim();
        try
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                ShowError($"Введите значение для '{fieldName}'.");
                result = 0;
                return false;
            }

            result = double.Parse(text);
            return true;
        }
        catch (OverflowException)
        {
            throw new HotelOverflowException(fieldName, text);
        }
        catch (FormatException)
        {
            ShowError($"Неверный формат числа в '{fieldName}'.");
            result = 0;
            return false;
        }
    }
}
