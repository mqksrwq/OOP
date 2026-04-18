namespace Lab7.Operations;

/// <summary>
/// Данные формы для создания или обновления гостиницы
/// </summary>
/// <param name="GroupName">Имя группы</param>
/// <param name="Name">Название гостиницы</param>
/// <param name="Address">Адрес гостиницы</param>
/// <param name="TotalRoomsText">Текстовое значение количества мест</param>
/// <param name="OccupiedRoomsText">Текстовое значение занятых мест</param>
/// <param name="PricePerDayText">Текстовое значение цены за день</param>
/// <param name="RatingText">Текстовое значение рейтинга</param>
/// <param name="HasFreeWiFi">Наличие Wi-Fi</param>
public sealed record HotelFormData(
    string GroupName,
    string Name,
    string Address,
    string TotalRoomsText,
    string OccupiedRoomsText,
    string PricePerDayText,
    string RatingText,
    bool HasFreeWiFi
);



