﻿using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        
    }
}
