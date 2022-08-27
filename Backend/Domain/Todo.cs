namespace Domain
{
    public class Todo
    {
        public Todo(string title, DateTime date, string description, string category, string city, string venue)
        {
            Title = title;
            Date = date;
            Description = description;
            Category = category;
            City = city;
            Venue = venue;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public string City { get; private set; }
        public string Venue { get; private set; }
    }
}
