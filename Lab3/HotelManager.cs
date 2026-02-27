namespace Lab3;

public class HotelManager
{
    private static readonly Lazy<HotelManager> _instance =
        new Lazy<HotelManager>(() => new HotelManager());

    public static HotelManager Instance => _instance.Value;

    private List<Hotel> _hotels;
    
    private HotelManager()
    {
        _hotels = new List<Hotel>();
    }

    public void AddHotel(Hotel hotel)
    {
        _hotels.Add(hotel);
    }

    public void RemoveHotel(Hotel hotel)
    {
        _hotels.Remove(hotel);
    }

    public List<Hotel> GetAllHotels()
    {
        return _hotels;
    }
}