using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newproject.DTOs;
using ProductApp.Application.Queries;
using ProductApp.Application.Commands;

namespace Newproject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly GetAllProductsQueryHandler _getAllHandler;
        private readonly AddProductCommandHandler _addHandler;
        private readonly GetProductByIdQueryHandler _getByIdHandler;
        private readonly UpdateProductCommandHandler _updateHandler;
        private readonly DeleteProductCommandHandler _deleteHandler;

        public ProductController(
            GetAllProductsQueryHandler getAllHandler,
            AddProductCommandHandler addHandler,
            GetProductByIdQueryHandler getByIdHandler,
            UpdateProductCommandHandler updateHandler,
            DeleteProductCommandHandler deleteHandler)
        {
            _getAllHandler = getAllHandler;
            _addHandler = addHandler;
            _getByIdHandler = getByIdHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            var products = await _getAllHandler.Handle();
            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _getByIdHandler.Handle(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult> Add(ProductDTO productDto)
        {
            await _addHandler.Handle(productDto);
            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: api/Product
        [HttpPut]
        public async Task<ActionResult> Update(ProductDTO productDto)
        {
            await _updateHandler.Handle(productDto);
            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _deleteHandler.Handle(id);
            return NoContent();
        }

        // Diğer CRUD işlemleri için benzer handler’lar ekleyebilirsin.
    }
}