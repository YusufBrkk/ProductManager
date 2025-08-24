using Newproject.DTOs;
using Newproject.Services;

namespace ProductApp.Application.Commands
{
public class UpdateProductCommandHandler
{
    private readonly IProductService _productService;
    public UpdateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task Handle(ProductDTO productDto)
    {
        await _productService.UpdateAsync(productDto);
    }
}}