using ProductApp.Application.DTOs;
using ProductApp.Application.Interfaces;

namespace ProductApp.Application.Queries
{

public class GetAllProductsQueryHandler
{
    private readonly IProductService _productService;
    public GetAllProductsQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IEnumerable<ProductDTO>> Handle()
    {
        return await _productService.GetAllAsync();
    }
}}