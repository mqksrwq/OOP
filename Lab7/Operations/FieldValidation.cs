using System.Globalization;
using System.Text.RegularExpressions;

namespace Lab7.Operations;

internal static class FieldValidation
{
    private static readonly Regex NamePattern = new(@"^[\p{L}\p{Nd}][\p{L}\p{Nd}\s\-\.\(\)]*$", RegexOptions.Compiled);

    public static bool TryNormalizeName(string? input, string fieldName, int maxLength, out string value, out string error)
    {
        value = (input ?? string.Empty).Trim();
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(value))
        {
            error = $"Поле '{fieldName}' не может быть пустым.";
            return false;
        }

        if (Regex.IsMatch(value, @"\s{2,}"))
        {
            error = $"Поле '{fieldName}' не должно содержать подряд несколько пробелов.";
            return false;
        }

        if (value.Length > maxLength)
        {
            error = $"Поле '{fieldName}' слишком длинное (максимум {maxLength} символов).";
            return false;
        }

        if (!NamePattern.IsMatch(value))
        {
            error = $"Поле '{fieldName}' содержит недопустимые символы.";
            return false;
        }

        return true;
    }

    public static bool TryNormalizeAddress(string? input, out string value, out string error)
    {
        value = (input ?? string.Empty).Trim();
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(value))
        {
            error = "Поле 'Адрес' не может быть пустым.";
            return false;
        }

        if (Regex.IsMatch(value, @"\s{2,}"))
        {
            error = "Поле 'Адрес' не должно содержать подряд несколько пробелов.";
            return false;
        }

        if (value.Length > 120)
        {
            error = "Поле 'Адрес' слишком длинное (максимум 120 символов).";
            return false;
        }

        return true;
    }

    public static bool TryParseTotalRooms(string text, out int value, out string error)
    {
        error = string.Empty;
        if (!int.TryParse(text.Trim(), out value))
        {
            error = "Некорректное число в поле 'Всего мест'.";
            return false;
        }

        if (value <= 0)
        {
            error = "Поле 'Всего мест' должно быть больше 0.";
            return false;
        }

        return true;
    }

    public static bool TryParseOccupiedRooms(string text, int totalRooms, out int value, out string error)
    {
        error = string.Empty;
        if (!int.TryParse(text.Trim(), out value))
        {
            error = "Некорректное число в поле 'Заселено'.";
            return false;
        }

        if (value < 0)
        {
            error = "Поле 'Заселено' не может быть отрицательным.";
            return false;
        }

        if (value > totalRooms)
        {
            error = "Поле 'Заселено' не может быть больше поля 'Всего мест'.";
            return false;
        }

        return true;
    }

    public static bool TryParsePrice(string text, out decimal value, out string error)
    {
        error = string.Empty;
        var trimmed = text.Trim();
        if (!decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.CurrentCulture, out value) &&
            !decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
        {
            error = "Некорректное число в поле 'Цена/день'.";
            return false;
        }

        if (value <= 0)
        {
            error = "Поле 'Цена/день' должно быть больше 0.";
            return false;
        }

        return true;
    }

    public static bool TryParseRating(string text, out double value, out string error)
    {
        error = string.Empty;
        var trimmed = text.Trim();
        if (!double.TryParse(trimmed, NumberStyles.Number, CultureInfo.CurrentCulture, out value) &&
            !double.TryParse(trimmed, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
        {
            error = "Некорректное число в поле 'Рейтинг'.";
            return false;
        }

        if (value < 0 || value > 5)
        {
            error = "Поле 'Рейтинг' должно быть в диапазоне от 0 до 5.";
            return false;
        }

        return true;
    }
}
