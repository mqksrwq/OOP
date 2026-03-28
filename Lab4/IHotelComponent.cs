using System.Collections.Generic;

namespace Lab4;

/// <summary>
/// Общий интерфейс для листьев и композитов «Гостиница/Коллекция».
/// Позволяет клиенту работать с одиночными отелями и их группами одинаково.
/// </summary>
public interface IHotelComponent
{
    /// <summary>
    /// Имя компонента
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Текстовое представление компонента c отступом
    /// </summary>
    /// <param name="indent">Количество пробелов слева.</param>
    string Describe(int indent = 0);

    /// <summary>
    /// Добавление дочернего компонента (поддерживается только композитом).
    /// </summary>
    void Add(IHotelComponent component);

    /// <summary>
    /// Удаление дочернего компонента по имени (поддерживается только композитом).
    /// </summary>
    bool Remove(string name);

    /// <summary>
    /// Поиск компонента по имени во всём поддереве.
    /// </summary>
    IHotelComponent? Find(string name);

    /// <summary>
    /// Дочерние элементы (для листа — пустая коллекция).
    /// </summary>
    IEnumerable<IHotelComponent> Children { get; }
}
