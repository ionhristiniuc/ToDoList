using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Core.Dto
{
    public class UserRegistrationRequest
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }
    }
}
