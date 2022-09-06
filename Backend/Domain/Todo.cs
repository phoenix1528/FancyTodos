using Domain.Validators;
using FluentValidation;

namespace Domain
{
    public class Todo
    {
        public Guid Id { get; private set; } = Guid.Empty;
        public string Title { get; private set; } = string.Empty;
        public DateTime Date { get; private set; } = DateTime.MinValue;
        public string Description { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string Venue { get; private set; } = string.Empty;

        public Todo(string title, DateTime date, string description, string category, string city, string venue)
        {
            Title = title;
            Date = date;
            Description = description;
            Category = category;
            City = city;
            Venue = venue;
        }

        public static Todo Create(Todo todo)
        {
            new TodoValidator().ValidateAndThrow(todo);
            return todo;
        }

        public void Update(Todo todo)
        {
            new TodoValidator().ValidateAndThrow(todo);

            Title = todo.Title;
            Date = todo.Date;
            Description = todo.Description;
            Category = todo.Category;
            City = todo.City;
            Venue = todo.Venue;
        }
    }
}
