using Newproject.Services;

public class DeleteProductCommandHandler
{
    private readonly IProductService _productService;
    public DeleteProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task Handle(int id)
    {
        await _productService.DeleteAsync(id);
    }
}