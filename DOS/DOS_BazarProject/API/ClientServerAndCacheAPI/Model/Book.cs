using System;
using System.ComponentModel.DataAnnotations;

namespace ClientServerAndCacheAPI.Model
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        [Required] 
        public string BookName { get; set; }
        [Required]
        public string BookTopic { get; set; }
        [Required]
        public double BookCost { get; set; }
        [Required]
        public int CountInStock { get; set; }
    }
}