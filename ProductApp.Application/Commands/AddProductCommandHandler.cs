using Newproject.DTOs;
using Newproject.Services;

namespace ProductApp.Application.Queries
{
public class AddProductCommandHandler
{
    private readonly IProductService _productService;
    public AddProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task Handle(ProductDTO productDto)
    {
        await _productService.AddAsync(productDto);
    }
}}