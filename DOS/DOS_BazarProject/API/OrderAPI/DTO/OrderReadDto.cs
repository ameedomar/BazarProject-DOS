using System;

namespace OrderAPI.DTO
{
    public class OrderReadDto
    {
      
        public Guid Id { get; set; }
      
        public Guid ItemId { get; set; }
        
        public string Date { get; set; }
    }
}