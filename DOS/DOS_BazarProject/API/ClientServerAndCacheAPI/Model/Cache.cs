using System.Collections.Generic;

namespace ClientServerAndCacheAPI.Model
{
    public class Cache
    {
        public Book Book { get; set; }

        public IEnumerable<Book> Books { get; set; }
        
        public IEnumerable<Order> Orders { get; set; }
        
    }
    
}