namespace Lab3;

/// <summary>
/// Глобальное состояние приложения.
/// Реализовано как Singleton.
/// </summary>
public sealed class HotelAppState
{
    private static readonly Lazy<HotelAppState> _instance =
        new(() => new HotelAppState());

    public static HotelAppState Instance => _instance.Value;

    public HotelsHashtableCollection Hotels { get; }
    public HotelsCollectionListener Listener { get; }

    private HotelAppState()
    {
        Hotels = new HotelsHashtableCollection();
        Listener = new HotelsCollectionListener(Hotels);
    }
}