namespace Lab7;

/// <summary>
/// Глобальное состояние приложения.
/// Реализовано как Singleton.
/// </summary>
public sealed class HotelAppState
{
    /// <summary>
    /// Лениво инициализируемый экземпляр состояния приложения.
    /// </summary>
    private static readonly Lazy<HotelAppState> _instance =
        new(() => new HotelAppState());

    /// <summary>
    /// Единственный экземпляр состояния приложения
    /// </summary>
    public static HotelAppState Instance => _instance.Value;

    /// <summary>
    /// Коллекция всех гостиниц и их групп
    /// </summary>
    public HotelsHashtableCollection Hotels { get; }

    /// <summary>
    /// Слушатель событий изменения коллекции гостиниц
    /// </summary>
    public HotelsCollectionListener Listener { get; }

    /// <summary>
    /// Скрытый конструктор
    /// </summary>
    private HotelAppState()
    {
        Hotels = new HotelsHashtableCollection();
        Listener = new HotelsCollectionListener(Hotels);
    }
}

