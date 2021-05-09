using AutoMapper;
using WebApi_JWT_Security.DTOs.Product;
using WebApi_JWT_Security.Entities;

namespace WebApi_JWT_Security.Configurations.AutoMapperConfigurations
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
