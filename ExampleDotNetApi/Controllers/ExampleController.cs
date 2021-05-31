using AutoMapper;
using ExampleDotNetApi.Models;
using ExampleDotNetApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleDotNetApi.Controllers
{
    [ApiController]
    [Route("example")]
    public class ExampleController : ControllerBase
    {
        private readonly IExampleService exampleService;

        private readonly IMapper mapper;

        public ExampleController(IExampleService exampleService, IMapper mapper)
        {
            this.exampleService = exampleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetExamples()
        {
            var examples = await this.exampleService.GetExamplesAsync();
            var result = this.mapper.Map<IEnumerable<ExampleListModel>>(examples);
            return this.Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExample([FromRoute] long id)
        {
            var example = await this.exampleService.GetExampleAsync(id);
            if (example == null)
            {
                return this.NotFound();
            }

            var result = this.mapper.Map<ExampleModel>(example);
            return this.Ok(result);
        }

        [HttpPost]
        [HttpPost("{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> SaveExample([FromRoute] long? id, [FromBody] ExampleEditModel editModel)
        {
            Example example;
            if (id != null)
            {
                example = await this.exampleService.GetExampleAsync(id.Value);
                if (example == null)
                {
                    return this.NotFound();
                }
                this.mapper.Map(editModel, example);
            }
            else
            {
                example = this.mapper.Map<Example>(editModel);
            }

            var result = await this.exampleService.AddOrUpdateExampleAsync(example);

            var model = this.mapper.Map<ExampleModel>(result);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExample([FromRoute] long id)
        {
            var example = await this.exampleService.DeleteExampleAsync(id);
            if (example == null)
            {
                return this.NotFound();
            }

            var result = this.mapper.Map<ExampleModel>(example);
            return this.Ok(result);
        }
    }
}
