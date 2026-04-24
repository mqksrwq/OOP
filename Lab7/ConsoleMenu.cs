using System;
using System.Globalization;

namespace Lab7;

internal static class ConsoleMenu
{
    public static void Run()
    {
        var root = HotelAppState.Instance.Hotels;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Lab7: управление гостиницами (консоль) ===");
            Console.WriteLine("1. Показать структуру");
            Console.WriteLine("2. Создать группу");
            Console.WriteLine("3. Создать гостиницу");
            Console.WriteLine("4. Найти гостиницу");
            Console.WriteLine("5. Изменить гостиницу");
            Console.WriteLine("6. Удалить компонент");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            var choice = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine(root.Describe());
                    break;
                case "2":
                    CreateGroup(root);
                    break;
                case "3":
                    CreateHotel(root);
                    break;
                case "4":
                    FindHotel(root);
                    break;
                case "5":
                    EditHotel(root);
                    break;
                case "6":
                    RemoveComponent(root);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неизвестный пункт меню.");
                    break;
            }
        }
    }

    private static void CreateGroup(HotelsHashtableCollection root)
    {
        Console.Write("Название новой группы: ");
        var name = (Console.ReadLine() ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Название группы не может быть пустым.");
            return;
        }

        if (root.Find(name) != null)
        {
            Console.WriteLine("Компонент с таким именем уже существует.");
            return;
        }

        Console.Write("Родительская группа (Enter = корневая): ");
        var parentName = (Console.ReadLine() ?? string.Empty).Trim();

        var parentGroup = ResolveGroup(root, parentName);
        if (parentGroup == null)
        {
            Console.WriteLine("Родительская группа не найдена.");
            return;
        }

        try
        {
            parentGroup.Add(new HotelsHashtableCollection(name));
            Console.WriteLine("Группа создана.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void CreateHotel(HotelsHashtableCollection root)
    {
        Console.Write("Группа: ");
        var groupName = (Console.ReadLine() ?? string.Empty).Trim();
        var group = ResolveGroup(root, groupName);
        if (group == null)
        {
            Console.WriteLine("Группа не найдена.");
            return;
        }

        Console.Write("Название гостиницы: ");
        var name = (Console.ReadLine() ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Название не может быть пустым.");
            return;
        }

        if (root.Find(name) != null)
        {
            Console.WriteLine("Компонент с таким именем уже существует.");
            return;
        }

        Console.Write("Адрес: ");
        var address = (Console.ReadLine() ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(address))
            address = "Адрес не задан";

        var total = ReadInt("Всего мест: ");
        var occupied = ReadInt("Заселено: ");
        var price = ReadDecimal("Цена за день: ");
        var rating = ReadDouble("Рейтинг: ");
        var hasWifi = ReadYesNo("Бесплатный Wi-Fi (y/n): ");

        var hotel = new Hotel(name, occupied, total, price, address, rating, hasWifi);

        try
        {
            group.Add(hotel);
            Console.WriteLine("Гостиница создана.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void FindHotel(HotelsHashtableCollection root)
    {
        Console.Write("Название гостиницы: ");
        var name = (Console.ReadLine() ?? string.Empty).Trim();
        if (TryFindHotelWithParent(root, name, out var group, out var hotel))
        {
            Console.WriteLine($"Группа: {group.Name}");
            Console.WriteLine(hotel.ToString());
            return;
        }

        Console.WriteLine("Гостиница не найдена.");
    }

    private static void EditHotel(HotelsHashtableCollection root)
    {
        Console.Write("Название гостиницы для изменения: ");
        var name = (Console.ReadLine() ?? string.Empty).Trim();

        if (!TryFindHotelWithParent(root, name, out var currentGroup, out var currentHotel))
        {
            Console.WriteLine("Гостиница не найдена.");
            return;
        }

        Console.WriteLine("Нажмите Enter, чтобы оставить текущее значение.");

        var targetGroup = PromptString($"Группа [{currentGroup.Name}]: ", currentGroup.Name);
        var newName = PromptString($"Название [{currentHotel.Name}]: ", currentHotel.Name);
        var address = PromptString($"Адрес [{currentHotel.Address}]: ", currentHotel.Address);
        var total = PromptInt($"Всего мест [{currentHotel.TotalRooms}]: ", currentHotel.TotalRooms);
        var occupied = PromptInt($"Заселено [{currentHotel.OccupiedRooms}]: ", currentHotel.OccupiedRooms);
        var price = PromptDecimal($"Цена за день [{currentHotel.PricePerDay}]: ", currentHotel.PricePerDay);
        var rating = PromptDouble($"Рейтинг [{currentHotel.Rating}]: ", currentHotel.Rating);
        var hasWifi = PromptBool($"Wi-Fi (y/n) [{(currentHotel.HasFreeWiFi ? "y" : "n")}]: ", currentHotel.HasFreeWiFi);

        var destination = ResolveGroup(root, targetGroup);
        if (destination == null)
        {
            Console.WriteLine("Целевая группа не найдена.");
            return;
        }

        if (!string.Equals(newName, currentHotel.Name, StringComparison.Ordinal) && root.Find(newName) != null)
        {
            Console.WriteLine("Компонент с таким именем уже существует.");
            return;
        }

        var updated = new Hotel(newName, occupied, total, price, address, rating, hasWifi);

        try
        {
            currentGroup.Remove(currentHotel.Name);
            destination.Add(updated);
            Console.WriteLine("Изменения сохранены.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void RemoveComponent(HotelsHashtableCollection root)
    {
        Console.Write("Имя компонента для удаления: ");
        var name = (Console.ReadLine() ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Имя не может быть пустым.");
            return;
        }

        if (string.Equals(name, root.Name, StringComparison.Ordinal))
        {
            Console.WriteLine("Нельзя удалить корневую коллекцию.");
            return;
        }

        if (TryRemoveRecursive(root, name))
            Console.WriteLine("Компонент удалён.");
        else
            Console.WriteLine("Компонент не найден.");
    }

    private static bool TryRemoveRecursive(HotelsHashtableCollection collection, string name)
    {
        if (collection.Remove(name))
            return true;

        foreach (var child in collection.Children)
        {
            if (child is HotelsHashtableCollection nested && TryRemoveRecursive(nested, name))
                return true;
        }

        return false;
    }

    private static HotelsHashtableCollection? ResolveGroup(HotelsHashtableCollection root, string groupName)
    {
        if (string.IsNullOrWhiteSpace(groupName))
            return root;

        return root.Find(groupName) as HotelsHashtableCollection;
    }

    private static bool TryFindHotelWithParent(HotelsHashtableCollection root, string name,
        out HotelsHashtableCollection group, out Hotel hotel)
    {
        foreach (var child in root.Children)
        {
            if (child is Hotel h && h.Name == name)
            {
                group = root;
                hotel = h;
                return true;
            }

            if (child is HotelsHashtableCollection nested && TryFindHotelWithParent(nested, name, out group, out hotel))
                return true;
        }

        group = null!;
        hotel = null!;
        return false;
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse((Console.ReadLine() ?? string.Empty).Trim(), out var value))
                return value;

            Console.WriteLine("Введите целое число.");
        }
    }

    private static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse((Console.ReadLine() ?? string.Empty).Trim(), NumberStyles.Number,
                    CultureInfo.CurrentCulture, out var value))
                return value;

            if (decimal.TryParse((Console.ReadLine() ?? string.Empty).Trim(), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out value))
                return value;

            Console.WriteLine("Введите число (decimal).");
        }
    }

    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse((Console.ReadLine() ?? string.Empty).Trim(), NumberStyles.Number,
                    CultureInfo.CurrentCulture, out var value))
                return value;

            if (double.TryParse((Console.ReadLine() ?? string.Empty).Trim(), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out value))
                return value;

            Console.WriteLine("Введите число (double).");
        }
    }

    private static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var value = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();
            if (value is "y" or "yes" or "д" or "да")
                return true;
            if (value is "n" or "no" or "н" or "нет")
                return false;

            Console.WriteLine("Введите y/n.");
        }
    }

    private static string PromptString(string prompt, string currentValue)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? currentValue : input.Trim();
    }

    private static int PromptInt(string prompt, int currentValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return currentValue;
            if (int.TryParse(input.Trim(), out var parsed))
                return parsed;
            Console.WriteLine("Введите целое число.");
        }
    }

    private static decimal PromptDecimal(string prompt, decimal currentValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return currentValue;

            var text = input.Trim();
            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out var parsed) ||
                decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out parsed))
                return parsed;

            Console.WriteLine("Введите число (decimal).");
        }
    }

    private static double PromptDouble(string prompt, double currentValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return currentValue;

            var text = input.Trim();
            if (double.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out var parsed) ||
                double.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out parsed))
                return parsed;

            Console.WriteLine("Введите число (double).");
        }
    }

    private static bool PromptBool(string prompt, bool currentValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return currentValue;

            var text = input.Trim().ToLowerInvariant();
            if (text is "y" or "yes" or "д" or "да")
                return true;
            if (text is "n" or "no" or "н" or "нет")
                return false;

            Console.WriteLine("Введите y/n.");
        }
    }
}
