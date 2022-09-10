using Domain.Validators;
using FluentValidation;

namespace Domain
{
    public class Todo
    {
        public Todo(string title, DateTime date, string description, string category, string city, string venue, Guid id = default)
        {
            Title = title;
            Date = date;
            Description = description;
            Category = category;
            City = city;
            Venue = venue;
            Id = id;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public string City { get; private set; }
        public string Venue { get; private set; }

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
