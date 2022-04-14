using System;
using System.ComponentModel.DataAnnotations;

namespace OrderAPI.DTO
{
    public class OrderCreateDto
    {
        
        [Required] 
        public Guid ItemId { get; set; }
        [Required] 
        public string Date { get; set; }
        
    }
}
