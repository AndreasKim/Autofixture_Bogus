using AutoFixture;
using AutoFixtureBogus.TestData;
using Moq;

namespace AutoFixtureBogus;

public class NoUserNameDataAttribute() 
    : AutoMoqDataAttribute(new NoUserNameCustomization());

public class NoUserNameCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => TestGenerators.User().RuleFor(p => p.UserName, "").Generate());

        var mock = new Mock<IExampleRepository>();
        mock.Setup(p => p.Save()).Returns(true);
        fixture.Inject(mock);
    }
}
