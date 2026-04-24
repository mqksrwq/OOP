using System.Windows.Forms;

namespace Lab7.Operations;

/// <summary>
/// Парсер данных формы гостиницы
/// </summary>
internal static class HotelFormDataParser
{
    /// <summary>
    /// Пытается создать объекты группы и гостиницы из данных формы
    /// </summary>
    /// <param name="data">Входные данные формы</param>
    /// <param name="group">Возвращаемая группа гостиниц</param>
    /// <param name="hotel">Возвращаемая гостиница</param>
    /// <returns>Признак успешного парсинга и создания объектов</returns>
    public static bool TryBuild(HotelFormData data, out HotelsHashtableCollection? group, out Hotel? hotel)
    {
        group = null;
        hotel = null;

        if (!FieldValidation.TryNormalizeName(data.GroupName, "Название группы", 60, out var groupName,
                out var error))
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (!FieldValidation.TryNormalizeName(data.Name, "Название гостиницы", 80, out var hotelName, out error))
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (!FieldValidation.TryNormalizeAddress(data.Address, out var address, out error))
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        var root = HotelAppState.Instance.Hotels;
        var groupComponent = root.Find(groupName);
        if (groupComponent is not HotelsHashtableCollection resolvedGroup)
        {
            MessageBox.Show("Группа не найдена. Создайте группу на соответствующей форме.", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (!FieldValidation.TryParseTotalRooms(data.TotalRoomsText, out int total, out error))
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (!FieldValidation.TryParseOccupiedRooms(data.OccupiedRoomsText, total, out int occupied, out error))
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (!FieldValidation.TryParsePrice(data.PricePerDayText, out decimal price, out error))
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (!FieldValidation.TryParseRating(data.RatingText, out double rating, out error))
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        group = resolvedGroup;
        hotel = new Hotel(
            hotelName,
            occupied,
            total,
            price,
            address,
            rating,
            data.HasFreeWiFi
        );

        return true;
    }
}



