using System;
using System.Collections.Generic;
using System.Linq;
using CatalogServer.Model;
using Microsoft.EntityFrameworkCore;

namespace CatalogServer.Data
{
    public class SqlCatalogRepo : ICatalogRepo // this class implement the interface that contain the functionality that we have 
    {
        private readonly CatalogContext _context;

        public SqlCatalogRepo( CatalogContext context)
        {
            _context = context;
        }


       

        public IEnumerable<Book> SearchByTopic(string topic)
        {
            var dataFromDb = _context.Catalogs.Where(row => row.BookTopic.ToLower().
                Contains(topic.ToLower())).ToList();
            return dataFromDb;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Catalogs.ToList();
        }

        public Book GetInfoById(Guid id)
        {
            return _context.Catalogs.FirstOrDefault(p => p.Id == id);
        }

        public bool AddBook(Book book)
        {
            if (book == null)
            {
                Console.WriteLine("The book is null");
                throw new ArgumentNullException();
            }

            var bookFromDb = _context.Catalogs.FirstOrDefault(b => b.BookName.ToLower().Equals(book.BookName.ToLower()));

            if (bookFromDb != null)
            {
                Console.WriteLine("The book already added");
                return false;
            }
            
            Console.WriteLine("The book has been added successfully");
            _context.Catalogs.Add(book);
            return true;
        }


        public bool SaveChanges()
        {
            Console.WriteLine("Data has been saved successfully");
            return (_context.SaveChanges() >= 0);
        }

        public void Update(Book book)
        {
            Console.WriteLine("Data has been update successfully");
        }

        public int CheckStock(Guid id)
        {
        
            var bookToCheckValue =GetInfoById(id);

            if(bookToCheckValue == null)// there is no book in this id 
            {
                
                return 0;
            }
            else if (bookToCheckValue.CountInStock > 0)// there is a book with this id
            {
               
                return 1;
            }
            else//// book is out of stock
            {
               
                return 2;
            }
                
        }

        public void DecreaseBookCount(Guid id)
        {
            
            _context.Database.ExecuteSqlInterpolated(
                $"UPDATE Catalogs SET CountInStock= CountInStock - 1 WHERE Id={id} ");
            Console.WriteLine("update has been done successfully");
            
        }
    }
}
