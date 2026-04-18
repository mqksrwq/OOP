using System;
using System.Windows.Forms;

namespace Lab7;

/// <summary>
/// Контракт главного представления в MVP.
/// Определяет события навигации и операции отображения экранов.
/// </summary>
public interface IMainView
{
    /// <summary>
    /// Событие запроса отображения главного экрана.
    /// </summary>
    event EventHandler? MainViewRequested;

    /// <summary>
    /// Событие запроса отображения экрана гостиниц.
    /// </summary>
    event EventHandler? HotelViewRequested;

    /// <summary>
    /// Событие запроса отображения экрана групп.
    /// </summary>
    event EventHandler? GroupViewRequested;

    /// <summary>
    /// Показывает указанный экран в контейнере главной формы.
    /// </summary>
    /// <param name="view">Экран для отображения.</param>
    void ShowView(UserControl view);

    /// <summary>
    /// Обновляет состояние отмеченных пунктов меню.
    /// </summary>
    /// <param name="isMainActive">Активность главного пункта меню.</param>
    /// <param name="isHotelActive">Активность пункта гостиницы.</param>
    /// <param name="isGroupActive">Активность пункта групп.</param>
    void SetMenuState(bool isMainActive, bool isHotelActive, bool isGroupActive);
}


