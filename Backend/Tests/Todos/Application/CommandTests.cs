using Application.Mapping;
using Application.Todos.Commands;
using AutoMapper;
using FluentAssertions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Tests.Todos.Api;

namespace Tests.Todos.Application
{
    public class CommandTests : IClassFixture<ApplicationTestsFixture>
    {
        private readonly DbContextOptions _dbContextOptions;
        private readonly ApplicationTestsFixture _fixture;

        public CommandTests(ApplicationTestsFixture fixture)
        {
            _fixture = fixture;
            _dbContextOptions = SqliteInMemoryDb.CreateSqliteInMemoryDbOptions();

            using var context = new DataContext(_dbContextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task CreateTodo_Success_TodoExistsInDatabase()
        {
            using var context = new DataContext(_dbContextOptions);

            var createTodoDto = TodosDataHelper.GenerateCreateTodoDto();

            var command = new CreateTodo.Command(createTodoDto);
            var handler = new CreateTodo.Handler(context, _fixture.Mapper);

            var commandResponse = await handler.Handle(command, default);

            var createdTodo = await context.Todos.FindAsync(commandResponse.ItemId);

            createdTodo.Should().NotBeNull();

            commandResponse.ItemId.Should().Be(createdTodo!.Id);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task EditTodo_Success_TodoWasUpdatedInDatabase()
        {
            using var context = new DataContext(_dbContextOptions);

            var singleTodo = TodosDataHelper.GenerateSingleTodo();
            await context.Todos.AddAsync(singleTodo);
            await context.SaveChangesAsync();

            var editTodoDto = TodosDataHelper.GenerateEditTodoDto();

            var command = new EditTodo.Command(editTodoDto);
            var handler = new EditTodo.Handler(context, _fixture.Mapper);

            var commandResponse = await handler.Handle(command, default);

            var editedTodo = await context.Todos.FindAsync(singleTodo.Id);

            editedTodo.Should().NotBeNull();

            editedTodo!.City.Should().Be(editTodoDto.City);
            editedTodo!.Venue.Should().Be(editTodoDto.Venue);
            editedTodo!.Category.Should().Be(editTodoDto.Category);
            editedTodo!.Date.Should().Be(editTodoDto.Date);
            editedTodo!.Description.Should().Be(editTodoDto.Description);
        }
    }
}
