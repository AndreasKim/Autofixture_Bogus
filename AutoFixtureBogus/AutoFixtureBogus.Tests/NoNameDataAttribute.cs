using AutoFixture;
using AutoFixtureBogus.TestData;

namespace AutoFixtureBogus;

public class NoNameDataAttribute() 
    : AutoMoqDataAttribute(new NoNameCustomization());

public class NoNameCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<User>(p => p.With(g => g.FirstName, "").With(g => g.LastName, ""));
    }
}
