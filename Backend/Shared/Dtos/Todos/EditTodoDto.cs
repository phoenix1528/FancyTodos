namespace Shared.Dtos.Todos
{
    public class EditTodoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }

        public EditTodoDto(Guid id, string title, DateTime date, string description, string category, string city, string venue)
        {
            Id = id;
            Title = title;
            Date = date;
            Description = description;
            Category = category;
            City = city;
            Venue = venue;
        }
    }
}
