using System;
using Lab7.Operations;

namespace Lab7;

/// <summary>
/// Контракт представления формы гостиницы в MVC.
/// </summary>
public interface IHotelView
{
    /// <summary>
    /// Событие запроса создания гостиницы.
    /// </summary>
    event EventHandler? CreateHotelRequested;

    /// <summary>
    /// Событие запроса сохранения изменений гостиницы.
    /// </summary>
    event EventHandler? SaveHotelRequested;

    /// <summary>
    /// Событие запроса поиска гостиницы.
    /// </summary>
    event EventHandler? FindHotelRequested;

    /// <summary>
    /// Событие запроса очистки формы.
    /// </summary>
    event EventHandler? ClearRequested;

    /// <summary>
    /// Текст поиска гостиницы.
    /// </summary>
    string SearchText { get; }

    /// <summary>
    /// Считывает данные формы в DTO.
    /// </summary>
    /// <returns>Данные формы гостиницы.</returns>
    HotelFormData ReadFormData();

    /// <summary>
    /// Заполняет поля формы данными найденной гостиницы.
    /// </summary>
    /// <param name="group">Группа гостиницы.</param>
    /// <param name="hotel">Гостиница.</param>
    void FillForm(HotelsHashtableCollection group, Hotel hotel);

    /// <summary>
    /// Очищает поля формы.
    /// </summary>
    void ClearForm();

    /// <summary>
    /// Включает или отключает кнопку сохранения.
    /// </summary>
    /// <param name="enabled">Доступность кнопки.</param>
    void SetSaveEnabled(bool enabled);

    /// <summary>
    /// Показывает информационное сообщение пользователю.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    void ShowInfo(string message);
}


