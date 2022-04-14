using System;
using System.ComponentModel.DataAnnotations;

namespace ClientServerAndCacheAPI.DTO
{
    public class OrderCreateDto
    {
        
        [Required] 
        public Guid ItemId { get; set; }
        [Required] 
        public string Date { get; set; }
        
    }
}
