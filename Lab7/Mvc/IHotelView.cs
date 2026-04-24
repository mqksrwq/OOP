namespace Lab7;

/// <summary>
/// Контракт представления формы гостиницы в MVC.
/// </summary>
public interface IHotelView
{
    event EventHandler? CreateRequested;
    event EventHandler? ApplyRequested;
    event EventHandler? CancelRequested;
    event EventHandler? ExitRequested;
    event EventHandler<HotelSelectedEventArgs>? EditRequested;

    bool TryGetHotelFromForm(out HotelFormInput input);
    void FillForm(Hotel hotel);

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

public readonly struct HotelFormInput
{
    public HotelFormInput(string name, int occupiedRooms, int totalRooms, decimal pricePerDay,
        string address, double rating, bool hasFreeWiFi)
    {
        Name = name;
        OccupiedRooms = occupiedRooms;
        TotalRooms = totalRooms;
        PricePerDay = pricePerDay;
        Address = address;
        Rating = rating;
        HasFreeWiFi = hasFreeWiFi;
    }

    public string Name { get; }
    public int OccupiedRooms { get; }
    public int TotalRooms { get; }
    public decimal PricePerDay { get; }
    public string Address { get; }
    public double Rating { get; }
    public bool HasFreeWiFi { get; }
}


