using ExampleDotNetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleDotNetApi.Services
{
    public interface IExampleService
    {
        Task<IEnumerable<Example>> GetExamplesAsync();

        Task<Example> GetExampleAsync(long id);

        Task<Example> AddOrUpdateExampleAsync(Example example);

        Task<Example> DeleteExampleAsync(long id);
    }
}
