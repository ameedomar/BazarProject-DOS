using OrderAPI.DTO;
using OrderAPI.Model;

namespace OrderAPI.Profile
{
    public class OrderProfile : AutoMapper.Profile
    {

        public OrderProfile()
        {

            CreateMap<OrderCreateDto,Order>();
            CreateMap<Order,OrderReadDto>();

        }
        
    }
}