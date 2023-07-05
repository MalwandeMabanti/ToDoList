using Microsoft.AspNetCore.Identity;

namespace ToDoList.Data
{
    public class ToDoUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
