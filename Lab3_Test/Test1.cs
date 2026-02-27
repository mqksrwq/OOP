namespace Lab3_Test;
using Lab3;

[TestClass]
public sealed class Test1
{
    [TestInitialize]
    public void Setup()
    {
        // Очищаем Singleton перед каждым тестом
        HotelsHashtableCollection.Instance.Clear();
    }
    
    [TestMethod]
    public void Instance_Should_Return_Same_Object()
    {
        var instance1 = HotelsHashtableCollection.Instance;
        var instance2 = HotelsHashtableCollection.Instance;

        Assert.AreSame(instance1, instance2);
    }
    
    [TestMethod]
    public void Add_Should_Increase_Count()
    {
        var collection = HotelsHashtableCollection.Instance;

        var hotel = new Hotel("TestHotel", 1, 10, 1000, "Address", 5, true);
        collection.Add(hotel);

        Assert.AreEqual(1, collection.Count);
    }
}