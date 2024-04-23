﻿using System.ComponentModel.DataAnnotations;
namespace TodoApi.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }

    }

}
