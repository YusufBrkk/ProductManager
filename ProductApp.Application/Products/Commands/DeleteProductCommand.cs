using MediatR;

namespace ProductApp.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteProductCommand(int id) => Id = id;
    }
}