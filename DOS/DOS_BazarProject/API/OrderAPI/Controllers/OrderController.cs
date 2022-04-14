using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Data;
using OrderAPI.DTO;
using OrderAPI.Model;

namespace OrderAPI.Controllers
{
    
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _repo;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _hostName;

        public OrderController( IOrderRepo repo,IMapper mapper,IHttpClientFactory clientFactory)
        {
            _repo = repo;
            _mapper = mapper;
            _clientFactory = clientFactory;
            _hostName = Dns.GetHostName();
        }


        [HttpGet("getAllOrder")]
        public ActionResult<IEnumerable<OrderReadDto>> GetAllOrders()
        {
            var orders = _repo.GetAllOrders();
            if (orders == null)
            {
                Console.WriteLine("The order is null");
                return NotFound();
            }
            
            Console.WriteLine("The orders have been sent");
            var mappedOrders = _mapper.Map<IEnumerable<OrderReadDto>>(orders);
            return Ok(mappedOrders);
        }




        public HttpStatusCode SendCheckRequest(Guid id) //send check request to catalog server to check the stock 
        {
            var client = _clientFactory.CreateClient();//create a mock client to send the "check request"
            var request = new HttpRequestMessage(HttpMethod.Get,"http://catalog_server/api/books/checkStock/"+id );
            Console.WriteLine("Send Query request to CatalogServer");
            var response = client.Send(request);
            
            return response.StatusCode;
        }
        
        
        
        public void SendDecreaseRequest(Guid id)// send request to catalog server to decrese the stock 
        {
            var client = _clientFactory.CreateClient();//create a mock client to send the "check request"
            var request = new HttpRequestMessage(HttpMethod.Post,"http://catalog_server/api/books/decreaseAndSync/"+id );
            Console.WriteLine("Send Update request to CatalogServer");
            client.Send(request);

            
        }
        
        
        //this method receive the new order request what it responsibility : first to check if there is a stock of the required book 
        // if yes update the stock decrease the book stock (all that done in a server to server request)
        // if no send back a Problem message to the client
        [HttpPost("addOrder")]
        public  ActionResult<OrderReadDto> Purchase(OrderCreateDto order)
        {
            var mappedCreateOrder = _mapper.Map<Order>(order);
            Guid itemId = mappedCreateOrder.ItemId;
            
            var response=  SendCheckRequest(itemId);
            
                //check the response StatusCode to determine the response of the client request
           if (response == HttpStatusCode.OK)//everything is ok and the order have been stored
           {
               
               Console.WriteLine("StockCount > 0  ");
               SendDecreaseRequest(itemId);
               Console.WriteLine("Purchase done successfully");
               _repo.Purchase(mappedCreateOrder);
               _repo.SaveChanges();
               var mappedRead = _mapper.Map<OrderReadDto>(mappedCreateOrder);
               return Ok(mappedRead);
           }
           else if (response == HttpStatusCode.NotFound)// the required book is not exist in our database
           {
               Console.WriteLine("There is no book with this Id :"+order.ItemId+" request failed");
               return NotFound();
           }
           else if (response == HttpStatusCode.NoContent)// the book is out of stock
           {
               Console.WriteLine("Book out of stock");
               return Problem("Book out of stock");
           }

           return Problem("There is Something wrong in adding order!! ");

        }

       
        
        
        




    }
}
