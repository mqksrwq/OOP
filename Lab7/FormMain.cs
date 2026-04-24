namespace Lab7;

/// <summary>
/// Главное окно приложения (View).
/// </summary>
public partial class FormMain : Form, IMainView
{
    /// <summary>
    /// Экран для отображения дерева групп и гостиниц.
    /// </summary>
    private readonly HotelsTreeViewControl _mainView;

    /// <summary>
    /// Экран для работы с гостиницами.
    /// </summary>
    private readonly HotelViewControl _hotelView;

    /// <summary>
    /// Экран для работы с группами.
    /// </summary>
    private readonly GroupViewControl _groupView;

    /// <summary>
    /// Контроллер главного окна (навигация между экранами).
    /// </summary>
    private readonly MainController _mainController;

    /// <summary>
    /// Контроллер формы гостиниц.
    /// </summary>
    private readonly HotelController _hotelController;

    /// <summary>
    /// Контроллер формы групп.
    /// </summary>
    private readonly GroupController _groupController;

    /// <summary>
    /// Событие запроса показа главного экрана.
    /// </summary>
    public event EventHandler? MainViewRequested;

    /// <summary>
    /// Событие запроса показа экрана гостиниц.
    /// </summary>
    public event EventHandler? HotelViewRequested;

    /// <summary>
    /// Событие запроса показа экрана групп.
    /// </summary>
    public event EventHandler? GroupViewRequested;

    /// <summary>
    /// Инициализирует главное окно приложения.
    /// </summary>
    public FormMain()
    {
        InitializeComponent();

        MessageBox.Show(this,
            "\u041b\u0430\u0431\u043e\u0440\u0430\u0442\u043e\u0440\u043d\u0430\u044f \u21167 - \u0412\u0430\u0440\u0438\u0430\u043d\u0442 9 (\u0413\u043e\u0441\u0442\u0438\u043d\u0438\u0446\u0430, MVC)\n\n\u0413\u0440\u0443\u043f\u043f\u0430 24\u0412\u041f1 - \u0421\u0442\u0443\u0434\u0435\u043d\u0442\u044b: \u0411\u043e\u044f\u0440\u043a\u0438\u043d \u041c\u0430\u043a\u0441\u0438\u043c \u0438 \u041c\u0438\u0448\u0438\u043d \u0410\u0440\u0442\u0451\u043c",
            "\u041f\u0440\u0438\u0432\u0435\u0442!!");

        _mainView = new HotelsTreeViewControl();
        _hotelView = new HotelViewControl();
        _groupView = new GroupViewControl();

        _hotelController = new HotelController(_hotelView);
        _groupController = new GroupController(_groupView);
        _mainController = new MainController(this, _mainView, _hotelView, _groupView);

        menuItemMain.Click += (_, _) => MainViewRequested?.Invoke(this, EventArgs.Empty);
        menuItemHotel.Click += (_, _) => HotelViewRequested?.Invoke(this, EventArgs.Empty);
        menuItemGroup.Click += (_, _) => GroupViewRequested?.Invoke(this, EventArgs.Empty);

        MainViewRequested?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Обновляет состояние пунктов меню.
    /// </summary>
    /// <param name="isMainActive">Активен ли пункт «Главная».</param>
    /// <param name="isHotelActive">Активен ли пункт «Гостиница».</param>
    /// <param name="isGroupActive">Активен ли пункт «Группа».</param>
    public void SetMenuState(bool isMainActive, bool isHotelActive, bool isGroupActive)
    {
        menuItemMain.Checked = isMainActive;
        menuItemHotel.Checked = isHotelActive;
        menuItemGroup.Checked = isGroupActive;
    }

    /// <summary>
    /// Показывает указанный экран в центральной области формы.
    /// </summary>
    /// <param name="view">Отображаемый пользовательский контрол.</param>
    public void ShowView(UserControl view)
    {
        panelContent.Controls.Clear();
        view.Dock = DockStyle.Fill;
        panelContent.Controls.Add(view);
    }
}
