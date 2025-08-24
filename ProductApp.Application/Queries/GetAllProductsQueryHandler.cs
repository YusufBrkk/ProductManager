using Newproject.DTOs;
using Newproject.Services;

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