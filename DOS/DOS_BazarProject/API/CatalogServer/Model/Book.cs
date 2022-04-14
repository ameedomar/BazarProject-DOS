using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogServer.Model
{
    public class Book //this class represent the field that we will store it in the database and it's represent the BookInfo
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
