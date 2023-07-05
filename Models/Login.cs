using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Login
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
