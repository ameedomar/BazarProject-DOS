using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using CatalogServer.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ClientServerAndCacheAPI.DTO;

namespace ClientServerAndCacheAPI.Controller
{
    
    [Controller]
    [Route("api/add")]
    public class ClientController : ControllerBase
    {
        private static bool _catalogFlag ;
        private static bool _orderFlag ;
        private readonly IHttpClientFactory _clientFactory;
        

        public ClientController(IHttpClientFactory clientFactory )
        {
            _clientFactory = clientFactory;
          
        }

        private string GetCatalogHost()
        {
            var host = _catalogFlag ? "catalog" : "catalog_replica";
            _catalogFlag = !_catalogFlag;
            return host;
        }
        private string GetOrderHost()
        {
            var host = _orderFlag ? "order" : "order_replica";
            _orderFlag = !_orderFlag;
            return host;
        }

        [HttpPost("book")] //addBook
        public ActionResult<BookCreateDto> AddBook([FromBody]BookCreateDto bookCreateDto)
        {
            var client = _clientFactory.CreateClient();
            var host = GetCatalogHost();
            var request ="http://"+host+"/api/books/addBookToCacheAndSync";
            var response=client.PostAsJsonAsync(request,bookCreateDto);
            Console.WriteLine("send addBook request to "+host+" server");
            if ( response.Result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(bookCreateDto);
            }
            else
            {
                return Problem("Some goes wrong in adding book");
            }
            
        }
        
        [HttpPost("order")] //addOrder
        public ActionResult<OrderCreateDto> AddOrder([FromBody]OrderCreateDto orderCreateDto )
        {
            var client = _clientFactory.CreateClient();
            var host = GetOrderHost();
            var request ="http://"+host+"/api/order/addOrderToCacheAndSync";
            var response=client.PostAsJsonAsync(request,orderCreateDto);
            Console.WriteLine("send addOrder request to "+host+" server");
            if ( response.Result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(orderCreateDto);
            }
            else
            {
                return Problem("Some goes wrong in adding order");
            }
            
        }


        [HttpPatch("{id}")]
        public ActionResult UpdateBook(Guid id, [FromBody] JsonPatchDocument<BookUpdateDto> pathDoc)
        {
            
            var client = _clientFactory.CreateClient();
            var host = GetCatalogHost();
            var request ="http://"+host+"/api/books/updateCacheAndSync/"+id;
            var response = client.PatchAsync(request,new StringContent(JsonConvert.SerializeObject(pathDoc), Encoding.UTF8,
                "application/json-patch+json"));
            Console.WriteLine("send updateBook request to "+host+" server");
            
            if ( response.Result.StatusCode == HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            else
            {
                return Problem("Some goes wrong in updating book");
            }
            
            
        }










    }
}