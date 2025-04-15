using Microsoft.EntityFrameworkCore;

namespace DevNetCore.SimpleRepositoryExample;

public class SimpleRepositoryExample : SimpleRepository.Implementation.SimpleRepository
{
    public SimpleRepositoryExample(SimpleRepositoryExampleContext dbContext) : base(dbContext)
    {
    }
}