using ProductApp.Application.DTOs;
using ProductApp.Application.Interfaces;
using ProductApp.Infrastructure.Persistence;
using ProductApp.Infrastructure.Messaging;
using ProductApp.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;

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