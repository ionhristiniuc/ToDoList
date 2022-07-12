namespace ToDoList.API.Infrastructure.Data.Entities
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
    }
}
