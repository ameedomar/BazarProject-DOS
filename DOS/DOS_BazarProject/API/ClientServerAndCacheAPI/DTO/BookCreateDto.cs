using System;
using System.ComponentModel.DataAnnotations;

namespace ClientServerAndCacheAPI.DTO
{
    public class BookCreateDto// this DTO used to show the field that the client should put in the input request
    {
        
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
