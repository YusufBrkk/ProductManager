namespace ProductService.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        // Gerekirse başka alanlar ekleyebilirsin
    }
}