using Application.Mapping;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Todos.Application
{
    public class ApplicationTestsFixture : IDisposable
    {
        public readonly IMapper Mapper;

        public ApplicationTestsFixture()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TodoProfile());
            });

            Mapper = mockMapper.CreateMapper();
        }

        public void Dispose()
        {
        }
    }
}
