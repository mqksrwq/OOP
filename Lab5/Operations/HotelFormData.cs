namespace Lab5.Operations;

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

