using AutoMapper;
using System.Threading.Tasks;
using WebApi_JWT_Security.DTOs.Product;
using WebApi_JWT_Security.Persistence;

namespace WebApi_JWT_Security.Services
{
    public class ProductService : IProductService
    {
        private readonly WebApiDBContext _context;
        private readonly IMapper _mapper;

        public ProductService(WebApiDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetById(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            return _mapper.Map<ProductDto>(entity);
        }
    }
}
