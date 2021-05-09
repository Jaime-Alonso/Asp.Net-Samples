using System.Threading.Tasks;
using WebApi_JWT_Security.DTOs.Product;

namespace WebApi_JWT_Security.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetById(int id);
    }
}