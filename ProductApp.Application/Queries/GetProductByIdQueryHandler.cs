using ProductApp.Application.DTOs;
using ProductApp.Application.Interfaces;

public class GetProductByIdQueryHandler
{
    private readonly IProductService _productService;
    public GetProductByIdQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductDTO?> Handle(int id)
    {
        return await _productService.GetByIdAsync(id);
    }
}