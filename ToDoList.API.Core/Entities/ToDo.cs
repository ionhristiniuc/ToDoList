namespace ToDoList.API.Core.Entities
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
    }
}