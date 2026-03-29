using System.Windows.Forms;

namespace Lab4;

public partial class FormMain : Form
{
    private readonly HotelsTreeViewControl _mainView;
    private readonly HotelViewControl _hotelView;
    private readonly GroupViewControl _groupView;

    public FormMain()
    {
        InitializeComponent();

        MessageBox.Show(this,
            "Лабораторная №1 - Вариант 9 (Гостиница)\n\nГруппа 24ВП1 - Студенты: Бояркин Максим и Мишин Артём",
            "Привет!!");
        
        _mainView = new HotelsTreeViewControl();
        _hotelView = new HotelViewControl();
        _groupView = new GroupViewControl();

        menuItemMain.Click += (_, _) => ShowView(_mainView);
        menuItemHotel.Click += (_, _) => ShowView(_hotelView);
        menuItemGroup.Click += (_, _) => ShowView(_groupView);

        ShowView(_mainView);
    }

    /// <summary>
    /// Обновляет визуальное состояние главного меню (ставит "галочки").
    /// Сравнивает текущее отображаемое представление с доступными экранами.
    /// </summary>
    /// <param name="view">Ссылка на текущий активный пользовательский контрол.</param>
    private void UpdateMenuState(UserControl view)
    {
        menuItemMain.Checked = ReferenceEquals(view, _mainView);
        menuItemHotel.Checked = ReferenceEquals(view, _hotelView);
        menuItemGroup.Checked = ReferenceEquals(view, _groupView);
    }

    /// <summary>
    /// Переключает содержимое центральной панели на выбранное представление (View).
    /// </summary>
    /// <param name="view">Контрол, который нужно отобразить пользователю.</param>
    private void ShowView(UserControl view)
    {
        panelContent.Controls.Clear();
        view.Dock = DockStyle.Fill;
        panelContent.Controls.Add(view);
        UpdateMenuState(view);
    }
}
