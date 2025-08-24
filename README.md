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
6. Swagger arayüzü için: `http://localhost:5000/swagger`

## Kullanılan Teknolojiler
- ASP.NET Core Web API
- Entity Framework Core
- Redis
- Serilog
- Swagger
- JWT Authentication

## API Endpointleri
- **POST** `/api/auth/register`
- **POST** `/api/auth/login`
- **GET** `/api/product`
- **POST** `/api/product`
- **PUT** `/api/product`
- **DELETE** `/api/product/{id}`

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
