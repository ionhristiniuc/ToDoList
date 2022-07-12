namespace ToDoList.API.Core.Dto
{
    public class UpdateToDoRequest
    {
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
