namespace Domain
{
    public class Todo
    {
        public Todo()
        {

        }

        public Todo(string title, DateTime date, string description, string category, string city, string venue)
        {
            Id = Guid.NewGuid();
            Title = title;
            Date = date;
            Description = description;
            Category = category;
            City = city;
            Venue = venue;
        }

        public Guid Id { get; private set; } = Guid.Empty;
        public string Title { get; private set; } = string.Empty;
        public DateTime Date { get; private set; } = DateTime.MinValue;
        public string Description { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string Venue { get; private set; } = string.Empty;
    }
}
