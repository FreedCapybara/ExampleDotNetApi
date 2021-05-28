using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleDotNetApi.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<Example, ExampleModel>();
            this.CreateMap<Example, ExampleListModel>();
            this.CreateMap<ExampleEditModel, Example>();
        }
    }
}
