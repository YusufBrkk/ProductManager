using ProductService.Core.Entities;
using ProductService.Core.Interfaces;

namespace ProductService.Application.Queries
{
    public class GetAllProductsQueryHandler
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle()
        {
            return await _productRepository.GetAllAsync();
        }
    }
}