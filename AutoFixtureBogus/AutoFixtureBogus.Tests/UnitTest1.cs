using AutoFixture.Xunit2;
using AutoFixtureBogus.TestData;
using Moq;
using Xunit;

namespace AutoFixtureBogus.Tests;

public class UnitTest1
{
    [Theory, AutoMoqData]
    public void Test1(User user)
    {
        Console.WriteLine();
    }       
    
    [Theory, NoNameData]
    public void Test2(User user)
    {
        Console.WriteLine();
    }    

    [Theory, NoUserNameData]
    public void Test3(User user)
    {
        Console.WriteLine();
    }    
    
    [Theory, NoUserNameData]
    public void Test4([Frozen]Mock<IExampleRepository> mock, ExampleService service)
    {
        mock.Setup(p => p.Save()).Returns(true);

        var result = service.PerformAction();
        Console.WriteLine();
    }
}