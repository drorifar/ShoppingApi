using AutoMapper;
using Shopping.Entities;

namespace Shopping.Models.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryWithotProductDTO>();
            CreateMap<Category, CategoryDTO>();
        }
    }
}
