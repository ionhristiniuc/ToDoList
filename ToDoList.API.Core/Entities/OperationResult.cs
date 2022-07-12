namespace ToDoList.API.Core.Entities
{
    public class OperationResult
    {
        public OperationResult() { }

        public OperationResult(Error error)
        {
            Error = error;
        }
        
        public OperationResult(ErrorType errorType)
        {
            Error = new Error(errorType, string.Empty);
        }

        public bool Success => Error == null;

        public Error Error { get; }
    }

    public class OperationResult<T> : OperationResult
    {
        public OperationResult(T value)
        {
            Value = value;
        }

        public OperationResult(Error error) 
            : base(error) { }
        
        public OperationResult(ErrorType errorType) 
            : base(errorType) { }

        public T Value { get; }
    }
    
    public class Error
    {
        public ErrorType Type { get; set; }
        public string Description { get; set; }

        public Error(ErrorType type, string description)
        {
            Type = type;
            Description = description;
        }
    }

    public enum ErrorType
    {
        Unknown,
        ResourceNotFound,
        WrongPassword
    }
}
