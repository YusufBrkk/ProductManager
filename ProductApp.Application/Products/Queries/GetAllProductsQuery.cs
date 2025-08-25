using MediatR;
using ProductApp.Domain.Entities;
using System.Collections.Generic;

namespace ProductApp.Application.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<Product>> { }
}