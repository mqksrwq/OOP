using Lab3;

namespace Lab3_Test;

[TestClass]
public sealed class SingletonTests
{
    [TestMethod]
    public void HotelAppState_Instance_ShouldReturnSameObject()
    {
        var first = HotelAppState.Instance;
        var second = HotelAppState.Instance;

        Assert.AreSame(first, second);
        Assert.AreSame(first.Hotels, second.Hotels);
    }
}
