using Application.Todos.Commands;
using FluentAssertions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

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
        public async Task CreateTodo_ValidationFailure_ReturnsFailureCommandResponseAndTodoDoesNotExistInDatabase()
        {
            using var context = new DataContext(_dbContextOptions);

            var createTodoDto = TodosDataHelper.GenerateInvalidCreateTodoDto();

            var command = new CreateTodo.Command(createTodoDto);
            var handler = new CreateTodo.Handler(context, _fixture.Mapper);

            var commandResponse = await handler.Handle(command, default);

            commandResponse.Should().BeOfType<FailureCommandResponse>();

            var createdTodo = await context.Todos.FindAsync(commandResponse.ItemId);

            createdTodo.Should().BeNull();
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

        [Fact]
        [Trait("Category", "Unit")]
        public async Task DeleteTodo_Success_TodoWasDeletedInDatabase()
        {
            using var context = new DataContext(_dbContextOptions);

            var singleTodo = TodosDataHelper.GenerateSingleTodo();
            await context.Todos.AddAsync(singleTodo);
            await context.SaveChangesAsync();

            var command = new DeleteTodo.Command(singleTodo.Id);
            var handler = new DeleteTodo.Handler(context, _fixture.Mapper);

            var commandResponse = await handler.Handle(command, default);

            var editedTodo = await context.Todos.FindAsync(singleTodo.Id);

            editedTodo.Should().BeNull();
        }
    }
}
