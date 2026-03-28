using System;
using System.Collections.Generic;

namespace Lab4;

/// <summary>
/// Класс гостиницы (лист в паттерне Composite)
/// </summary>
public class Hotel : IHotelComponent
{
    // Свойства класса
    public string Name { get; set; }
    public int OccupiedRooms { get; set; }
    public int TotalRooms { get; set; }
    public decimal PricePerDay { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }
    public bool HasFreeWiFi { get; set; }
    
    /// <summary>
    /// Конструктор без параметров
    /// </summary>
    public Hotel()
    {
        Name = "Неизвестно";
        OccupiedRooms = 0;
        TotalRooms = 10;
        PricePerDay = 1000m;
        Address = "Адрес не задан";
        Rating = 3.5;
        HasFreeWiFi = true;
    }

    /// <summary>
    /// Конструктор со всеми параметрами
    /// </summary>
    /// <param name="name"> Название гостиницы </param>
    /// <param name="occupiedRooms"> Количество заселенных мест </param>
    /// <param name="totalRooms"> Общее количество мест </param>
    /// <param name="pricePerDay"> Стоимость за день </param>
    /// <param name="address"> Адрес </param>
    /// <param name="rating"> Рейтинг </param>
    /// <param name="hasFreeWiFi"> Наличие бесплатного WiFi </param>
    public Hotel(string name, int occupiedRooms, int totalRooms, decimal pricePerDay, string address, double rating,
        bool hasFreeWiFi)
    {
        Name = name;
        OccupiedRooms = occupiedRooms;
        TotalRooms = totalRooms;
        PricePerDay = pricePerDay;
        Address = address;
        Rating = rating;
        HasFreeWiFi = hasFreeWiFi;
    }

    /// <summary>
    /// Метод, переопределяющий toString() для всего объекта
    /// </summary>
    /// <returns> Объект в виде строки </returns>
    public override string ToString()
    {
        return $"Название: {Name}\n" +
               $"Заселено: {OccupiedRooms} из {TotalRooms}\n" +
               $"Цена за день: {PricePerDay} руб\n" +
               $"Адрес: {Address}, Рейтинг: {Rating}\n" +
               $"Wi-Fi: {(HasFreeWiFi ? "Да" : "Нет")}\n";
    }

    /// <summary>
    /// Формирует строковое представление объекта с заданным отступом.
    /// Используется в паттерне "Компоновщик" для визуализации иерархии дерева.
    /// </summary>
    /// <param name="indent">Количество пробелов для отступа слева (уровень вложенности).</param>
    /// <returns>Строка, содержащая данные о гостинице, сдвинутая вправо на значение indent.</returns>
    public string Describe(int indent = 0)
    {
        var pad = new string(' ', Math.Max(0, indent));
        return pad + ToString();
    }

    /// <summary>
    /// Добавление компонента. 
    /// Для класса Hotel (листа) операция не поддерживается, так как отель не может содержать другие компоненты.
    /// </summary>
    /// <param name="component">Компонент для добавления.</param>
    /// <exception cref="NotSupportedException">Всегда выбрасывается, так как Hotel — конечный узел.</exception>
    public void Add(IHotelComponent component) =>
        throw new NotSupportedException("Нельзя добавить компонент к листу Hotel.");

    /// <summary>
    /// Удаление компонента по имени.
    /// Для класса Hotel (листа) операция не поддерживается.
    /// </summary>
    /// <param name="name">Название компонента для удаления.</param>
    /// <returns>Метод не возвращает значение, так как выбрасывает исключение.</returns>
    /// <exception cref="NotSupportedException">Всегда выбрасывается, так как у отеля нет дочерних элементов.</exception>
    public bool Remove(string name) =>
        throw new NotSupportedException("Нельзя удалять дочерние элементы у листа Hotel.");

    /// <summary>
    /// Поиск компонента в текущем узле.
    /// Является "базовым случаем" рекурсии: если имя совпадает, возвращает текущий объект.
    /// </summary>
    /// <param name="name">Искомое название отеля.</param>
    /// <returns>
    /// Ссылка на текущий объект (IHotelComponent), если имя совпало; иначе — null.
    /// </returns>
    public IHotelComponent? Find(string name) => Name == name ? this : null;

    /// <summary>
    /// Возвращает коллекцию дочерних элементов.
    /// Для узла-листа (Hotel) всегда возвращает пустую последовательность.
    /// </summary>
    /// <remarks>
    /// Использование 'yield break' позволяет избежать создания пустого списка в памяти
    /// и предотвращает ошибки NullReferenceException у клиента, предоставляя пустой итератор вместо null.
    /// </remarks>
    public IEnumerable<IHotelComponent> Children
    {
        get { yield break; }
    }
}
