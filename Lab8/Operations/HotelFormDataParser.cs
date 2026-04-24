using System.Windows.Forms;

namespace Lab8.Operations;

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

        var groupName = data.GroupName.Trim();
        if (string.IsNullOrWhiteSpace(groupName))
        {
            MessageBox.Show("Введите название группы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        if (!int.TryParse(data.TotalRoomsText.Trim(), out int total))
        {
            MessageBox.Show("Некорректное число в поле 'Всего мест'.", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return false;
        }

        if (!int.TryParse(data.OccupiedRoomsText.Trim(), out int occupied))
        {
            MessageBox.Show("Некорректное число в поле 'Заселено'.", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return false;
        }

        if (!decimal.TryParse(data.PricePerDayText.Trim(), out decimal price))
        {
            MessageBox.Show("Некорректное число в поле 'Цена/день'.", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return false;
        }

        if (!double.TryParse(data.RatingText.Trim(), out double rating))
        {
            MessageBox.Show("Некорректное число в поле 'Рейтинг'.", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return false;
        }

        group = resolvedGroup;
        hotel = new Hotel(
            data.Name.Trim(),
            occupied,
            total,
            price,
            data.Address.Trim(),
            rating,
            data.HasFreeWiFi
        );

        return true;
    }
}




