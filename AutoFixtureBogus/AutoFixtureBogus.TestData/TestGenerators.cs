using Bogus;

namespace AutoFixtureBogus.TestData;

public static class TestGenerators
{
    public static Faker<User> User() => new Faker<User>()
        .RuleFor(u => u.FirstName, f => f.Name.FirstName())
        .RuleFor(u => u.LastName, f => f.Name.LastName())
        .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
        .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
        .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
        .RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")
        .RuleFor(u => u.SomeGuid, Guid.NewGuid)
        .RuleFor(u => u.CartId, f => Guid.NewGuid())
        .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName);
        
}