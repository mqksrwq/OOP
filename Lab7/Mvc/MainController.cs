using System;
using System.Windows.Forms;

namespace Lab7;

/// <summary>
/// Контроллер главного экрана.
/// Отвечает за навигацию между представлениями в стиле MVC.
/// </summary>
public sealed class MainController
{
    /// <summary>
    /// Представление главной формы.
    /// </summary>
    private readonly IMainView _view;

    /// <summary>
    /// Представление дерева всех групп и гостиниц.
    /// </summary>
    private readonly HotelsTreeViewControl _mainView;

    /// <summary>
    /// Представление формы работы с гостиницей.
    /// </summary>
    private readonly HotelViewControl _hotelView;

    /// <summary>
    /// Представление формы работы с группами.
    /// </summary>
    private readonly GroupViewControl _groupView;

    /// <summary>
    /// Инициализирует контроллер и подписывает обработчики событий навигации.
    /// </summary>
    /// <param name="view">Главное представление.</param>
    /// <param name="mainView">Экран дерева гостиниц.</param>
    /// <param name="hotelView">Экран редактирования гостиниц.</param>
    /// <param name="groupView">Экран работы с группами.</param>
    public MainController(
        IMainView view,
        HotelsTreeViewControl mainView,
        HotelViewControl hotelView,
        GroupViewControl groupView)
    {
        _view = view;
        _mainView = mainView;
        _hotelView = hotelView;
        _groupView = groupView;

        _view.MainViewRequested += (_, _) => Show(_mainView, true, false, false);
        _view.HotelViewRequested += (_, _) => Show(_hotelView, false, true, false);
        _view.GroupViewRequested += (_, _) => Show(_groupView, false, false, true);
    }

    /// <summary>
    /// Показывает выбранное представление и обновляет состояние пунктов меню.
    /// </summary>
    /// <param name="userControl">Отображаемый экран.</param>
    /// <param name="main">Признак активной вкладки главного экрана.</param>
    /// <param name="hotel">Признак активной вкладки гостиниц.</param>
    /// <param name="group">Признак активной вкладки групп.</param>
    private void Show(UserControl userControl, bool main, bool hotel, bool group)
    {
        _view.ShowView(userControl);
        _view.SetMenuState(main, hotel, group);
    }
}


