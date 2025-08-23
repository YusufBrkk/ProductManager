namespace Newproject.DTOs
{
    // Ürün verisinin API üzerinden taşınması için kullanılan DTO sınıfı
    public class ProductDTO
    {
        public int Id { get; set; }           // Ürün ID'si
        public string Name { get; set; }      // Ürün adı
        public decimal Price { get; set; }    // Ürün fiyatı
    }
}