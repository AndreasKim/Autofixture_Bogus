namespace AutoFixtureBogus.TestData;

public class ExampleService(IExampleRepository exampleRepository)
{
    private readonly IExampleRepository _exampleRepository = exampleRepository;

    public bool PerformAction() 
    {
        return _exampleRepository.Save();
    }
}

public interface IExampleRepository
{
    bool Save();
}

public class ExampleRepository : IExampleRepository
{
    public bool Save()
    {
        return true;
    }
}
