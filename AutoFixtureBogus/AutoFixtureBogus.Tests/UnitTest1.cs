namespace AutoFixtureBogus.Tests
{
    public class UnitTest1
    {
        [Theory, AutoMoqData]
        public void Test1(User user)
        {
            Console.WriteLine();
        }
    }
}