namespace Lab8;

/// <summary>
/// Контракт представления формы гостиницы в MVP.
/// </summary>
public interface IHotelView
{
    event EventHandler? CreateRequested;
    event EventHandler? ApplyRequested;
    event EventHandler? CancelRequested;
    event EventHandler? ExitRequested;
    event EventHandler<HotelSelectedEventArgs>? EditRequested;

    string HotelNameText { get; set; }
    string OccupiedRoomsText { get; set; }
    string TotalRoomsText { get; set; }
    string PricePerDayText { get; set; }
    string AddressText { get; set; }
    string RatingText { get; set; }
    bool HasFreeWiFiChecked { get; set; }

    void ShowError(string message, string title = "Ошибка");
    void ShowWarning(string message, string title = "Предупреждение");
    void RenderHotels();
    void SetEditMode(bool enabled);
    void ClearFormFields();
    void CloseView();
}

public sealed class HotelSelectedEventArgs : EventArgs
{
    public HotelSelectedEventArgs(Hotel hotel)
    {
        Hotel = hotel;
    }

    public Hotel Hotel { get; }
}
