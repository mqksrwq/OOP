using System.Globalization;
using Lab7.Operations;

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
        var nameInput = Console.ReadLine();
        if (!FieldValidation.TryNormalizeName(nameInput, "Название группы", 60, out var name, out var error))
        {
            Console.WriteLine(error);
            return;
        }

        if (root.Find(name) != null)
        {
            Console.WriteLine("Компонент с таким именем уже существует.");
            return;
        }

        Console.Write("Родительская группа (Enter = корневая): ");
        var parentInput = Console.ReadLine();
        var parentName = string.IsNullOrWhiteSpace(parentInput) ? string.Empty : parentInput.Trim();

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
        var groupInput = Console.ReadLine();
        if (!FieldValidation.TryNormalizeName(groupInput, "Название группы", 60, out var groupName,
                out var groupError))
        {
            Console.WriteLine(groupError);
            return;
        }

        var group = ResolveGroup(root, groupName);
        if (group == null)
        {
            Console.WriteLine("Группа не найдена.");
            return;
        }

        Console.Write("Название гостиницы: ");
        var nameInput = Console.ReadLine();
        if (!FieldValidation.TryNormalizeName(nameInput, "Название гостиницы", 80, out var name, out var nameError))
        {
            Console.WriteLine(nameError);
            return;
        }

        if (root.Find(name) != null)
        {
            Console.WriteLine("Компонент с таким именем уже существует.");
            return;
        }

        Console.Write("Адрес: ");
        var addressInput = Console.ReadLine();
        if (!FieldValidation.TryNormalizeAddress(addressInput, out var address, out var addressError))
        {
            Console.WriteLine(addressError);
            return;
        }

        var total = ReadPositiveInt("Всего мест: ");
        var occupied = ReadOccupiedInt("Заселено: ", total);
        var price = ReadPositiveDecimal("Цена за день: ");
        var rating = ReadRating("Рейтинг: ");
        var hasWifi = ReadYesNo("Бесплатный Wi-Fi (y/n): ");

        try
        {
            group.Add(new Hotel(name, occupied, total, price, address, rating, hasWifi));
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
        var input = Console.ReadLine();
        if (!FieldValidation.TryNormalizeName(input, "Название гостиницы", 80, out var name, out var error))
        {
            Console.WriteLine(error);
            return;
        }

        if (TryFindHotelWithParent(root, name, out var group, out var hotel))
        {
            Console.WriteLine($"Группа: {group.Name}");
            Console.WriteLine(hotel);
            return;
        }

        Console.WriteLine("Гостиница не найдена.");
    }

    private static void EditHotel(HotelsHashtableCollection root)
    {
        Console.Write("Название гостиницы для изменения: ");
        var input = Console.ReadLine();
        if (!FieldValidation.TryNormalizeName(input, "Название гостиницы", 80, out var searchName, out var searchError))
        {
            Console.WriteLine(searchError);
            return;
        }

        if (!TryFindHotelWithParent(root, searchName, out var currentGroup, out var currentHotel))
        {
            Console.WriteLine("Гостиница не найдена.");
            return;
        }

        Console.WriteLine("Нажмите Enter, чтобы оставить текущее значение.");

        var targetGroupName = PromptGroupName($"Группа [{currentGroup.Name}]: ", currentGroup.Name);
        var newName = PromptHotelName($"Название [{currentHotel.Name}]: ", currentHotel.Name);
        var address = PromptAddress($"Адрес [{currentHotel.Address}]: ", currentHotel.Address);
        var total = PromptTotal($"Всего мест [{currentHotel.TotalRooms}]: ", currentHotel.TotalRooms);
        var occupied = PromptOccupied($"Заселено [{currentHotel.OccupiedRooms}]: ", currentHotel.OccupiedRooms, total);
        var price = PromptPrice($"Цена за день [{currentHotel.PricePerDay}]: ", currentHotel.PricePerDay);
        var rating = PromptRating($"Рейтинг [{currentHotel.Rating}]: ", currentHotel.Rating);
        var hasWifi = PromptBool($"Wi-Fi (y/n) [{(currentHotel.HasFreeWiFi ? "y" : "n")}]: ", currentHotel.HasFreeWiFi);

        var destination = ResolveGroup(root, targetGroupName);
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

        try
        {
            currentGroup.Remove(currentHotel.Name);
            destination.Add(new Hotel(newName, occupied, total, price, address, rating, hasWifi));
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
        var input = Console.ReadLine();
        if (!FieldValidation.TryNormalizeName(input, "Имя компонента", 80, out var name, out var error))
        {
            Console.WriteLine(error);
            return;
        }

        if (string.Equals(name, root.Name, StringComparison.Ordinal))
        {
            Console.WriteLine("Нельзя удалить корневую коллекцию.");
            return;
        }

        Console.WriteLine(TryRemoveRecursive(root, name) ? "Компонент удалён." : "Компонент не найден.");
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

    private static int ReadPositiveInt(string prompt)
    {
        while (true)
        {
            var value = ReadInt(prompt);
            if (value > 0)
                return value;
            Console.WriteLine("Значение должно быть больше 0.");
        }
    }

    private static int ReadOccupiedInt(string prompt, int total)
    {
        while (true)
        {
            var value = ReadInt(prompt);
            if (value < 0)
            {
                Console.WriteLine("Значение не может быть отрицательным.");
                continue;
            }

            if (value > total)
            {
                Console.WriteLine("Заселено не может быть больше общего количества мест.");
                continue;
            }

            return value;
        }
    }

    private static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = (Console.ReadLine() ?? string.Empty).Trim();
            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out var value) ||
                decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                return value;

            Console.WriteLine("Введите число (decimal).");
        }
    }

    private static decimal ReadPositiveDecimal(string prompt)
    {
        while (true)
        {
            var value = ReadDecimal(prompt);
            if (value > 0)
                return value;
            Console.WriteLine("Значение должно быть больше 0.");
        }
    }

    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = (Console.ReadLine() ?? string.Empty).Trim();
            if (double.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out var value) ||
                double.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                return value;

            Console.WriteLine("Введите число (double).");
        }
    }

    private static double ReadRating(string prompt)
    {
        while (true)
        {
            var value = ReadDouble(prompt);
            if (value >= 0 && value <= 5)
                return value;
            Console.WriteLine("Рейтинг должен быть в диапазоне от 0 до 5.");
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

    private static string PromptGroupName(string prompt, string currentValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            var candidate = string.IsNullOrWhiteSpace(input) ? currentValue : input;
            if (FieldValidation.TryNormalizeName(candidate, "Название группы", 60, out var normalized, out var error))
                return normalized;

            Console.WriteLine(error);
        }
    }

    private static string PromptHotelName(string prompt, string currentValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            var candidate = string.IsNullOrWhiteSpace(input) ? currentValue : input;
            if (FieldValidation.TryNormalizeName(candidate, "Название гостиницы", 80, out var normalized, out var error))
                return normalized;

            Console.WriteLine(error);
        }
    }

    private static string PromptAddress(string prompt, string currentValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            var candidate = string.IsNullOrWhiteSpace(input) ? currentValue : input;
            if (FieldValidation.TryNormalizeAddress(candidate, out var normalized, out var error))
                return normalized;

            Console.WriteLine(error);
        }
    }

    private static int PromptTotal(string prompt, int currentValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return currentValue;

            if (FieldValidation.TryParseTotalRooms(input, out var value, out var error))
                return value;

            Console.WriteLine(error);
        }
    }

    private static int PromptOccupied(string prompt, int currentValue, int total)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return currentValue <= total ? currentValue : total;

            if (FieldValidation.TryParseOccupiedRooms(input, total, out var value, out var error))
                return value;

            Console.WriteLine(error);
        }
    }

    private static decimal PromptPrice(string prompt, decimal currentValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return currentValue;

            if (FieldValidation.TryParsePrice(input, out var value, out var error))
                return value;

            Console.WriteLine(error);
        }
    }

    private static double PromptRating(string prompt, double currentValue)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return currentValue;

            if (FieldValidation.TryParseRating(input, out var value, out var error))
                return value;

            Console.WriteLine(error);
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
