using System;
using System.Collections.Generic;

namespace Lab8;

/// <summary>
/// Класс гостиницы (лист в паттерне Composite)
/// </summary>
public class Hotel : IHotelComponent
{
    /// <summary>
    /// Название гостиницы.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Количество занятых номеров.
    /// </summary>
    public int OccupiedRooms { get; set; }

    /// <summary>
    /// Общее количество номеров.
    /// </summary>
    public int TotalRooms { get; set; }

    /// <summary>
    /// Стоимость проживания за сутки.
    /// </summary>
    public decimal PricePerDay { get; set; }

    /// <summary>
    /// Адрес гостиницы.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Рейтинг гостиницы.
    /// </summary>
    public double Rating { get; set; }

    /// <summary>
    /// Признак наличия бесплатного Wi‑Fi.
    /// </summary>
    public bool HasFreeWiFi { get; set; }

    /// <summary>
    /// Количество созданных экземпляров гостиницы.
    /// </summary>
    public static int InstanceCount { get; private set; }

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
        InstanceCount++;
    }

    /// <summary>
    /// Конструктор с одним параметром
    /// </summary>
    /// <param name="name"> Название гостиницы </param>
    public Hotel(string name) : this()
    {
        Name = name;
    }

    /// <summary>
    /// Конструктор с двумя параметрами
    /// </summary>
    /// <param name="name"> Название гостиницы </param>
    /// <param name="totalRooms"> Общее количество мест </param>
    public Hotel(string name, int totalRooms) : this(name)
    {
        TotalRooms = totalRooms;
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
    /// Возвращает описание гостиницы с отступом
    /// </summary>
    /// <param name="indent">Количество пробелов для отступа</param>
    /// <returns>Строковое описание гостиницы</returns>
    public string Describe(int indent = 0)
    {
        var pad = new string(' ', Math.Max(0, indent));
        return pad + ToString();
    }

    /// <summary>
    /// Не поддерживается для одиночной гостиницы
    /// </summary>
    /// <param name="component">Добавляемый компонент</param>
    public void Add(IHotelComponent component) =>
        throw new NotSupportedException("Нельзя добавить компонент к листу Hotel.");

    /// <summary>
    /// Не поддерживается для одиночной гостиницы
    /// </summary>
    /// <param name="name">Имя удаляемого компонента</param>
    /// <returns>Всегда выбрасывает исключение</returns>
    public bool Remove(string name) =>
        throw new NotSupportedException("Удаление не поддерживается листом.");

    /// <summary>
    /// Возвращает текущую гостиницу, если имена совпадают
    /// </summary>
    /// <param name="name">Имя для поиска</param>
    /// <returns>Найденный компонент или null</returns>
    public IHotelComponent? Find(string name)
    {
        return Name == name ? this : null;
    }

    /// <summary>
    /// Пустая коллекция дочерних элементов для одиночной гостиницы
    /// </summary>
    public IEnumerable<IHotelComponent> Children => Array.Empty<IHotelComponent>();

    /// <summary>
    /// Метод для вывода значения определенного поля
    /// </summary>
    /// <param name="fieldName"> Название поля </param>
    /// <returns> Значение поля в виде строки </returns>
    public string GetFieldValue(string fieldName)
    {
        return fieldName switch
        {
            "Name" => Name,
            "OccupiedRooms" => OccupiedRooms.ToString(),
            "TotalRooms" => TotalRooms.ToString(),
            "PricePerDay" => PricePerDay.ToString(),
            "Address" => Address,
            "Rating" => Rating.ToString(),
            "HasFreeWiFi" => HasFreeWiFi ? "Да" : "Нет",
            _ => "Поле не найдено"
        };
    }

    /// <summary>
    /// Метод для вывода поля OccupiedRooms в шестнадцатеричном формате
    /// </summary>
    /// <returns> Поле в шестнадцатеричном виде </returns>
    public string GetOccupiedRoomsHex()
    {
        return OccupiedRooms.ToString("X");
    }

    /// <summary>
    /// Метод для изменения полей
    /// </summary>
    /// <param name="fieldName"> Название поля </param>
    /// <param name="newValue"> Новое значение поля </param>
    /// <returns> Статус об изменении </returns>
    public bool SetFieldValue(string fieldName, string newValue)
    {
        try
        {
            switch (fieldName)
            {
                case "Name":
                    Name = newValue;
                    break;

                case "OccupiedRooms":
                    if (int.TryParse(newValue, out int occupied))
                        OccupiedRooms = occupied;
                    else
                        return false;
                    break;

                case "TotalRooms":
                    if (int.TryParse(newValue, out int total))
                        TotalRooms = total;
                    else
                        return false;
                    break;

                case "PricePerDay":
                    if (decimal.TryParse(newValue, out decimal price))
                        PricePerDay = price;
                    else
                        return false;
                    break;

                case "Address":
                    Address = newValue;
                    break;

                case "Rating":
                    if (double.TryParse(newValue, out double rating))
                        Rating = rating;
                    else
                        return false;
                    break;

                case "HasFreeWiFi":
                    if (bool.TryParse(newValue, out bool wifi))
                        HasFreeWiFi = wifi;
                    else
                    {
                        if (newValue.ToLower() == "да")
                            HasFreeWiFi = true;
                        else if (newValue.ToLower() == "нет")
                            HasFreeWiFi = false;
                        else
                            return false;
                    }

                    break;

                default:
                    return false; // поле не найдено
            }
        }
        catch
        {
            return false; // произошли ошибки
        }

        return true; // успешно изменено
    }
}



