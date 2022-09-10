using Domain;

namespace Tests.Todos
{
    public static class TodosDataHelper
    {
        public static IEnumerable<Todo> GenerateTodos()
        {
            var todos = new List<Todo>
            {
                new Todo
                (
                    "Past Todo 1",
                    DateTime.Now.AddMonths(-2),
                    "Todo 2 months ago",
                    "drinks",
                    "London",
                    "Pub"
                ),
                new Todo
                (
                    "Past Todo 2",
                    DateTime.Now.AddMonths(-1),
                    "Todo 1 month ago",
                    "culture",
                    "Paris",
                    "Louvre"
                ),
                new Todo
                (
                    "Future Todo 1",
                    DateTime.Now.AddMonths(1),
                    "Todo 1 month in future",
                    "culture",
                    "London",
                    "Natural History Museum"
                ),
                new Todo
                (
                    "Future Todo 2",
                    DateTime.Now.AddMonths(2),
                    "Todo 2 months in future",
                    "music",
                    "London",
                    "O2 Arena"
                ),
                new Todo
                (
                    "Future Todo 3",
                    DateTime.Now.AddMonths(3),
                    "Todo 3 months in future",
                    "drinks",
                    "London",
                    "Another pub"
                ),
                new Todo
                (
                    "Future Todo 4",
                    DateTime.Now.AddMonths(4),
                    "Todo 4 months in future",
                    "drinks",
                    "London",
                    "Yet another pub"
                ),
                new Todo
                (
                    "Future Todo 5",
                    DateTime.Now.AddMonths(5),
                    "Todo 5 months in future",
                    "drinks",
                    "London",
                    "Just another pub"
                ),
                new Todo
                (
                    "Future Todo 6",
                    DateTime.Now.AddMonths(6),
                    "Todo 6 months in future",
                    "music",
                    "London",
                    "Roundhouse Camden"
                ),
                new Todo
                (
                    "Future Todo 7",
                    DateTime.Now.AddMonths(7),
                    "Todo 2 months ago",
                    "travel",
                    "London",
                    "Somewhere on the Thames"
                ),
                new Todo
                (
                    "Future Todo 8",
                    DateTime.Now.AddMonths(8),
                    "Todo 8 months in future",
                    "film",
                    "London",
                    "Cinema"
                )
            };

            return todos;
        }

        public static Todo GenerateSingleTodo()
        {
            return new Todo
            (
                "Past Todo 1",
                DateTime.Now.AddMonths(-2),
                "Todo 2 months ago",
                "drinks",
                "London",
                "Pub",
                Guid.NewGuid()
            );
        }
    }
}
