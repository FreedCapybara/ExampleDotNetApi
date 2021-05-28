using ExampleDotNetApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleDotNetApi.Services
{
    public class ExampleService : IExampleService
    {
        private readonly ApplicationDbContext context;

        public ExampleService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Example>> GetExamplesAsync()
        {
            var examples = this.context.Examples;
            return await Task.FromResult(examples);
        }

        public async Task<Example> GetExampleAsync(long id)
        {
            var example = await this.context.Examples.FirstOrDefaultAsync(x => x.Id == id);
            return example;
        }

        public async Task<Example> AddOrUpdateExampleAsync(Example example)
        {
            if (example.Id > 0)
            {
                this.context.Update(example);
            }
            else
            {
                await this.context.AddAsync(example);
            }

            await this.context.SaveChangesAsync();
            return example;
        }

        public async Task<Example> DeleteExampleAsync(long id)
        {
            var example = await this.context.Examples.FirstOrDefaultAsync(x => x.Id == id);
            if (example != null)
            {
                this.context.Remove(example);
                await this.context.SaveChangesAsync();
            }

            return example;
        }
    }
}
