using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(20, ErrorMessage = "Title can't be longer 20 characters")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "Title can't be longer 50 characters")]
        public string Description { get; set; }
        public bool Completed { get; set; }

    }
}
