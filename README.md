<<<<<<< HEAD
# ProductManager
=======
# Newproject

## Proje Hakkında
Katmanlı mimari ile geliştirilmiş, Entity Framework Core ve MSSQL kullanan bir ASP.NET Core Web API örneği.

## Kurulum
1. Bu repoyu klonlayın veya indirin.
2. Gerekli NuGet paketlerini yükleyin:
   ```
   dotnet restore
   ```
3. Veritabanı bağlantı bilgisini `appsettings.json` dosyasında güncelleyin.
4. Migration ve veritabanı oluşturmak için:
   ```
   dotnet ef database update
   ```
5. Uygulamayı başlatın:
   ```
   dotnet run
   ```

## API Endpointleri
Swagger arayüzü ile tüm endpointleri test edebilirsiniz: `http://localhost:5100/swagger`

### Ürün İşlemleri
- **GET** `/api/Product` : Tüm ürünleri listeler
- **GET** `/api/Product/{id}` : Id ile ürün getirir
- **POST** `/api/Product` : Yeni ürün ekler
  - Body örneği:
    ```json
    {
      "name": "Ürün Adı",
      "price": 100
    }
    ```
- **DELETE** `/api/Product/{id}` : Id ile ürünü siler

### Örnek WeatherForecast Endpointi
- **GET** `/weatherforecast`

## Exception Handling
Global hata yönetimi ile beklenmeyen hatalarda anlamlı bir JSON mesajı döner.

## Katmanlar
- Models
- DTOs
- Repositories
- Services
- Controllers

## Notlar
- Tüm işlemler async/await ve EF Core ile yapılır.
- Swagger arayüzü development ortamında otomatik olarak açıktır.

---

Herhangi bir sorunda bana ulaşabilirsiniz.
>>>>>>> d8f6287 (ProductManager projesi ilk sürüm)
