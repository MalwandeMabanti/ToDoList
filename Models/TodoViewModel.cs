namespace ToDoList.Models
{
    public class TodoViewModel
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public IFormFile? Image { get; set; }
    }
}
