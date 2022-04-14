
using CatalogServer.DTO;
using ClientServerAndCacheAPI.DTO;
using ClientServerAndCacheAPI.Model;

namespace ClientServerAndCacheAPI.Profile
{
    public class CatalogProfile : AutoMapper.Profile// in this class we create the mapping that we want to apply 
    {

        public CatalogProfile()
        {
            CreateMap<Book,BookReadDto>();//used to map between Book class (source) and the BookReadDto class(Destination)
            CreateMap<BookReadDto,Book>();
            CreateMap<Book, BookUpdateDto>();
            CreateMap<BookUpdateDto,Book >();
            CreateMap<BookCreateDto, Book>();
        }
        
    }
}
