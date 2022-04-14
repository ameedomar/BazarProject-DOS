using System;

namespace ClientServerAndCacheAPI.DTO
{
    
    /*this Dto usage is to specify the response that will be send to the client 
     * so whatever we add to the book class the response will stay the same
     * ( Kind of interface contract )
     */
    public class BookReadDto
    {
        public Guid Id { get; set; }
        
        public string BookName { get; set; }
        
        public string BookTopic { get; set; }
        
        public double BookCost { get; set; }
       
        public int CountInStock { get; set; }
    }
}