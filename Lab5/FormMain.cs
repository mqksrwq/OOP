namespace Lab5;

/// <summary>
/// Главная форма приложения
/// </summary>
public partial class FormMain : Form
{
    /// <summary>
    /// Контрол для отображения дерева гостиниц
    /// </summary>
    private readonly HotelsTreeViewControl _mainView;

    /// <summary>
    /// Контрол для работы с одиночной гостиницей
    /// </summary>
    private readonly HotelViewControl _hotelView;

    /// <summary>
    /// Контрол для работы с группой гостиниц
    /// </summary>
    private readonly GroupViewControl _groupView;

    /// <summary>
    /// Конструктор главной формы
    /// </summary>
    public FormMain()
    {
        InitializeComponent();

        MessageBox.Show(this,
            "Лабораторная №5 - Вариант 9 (Гостиница)\n\nГруппа 24ВП1 - Студенты: Бояркин Максим и Мишин Артём",
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
    /// Обновляет состояние пунктов меню
    /// </summary>
    /// <param name="view">Активный контрол</param>
    private void UpdateMenuState(UserControl view)
    {
        menuItemMain.Checked = ReferenceEquals(view, _mainView);
        menuItemHotel.Checked = ReferenceEquals(view, _hotelView);
        menuItemGroup.Checked = ReferenceEquals(view, _groupView);
    }

    /// <summary>
    /// Показывает выбранный контрол в главном экране
    /// </summary>
    /// <param name="view">Контрол для отображения</param>
    private void ShowView(UserControl view)
    {
        panelContent.Controls.Clear();
        view.Dock = DockStyle.Fill;
        panelContent.Controls.Add(view);
        UpdateMenuState(view);
    }
}