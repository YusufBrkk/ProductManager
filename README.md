# Newproject

## Proje Hakkında
Bu proje, .NET tabanlı mikroservis mimarisi ile geliştirilmiş bir backend uygulamasıdır.  
Aşağıdaki servisleri içerir:
- **AuthService:** Kimlik doğrulama ve yetkilendirme (JWT, Refresh Token, Microsoft Identity)
- **ProductService:** Onion Architecture, CQRS, Redis cache, Event-Driven yapı, ürün işlemleri
- **LogService:** Merkezi log toplama (Serilog/Seq/ELK)
- **ApiGateway:** YARP ile merkezi kimlik doğrulama ve rate limiting


## Kurulum
1. Gerekli NuGet bağımlılıklarını yükleyin:
    ```sh
    dotnet restore
    ```
2. Projeyi build edin:
    ```sh
    dotnet build Newproject.sln
    ```
3. Her mikroservisi kendi klasöründen başlatın:
    ```sh
    dotnet run --project ./AuthService/AuthService.API.csproj
    dotnet run --project ./ProductApp.API/ProductApp.API.csproj
    dotnet run --project ./LogService.API/LogService.API.csproj
    dotnet run --project ./ApiGateway/ApiGateway.csproj
    ```

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

Herhangi bir sorunda bana ulaşabilirsiniz. yusufbrkk42@gmail.com
