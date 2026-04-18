namespace Lab5;

/// <summary>
/// Главная форма приложения
/// </summary>
public partial class FormMain : Form, IMainView
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
    /// Presenter главного окна (навигация между экранами).
    /// </summary>
    private readonly MainPresenter _mainPresenter;

    /// <summary>
    /// Presenter формы работы с гостиницами.
    /// </summary>
    private readonly HotelPresenter _hotelPresenter;

    /// <summary>
    /// Presenter формы работы с группами.
    /// </summary>
    private readonly GroupPresenter _groupPresenter;

    /// <summary>
    /// Событие запроса перехода на главный экран.
    /// </summary>
    public event EventHandler? MainViewRequested;

    /// <summary>
    /// Событие запроса перехода на экран гостиницы.
    /// </summary>
    public event EventHandler? HotelViewRequested;

    /// <summary>
    /// Событие запроса перехода на экран групп.
    /// </summary>
    public event EventHandler? GroupViewRequested;

    /// <summary>
    /// Конструктор главной формы
    /// </summary>
    public FormMain()
    {
        InitializeComponent();

        MessageBox.Show(this,
            "Лабораторная №7 - Вариант 9 (Гостиница, MVP)\n\nГруппа 24ВП1 - Студенты: Бояркин Максим и Мишин Артём",
            "Привет!!");

        _mainView = new HotelsTreeViewControl();
        _hotelView = new HotelViewControl();
        _groupView = new GroupViewControl();

        _hotelPresenter = new HotelPresenter(_hotelView);
        _groupPresenter = new GroupPresenter(_groupView);
        _mainPresenter = new MainPresenter(this, _mainView, _hotelView, _groupView);

        menuItemMain.Click += (_, _) => MainViewRequested?.Invoke(this, EventArgs.Empty);
        menuItemHotel.Click += (_, _) => HotelViewRequested?.Invoke(this, EventArgs.Empty);
        menuItemGroup.Click += (_, _) => GroupViewRequested?.Invoke(this, EventArgs.Empty);

        MainViewRequested?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Обновляет состояние флажков меню навигации.
    /// </summary>
    /// <param name="isMainActive">Активна ли вкладка главного экрана.</param>
    /// <param name="isHotelActive">Активна ли вкладка гостиницы.</param>
    /// <param name="isGroupActive">Активна ли вкладка групп.</param>
    public void SetMenuState(bool isMainActive, bool isHotelActive, bool isGroupActive)
    {
        menuItemMain.Checked = isMainActive;
        menuItemHotel.Checked = isHotelActive;
        menuItemGroup.Checked = isGroupActive;
    }

    /// <summary>
    /// Отображает выбранный экран в рабочей области формы.
    /// </summary>
    /// <param name="view">UserControl, который необходимо показать.</param>
    public void ShowView(UserControl view)
    {
        panelContent.Controls.Clear();
        view.Dock = DockStyle.Fill;
        panelContent.Controls.Add(view);
    }
}