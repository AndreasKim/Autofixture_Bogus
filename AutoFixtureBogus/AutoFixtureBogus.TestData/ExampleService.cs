namespace AutoFixtureBogus.TestData;

public class ExampleService
{
    private readonly IExampleRepository exampleRepository;

    public ExampleService(IExampleRepository exampleRepository)
    {
        this.exampleRepository = exampleRepository;
    }

    public bool PerformAction() 
    {
        return exampleRepository.Save();
    }
}

public interface IExampleRepository
{
    bool Save();
}

public class ExampleRepository : IExampleRepository
{
    public ExampleRepository()
    {

    }

    public bool Save()
    {
        return true;
    }
}
