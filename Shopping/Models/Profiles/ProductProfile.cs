using AutoMapper;
using Shopping.Entities;

namespace Shopping.Models.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductForCreationDTO, Product>();
            CreateMap<ProductForUpdateDTO, Product>();
            CreateMap<Product, ProductForUpdateDTO>();
        }
    }
}
