namespace ProductApp.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; } // ← required ekledik
        // Diğer ürün özelliklerini buraya ekleyebilirsin
    }
}