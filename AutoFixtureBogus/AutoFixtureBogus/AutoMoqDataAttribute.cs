using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace AutoFixtureBogus;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute()
        : base(() => new Fixture()
        .Customize(new AutoMoqCustomization())
        .Customize(new BogusCustomization()))
    {
    }    
    
    public AutoMoqDataAttribute(ICustomization postCustomization)
        : base(() => new Fixture()
        .Customize(new AutoMoqCustomization())
        .Customize(new BogusCustomization())
        .Customize(postCustomization))
    {
    }
}