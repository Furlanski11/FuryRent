﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FuryRent.Infrastructure.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Category identifier")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
