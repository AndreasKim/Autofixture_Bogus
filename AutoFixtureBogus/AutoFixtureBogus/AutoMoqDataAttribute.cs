using System.Reflection;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Bogus;

namespace AutoFixtureBogus
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(() => new Fixture()
            .Customize(new AutoMoqCustomization())
            .Customize(new TestGeneratorCustomization()))
        {
        }
    }

    public class TestGeneratorCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var currentAssembly = Assembly.GetAssembly(typeof(TestGenerators)) 
                ?? throw new ArgumentNullException(nameof(fixture));

            var methods = from type in currentAssembly.GetTypes() 
                            where (type.IsAbstract && type.IsSealed)
                          from method in type.GetMethods() 
                            where (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Faker<>))
                          select method;

            foreach (var method in methods)
            {
                var registerMethod = GetType().GetMethod(nameof(TestGeneratorCustomization.Register));
                if (registerMethod == null)
                    continue;

                var fakerReturnType = method.ReturnType.GetGenericArguments().Single();
                var genericRegisterMethod = registerMethod.MakeGenericMethod(fakerReturnType);
                genericRegisterMethod.Invoke(null, new object[] { fixture, method });
            }

            Console.WriteLine();
        }

        public static void Register<T>(IFixture fixture, MethodInfo method) where T : class
        {
            var generator = (Faker<T>?)method.Invoke(null, new object[] { })
                ?? throw new ArgumentException($"Method not found for {nameof(T)}");

            fixture.Register<T>(() => generator.Generate());
        }
    }
}
