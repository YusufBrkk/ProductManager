using ProductApp.Application.DTOs;
using ProductApp.Application.Interfaces;
using ProductApp.Infrastructure.Persistence;
using ProductApp.Infrastructure.Messaging;
using ProductApp.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;

namespace ProductApp.Application.Commands
{
    public class DeleteProductCommandHandler
    {
        private readonly ProductApp.Application.Interfaces.IProductService _productService;
        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Handle(int id)
        {
            await _productService.DeleteAsync(id);
        }
    }
}