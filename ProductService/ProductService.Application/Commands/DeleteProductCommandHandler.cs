using ProductService.Core.Interfaces;

namespace ProductService.Application.Commands
{
    public class DeleteProductCommandHandler
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}