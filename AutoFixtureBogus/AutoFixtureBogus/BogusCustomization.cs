using AutoFixture;
using Bogus;
using System.Reflection;
using AutoFixtureBogus.TestData;

namespace AutoFixtureBogus;

public class BogusCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var currentAssembly = Assembly.GetAssembly(typeof(TestGenerators)) 
            ?? throw new ArgumentNullException(nameof(fixture));

        var methods = from type in currentAssembly.GetTypes() 
                        where type.IsAbstract && type.IsSealed
                      from method in type.GetMethods() 
                        where method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Faker<>)
                      select method;

        foreach (var method in methods)
        {
            var registerMethod = GetType().GetMethod(nameof(Register));
            if (registerMethod == null)
                continue;

            var fakerReturnType = method.ReturnType.GetGenericArguments().Single();
            var genericRegisterMethod = registerMethod.MakeGenericMethod(fakerReturnType);
            genericRegisterMethod.Invoke(null, [fixture, method]);
        }
    }

    public static void Register<T>(IFixture fixture, MethodInfo method) where T : class
    {
        var generator = (Faker<T>?)method.Invoke(null, [])
            ?? throw new ArgumentException($"Method not found for {nameof(T)}");

        fixture.Register(() => generator.Generate());
    }
}
