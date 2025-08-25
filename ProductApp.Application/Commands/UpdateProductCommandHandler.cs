using ProductApp.Application.DTOs;
using ProductApp.Application.Interfaces;
using ProductApp.Infrastructure.Persistence;
using ProductApp.Infrastructure.Messaging;
using ProductApp.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;

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