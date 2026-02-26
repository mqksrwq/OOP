namespace Lab1_Test;
using Lab1;
[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void TestMethod1()
    {
        Hotel hotel = new Hotel();
        
        Assert.AreEqual("Неизвестно",hotel.Name);
        Assert.AreEqual(0,hotel.OccupiedRooms);
        Assert.AreEqual(10,hotel.TotalRooms);
        Assert.AreEqual(1000m,hotel.PricePerDay);
        Assert.AreEqual("Адрес не задан",hotel.Address);
        Assert.AreEqual(3.5,hotel.Rating);
        Assert.AreEqual(true,hotel.HasFreeWiFi);
    }
}