using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Lab8;

public partial class FormMain : Form, IHotelView
{
    private readonly HotelPresenter _presenter;

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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string HotelNameText { get => textBoxName.Text; set => textBoxName.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string OccupiedRoomsText { get => textBoxOccupiedRooms.Text; set => textBoxOccupiedRooms.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string TotalRoomsText { get => textBoxTotalRooms.Text; set => textBoxTotalRooms.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string PricePerDayText { get => textBoxPricePerDay.Text; set => textBoxPricePerDay.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string AddressText { get => textBoxAddress.Text; set => textBoxAddress.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string RatingText { get => textBoxRating.Text; set => textBoxRating.Text = value; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool HasFreeWiFiChecked { get => checkBoxHasFreeWiFi.Checked; set => checkBoxHasFreeWiFi.Checked = value; }

    public FormMain()
    {
        InitializeComponent();
        MessageBox.Show(this,
            "Лабораторная №8 - Вариант 9 (Гостиница, MVP)\n\nГруппа 24ВП1 - Студенты: Бояркин Максим и Мишин Артём",
            "Привет!!");

        MinimumSize = Size;
        MaximumSize = Size;

        _presenter = new HotelPresenter(this);
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
}

