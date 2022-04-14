using System;
using System.Collections.Generic;
using System.Net.Http;
using ClientServerAndCacheAPI.DTO;
using ClientServerAndCacheAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientServerAndCacheAPI.Controller
{
    [ApiController]
    [Route("api/cache")]
    public class CacheController : ControllerBase
    {
        private readonly Dictionary<string, Cache> _dictionary;
        private readonly IHttpClientFactory _clientFactory;
        private static bool _catalogFlag ;
        private static bool _orderFlag ;

        public CacheController(Dictionary<string,Cache> dictionary,IHttpClientFactory clientFactory)
        {
            _dictionary = dictionary;
            _clientFactory = clientFactory;
        }

        
        private string GetCatalogHost()
        {
            var host = _catalogFlag ? "catalog" : "catalog_replica";
            _catalogFlag = ! _catalogFlag;
            return host;
        }
        private string GetOrderHost()
        {
            var host = _orderFlag ? "order" : "order_replica";
            _orderFlag = ! _orderFlag;
            return host;
        }
        
        
        [HttpGet("getBookInfo/{key}")]
        public ActionResult<BookReadDto> GetBookInfo(Guid key)
        {
            if (_dictionary.ContainsKey(key.ToString()))
            {
                Console.WriteLine("Cache hit book with id :"+key);
                return Ok(_dictionary[key.ToString()].Book); 
            }
            else
            {
                Console.WriteLine("Cache miss book with id :"+key);
                var client = _clientFactory.CreateClient();//create a mock client to send the "check request"
                var host = GetCatalogHost();
                var request = new HttpRequestMessage(HttpMethod.Get,"http://"+host+"/api/books/getInfoById/"+key );
                Console.WriteLine("Send getBookInfo request to "+host);
                var response = client.Send(request);
                var book = JsonConvert.DeserializeObject<Book>(response.Content.ReadAsStringAsync().Result);
              
                _dictionary[key.ToString()] = new Cache
                {
                    Book = book,
                    Books = null,
                    Orders = null
                };
                return Ok(book);
            }
            
            
        }
        
        
        [HttpGet("GetAllBooks/{key}")]
        public ActionResult<IEnumerable<BookReadDto>> GetAllBooks(string key)
        {
            if (_dictionary.ContainsKey(key))
            {
                Console.WriteLine("Cache hit books exist");
                return Ok(_dictionary[key].Books); 
            }
            else
            {
                Console.WriteLine("Cache miss books not exist");
                var client = _clientFactory.CreateClient();//create a mock client to send the "check request"
                var host = GetCatalogHost();
                var request = new HttpRequestMessage(HttpMethod.Get,"http://"+host+"/api/books/getAllBooks" );
                Console.WriteLine("Send getAllBooks request to "+host);
                var response = client.Send(request);
                var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(response.Content.ReadAsStringAsync().Result);
                _dictionary[key] = new Cache
                {
                    Book = null,
                    Books = books,
                    Orders = null
                };
                return Ok(books);
            }
            
            
        }



        [HttpGet("getBooksByTopic/{key}")]
        public ActionResult<IEnumerable<BookReadDto>> GetBooksByTopic(string key)
        {
            if (_dictionary.ContainsKey(key))
            {
                Console.WriteLine("Cache hit topic :"+key);
                return Ok(_dictionary[key].Books); 
            }
            else
            {
                Console.WriteLine("Cache miss topic :"+key);
                var client = _clientFactory.CreateClient();//create a mock client to send the "check request"
                var host = GetCatalogHost();
                var request = new HttpRequestMessage(HttpMethod.Get,"http://"+host+"/api/books/searchByTopic/"+key );
                Console.WriteLine("Send searchByTopic request to "+host);
                var response = client.Send(request);
                var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(response.Content.ReadAsStringAsync().Result);
              
                _dictionary[key] = new Cache
                {
                    Book = null,
                    Books = books,
                    Orders = null
                };
                return Ok(books);
            }
            
            
        }
        
        
        [HttpGet("getAllOrders/{key}")]
        public ActionResult<IEnumerable<OrderReadDto>> GetAllOrders(string key)
        {
            if (_dictionary.ContainsKey(key))
            {
                Console.WriteLine("Cache hit orders  ");
                return Ok(_dictionary[key].Orders); 
            }
            else
            {
                Console.WriteLine("Cache miss "+key);
                var client = _clientFactory.CreateClient();//create a mock client to send the "check request"
                var host = GetOrderHost();
                var request = new HttpRequestMessage(HttpMethod.Get,"http://"+host+"/api/order/getAllOrder" );
                Console.WriteLine("Send getAllOrders request to "+host);
                var response = client.Send(request);
                var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(response.Content.ReadAsStringAsync().Result);
              
                _dictionary[key] = new Cache
                {
                    Book = null,
                    Books = null,
                    Orders = orders
                };
                return Ok(orders);
            }
            
            
        }
        

        [HttpDelete("{key}")]
        public ActionResult DeleteCache(string key)
        {
            if (_dictionary.ContainsKey(key))
            {
                _dictionary.Remove(key);
                Console.WriteLine("Data with Key : "+key +" has been deleted successfully");
            }

            return NotFound();

        }
        
        
    }
}