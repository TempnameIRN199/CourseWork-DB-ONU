﻿namespace CourseWork_DB_ONU.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
