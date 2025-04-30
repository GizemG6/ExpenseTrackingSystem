# ExpenseTrackingSystem 

Bu proje, şirket özelinde sahada çalışan personel için masraf kalemlerinin takibi ve yönetimi için oluşturulmuştur. Kullanıcıların masraflarını kategori bazlı takip edebildiği, rollerin ayrıldığı ve güvenli bir API altyapısı sunan bir masraf takip sistemidir. Onion Architecture kullanılarak katmanlı ve sürdürülebilir bir yapı kurulmuştur.

🔎 Proje Detayları

Sahada çalışan personel masraflarını anında sisteme girebilecek ve işveren bunu aynı zamanda hem takip edip edebilecek hem de vakit kaybetmeden harcamayı onaylayıp personele ödemesini yapabilecektir. Çalışan hem evrak fiş vb toplamaktan kurtulmuş olacak hem de uzun süre sahada olduğu durumda gecikmeden ödemesini alabilecektir. Uygulama şirket üzerinde yönetici ve saha personeli olmak üzere 2 farklı rolde(Admin, Employee) hizmet verecektir. Çalışan saha personeli sadece sisteme masraf girişi yapacak ve geri ödeme talep edecektir. Personel mevcut taleplerini görecek ve taleplerinin durumunu takip edebilecektir. Onayda bekleyen taleplerini görebilir ve bunları takip edebilir. Sistem yöneticisi konumunda olan şirket kullanıcıları ise mevcut talepleri görecek ve onları onaylayıp red edebilecektir. Onayladıkları ödemeler için anında ödeme işlemi banka entegrasyonu ile gerçekleştirilecek olup çalışan hesabına EFT ile ilgili tutar yatırılacaktır. Red olan talepler için bir açıklama alanı girişi mevcut ve talep sahibi masraf talebinin neden red olduğunu görebilir.

## 🛠️ Kullanılan Teknolojiler

| Teknoloji            | Açıklama                            |
|----------------------|-------------------------------------|
| .NET 8               | Framework                           |
| EntityFramework Core | Code First veri erişim              |
| MediatR              | CQRS ve handler yapısı              |
| FluentValidation     | Model doğrulama işlemleri           |
| Identity             | Kullanıcı kimlik yönetimi           |
| JWT	                 | JSON Web Token ile kimlik doğrulama |
| Redis                | Önbellekleme ve token yönetimi      |
| RabbitMQ             | Mesaj kuyruğu ve asenkron işlemler  |
| Serilog              | Loglama                             |
| Hangfire             | Arka plan görevleri                 |
| Dapper               | Performans odaklı mikro ORM         |
| SQL Server           | Veritabanı                          |
| xUnit	               | Unit test framework                 |
| FluentAssertions	   | Daha okunabilir test doğrulama      |
| Moq	                 | Mocklama                            |
| Swagger	             | API dökümantasyonu ve test aracı    |

## 🛠️ Kullanılan Mimari ve Design Pattern

| Mimari	             | Design Pattern                       |
|----------------------|--------------------------------------|
| Onion Architecture 	 | Service ve Repository Design Pattern |

📌Onion Architecture daha detaylı bilgi için:

https://medium.com/@0.gizemgunes/onion-architecture-nedir-ve-yazılımda-nasıl-kullanılır-c77a4a8cf18f

## ⚙️ Gereksinimler (Prerequisites):

✅ .NET 8 SDK

✅ Docker Desktop (RabbitMQ, SQL Server gibi servislerin konteyner ile çalıştırılması için)

✅ SQL Server 2022 Developer Edition veya Docker üzerinden SQL Server konteyneri

✅ Visual Studio 2022+ veya Rider

## 🛠️ Kurulum

Projeyi çalıştırmak için aşağıdaki adımları izleyin:

1️⃣ Projeyi klonlayın:

```bash
git clone https://github.com/GizemG6/ExpenseTrackingSystem.git
cd ExpenseTrackingSystem
```

2️⃣ appsettings.json dosyasını yapılandırın:

Presentation/ExpenseTrackingSystem.API/appsettings.json içindeki bağlantı ve servis bilgilerini kendinize göre güncelleyin.

![image](https://github.com/user-attachments/assets/52c83ac9-8da6-475d-88d1-34c44ac597fb)

🔗 ConnectionStrings

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=Gizem\\SQLEXPRESS;Initial Catalog=ExpenseTrackingSystemDb;Trusted_Connection=True;TrustServerCertificate=true;"
}
```

➜ Server alanını kendi SQL Server instance'ınıza göre değiştirin (örneğin: DESKTOP-XXXX\\SQLEXPRESS).

➜ Diğer alanlar aynı kalabilir.

🔐 Token

➜ JWT için kullanılan bu değerler ilk kurulumda kullanılabilir. Güvenlik ihtiyacınıza göre değiştirebilirsiniz.

📧 MailSettings

```json
"MailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SenderEmail": "0.gizemgunes@gmail.com",
  "SenderPassword": "jzie nwzu erte ifbg",
  "EnableSSL": true
}
```

➜ Proje için kendi mail adresim üzerinden uygulama şifresi oluşturdum. Deneme için kullanabilirsiniz.

➜ SenderEmail ve SenderPassword alanlarını kendi Gmail adresiniz ve uygulama şifreniz ile değiştirerek kullanabilirsiniz.

➜ Gmail üzerinden gönderim yapmak için iki adımlı doğrulama ve uygulama şifresi gereklidir.

🧠 Redis

```json
"Redis": {
  "ConnectionString": "localhost:1453"
}
```

➜ Redis için Docker kullanabilirsiniz. Örnek komut:

```bash
docker run -d -p 1453:6379 --name redis redis
```

➜ ConnectionString değerini container portuna göre ayarlayın.

🐰 RabbitMQ

```json
"RabbitMQ": {
  "Host": "localhost",
  "Port": 5672,
  "Username": "guest", 
  "Password": "guest"
}
```

➜ RabbitMQ için de Docker kullanılabilir. Örnek komut:

```bash
docker run -d --hostname my-rabbit --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

➜ Arayüze erişim: http://localhost:15672 (Kullanıcı adı/şifre: guest/guest)

➜ Host, Port, Username, Password değerlerini kurulumunuza göre güncelleyin.

3️⃣ Migration'ı çalıştırarak veritabanını oluşturun:

➜ .NET CLI Üzerinden

```bash
dotnet ef database update --startup-project Persistence/ExpenseTrackingSystem.API
```

Bu komut, veritabanı tablolarını oluşturur ve InitialMigration dosyasındaki seed verileriyle birlikte kullanıcı ve rol bilgilerini ekler.

➜ Visual Studio Üzerinden

![image](https://github.com/user-attachments/assets/b678d269-3071-4e02-bcab-79cfca8bb1d5)

-Package Manager Console'u açın.

-Default Project olarak Infrastructure.Persistence katmanını seçin.

![image](https://github.com/user-attachments/assets/2ea9c145-42e1-4584-a231-b64380951526)

-Startup projenin ExpenseTrackingSystem.API olduğundan emin olun.

-Aşağıdaki komutu çalıştırın:

```bash
update-database
```

Bu işlem de aynı şekilde veritabanını ve başlangıç verilerini oluşturur.

4️⃣ Uygulamayı başlatın:

```bash
dotnet run --project Presentation/ExpenseTrackingSystem.API
```

ya da Visual Studio veya Rider üzerinden çalıştırabilirsiniz.

5️⃣ Swagger üzerinden test edin:

Uygulama çalıştığında açılan Swagger sayfası üzerinden API endpointlerini test edebilirsiniz.

## 📁 Katman Yapısı

```mathematica
ExpenseTrackingSystem/
├── Core/
│   ├── ExpenseTrackingSystem.Application/
│   │   ├── Abstractions/ (Services, Token)
│   │   ├── Dtos/
│   │   ├── Features/ (Commands, Queries) Helpers/
│   │   ├── Helpers/
│   │   ├── Mapper/
│   │   ├── Repositories/
│   │   ├── Validators/
│   │   └── ServiceRegistration.cs
│   └── ExpenseTrackingSystem.Domain/ 
│       └── Entities/
│
├── Infrastructure/
│   ├── ExpenseTrackingSystem.Infrastructure/
│   │   ├── Services/
│   │   └── ServiceRegistration.cs
│   └── ExpenseTrackingSystem.Persistence/
│       ├── Context/
│       ├── Migrations/
│       ├── Repositories/
│       ├── Services/
│       └── ServiceRegistration.cs
│
├── Presentation/
│   └── ExpenseTrackingSystem.API/
│       ├── Controllers/
│       ├── Middlewares/
│       ├── appsettings.json
│       └── Program.cs
├── Test/ 
│   └── ExpenseTrackingSystem.Tests/ (xUnit Tests)
```

# 🧩 Domain Katmanı

Bu katman, çekirdek nesnelerini barındırır. Veri erişimi, API, UI gibi dış etkenlerden tamamen izole çalışır.

📚Kullanılan Paketler

Microsoft.AspNetCore.Identity.EntityFrameworkCore

## ✳️ Entities

💠 AppUser

Uygulama kullanıcısını temsil eder. Microsoft.AspNetCore.Identity.IdentityUser sınıfından türetilmiştir ve aşağıdaki gibi ek alanlara sahiptir:

| Property                      | Açıklama                                   |
|-------------------------------|--------------------------------------------|
| Id                            | Identity-String                            |
| FullName                      | Kullanıcı ismi                             |
| Title                         | Şirket içindeki görevi                     |
| IBAN                          | Kullanıcı IBAN'ı                           |
| IsActive                      | Kullanıcının aktiflik durumu               |
| CreatedDate                   | Kullanıcının eklendiği tarih               |
| UpdateDate	                  | Kullanıcının güncellendiği tarih           |
| RefreshToken                  | Token yenileme(ek güvenlik)                |
| RefreshTokenEndDate           | Refresh Token'ının geçerlilik süresinin    |
| ICollection<Expense> Expenses | Personel için birden fazla masraf ilişkisi |

💠 AppRole

Roller IdentityRole sınıfından türetilmiştir. Kullanıcılara atanabilecek Admin, Employee gibi roller burada tanımlanır.

💠 Expense

Kullanıcılara ait giderleri temsil eder.

| Property                             | Açıklama                                    |
|--------------------------------------|---------------------------------------------|
| Id                                   | Guid                                        |
| Amount                               | Masraf tutarı için                          |
| Date                                 | Masraf talebinin oluşturulduğu tarihi       |
| Location                             | Masraf talep yeri                           |
| RejectionReason                      | Masraf reddedildiyse sebebi                 |
| ReceiptFilePath	                     | Fatura vb. için dosya yolu                  |
| UserId, AppUser User                 | Masrafı oluşturan user                      |
| ExpenseCategory Category, CategoryId | Masraf kategori ilişkisi                    |
| ExpenseStatus Status                 | Masraf durumu (Pending, Approved, Rejected) |

```csharp
public enum ExpenseStatus
{
	Pending = 1,
	Approved,
	Rejected
}
```

💠 ExpenseCategory

Harcamaların kategorilerini tutar.

| Property                      | Açıklama                                         |
|-------------------------------|--------------------------------------------------|
| Id                            | int                                              |
| Name                          | Kategori ismi                                    |
| ICollection<Expense> Expenses | Bir kategoride birden fazla masraf olma ilişkisi |

💠 PaymentSimulation

Adminin ödeme simülasyonlarını içerir.

| Property          | Açıklama           |
|-------------------|--------------------|
| Id                | Guid               |
| PaymentDate       | Ödeme tarihi       |
| BankReferenceNo   | İşlem numarası     |
| PaidAmount        | Ödenen tutar       |
| SenderFullName    | Gönderenin ismi    |
| SenderIban	      | Gönderenin IBAN'ı  |
| ReceiverFullName  | Alıcının ismi      |
| ReceiverIban      | Alıcının IBAN'ı    |
| Expense Expense   | Masraf bilgisi     |

💠 AuditLog

Uygulama içinde yapılan işlemlerin loglanması için kullanılır.

| Property    | Açıklama                                 |
|-------------|------------------------------------------|
| Id          | Guid                                     |
| UserId      | İşlemi gerçekleştiren kullanıcının Id'si |
| Action      | Yapılan işlem türü                       |
| Entity      | İşlem yapılan entity                     |
| EntityId    | İşlem yapılan entity'nin Id'si           |
| ActionDate	| İşlemin gerçekleştirildiği tarih         |

# 🧩 Application Katmanı

Application katmanı, iş mantığı ve veri işleme işlemlerini içerir. Domain katmanındaki varlıkları (entity) kullanarak, dışa servisler sağlar ve bu sayede UI (kullanıcı arayüzü) veya API gibi diğer katmanlarla etkileşime girer.

📚Kullanılan Paketler

AutoMapper.Extensions.Microsoft.DependencyInjection

FluentValidation.AspNetCore

FluentValidation.DependencyInjectionExtensions

MediatR

## ✳️ Abstractions

Abstractions altındaki Services ve Token klasörleri içindeki arayüzler, uygulamanın dış dünyaya sağladığı servislerin soyutlamalarıdır.

### Services

Burada iş mantığını gerçekleştiren servis arayüzleri (interface) yer alır.

💠 IAuditLogService

```csharp
public interface IAuditLogService
{
	Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(string userId = null, DateTime? startDate = null, DateTime? endDate = null);
	Task LogActionAsync(string userId, string action, string entity, string entityId);
}
```

| Method            | Açıklama                                                                                                          |
|-------------------|-------------------------------------------------------------------------------------------------------------------|
| GetAuditLogsAsync | Audit Loglarını sorgulamak için                                                                                   |
| LogActionAsync    | Bir işlem gerçekleştiğinde (örneğin, bir kayıt ekleme) bir AuditLog kaydı oluşturur ve bunu veritabanına kaydeder |

💠 IAuthService

```csharp
public interface IAuthService
{
	Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
	Task<TokenDto> LoginAsync(string email, string password, int accessTokenLifeTime); 
	Task<TokenDto> RefreshTokenLoginAsync(string refreshToken);
	Task PasswordResetAsnyc(string email);
}
```

| Method                 | Açıklama                                                                             |
|------------------------|--------------------------------------------------------------------------------------|
| VerifyResetTokenAsync  | Şifre sıfırlama işlemi için sağlanan token'ı doğrulamak amacıyla kullanılır          |
| LoginAsync             | Kullanıcı maili ve şifre ile yapılan giriş işlemini yönetir                          |
| RefreshTokenLoginAsync | Mevcut bir refresh token ile yeni bir access token'ı almayı sağlar                   |
| LoginAsync             | Kullanıcıların şifrelerini sıfırlayabilmesi için bir şifre sıfırlama isteği başlatır |

💠 IExpenseCategoryService

```csharp
public interface IExpenseCategoryService
{
	Task<bool> CreateAsync(string name);
	Task<bool> UpdateAsync(int id, string name);
	Task<bool> DeleteAsync(int id);
	Task<ExpenseCategory?> GetByIdAsync(int id);
	Task<List<ExpenseCategory>> GetAllAsync();
}
```

| Method       | Açıklama                     |
|--------------|------------------------------|
| CreateAsync  | Kategori oluşturma           |
| UpdateAsync  | Kategori güncelleme          |
| DeleteAsync  | Kategori silme               |
| GetByIdAsync | Id'ye göre kategori getirme  |
| GetAllAsync  | Bütün kategorileri listeleme |

💠 IExpenseService

```csharp
public interface IExpenseService
{
	Task<List<Expense>> GetAllAsync();
	Task<Expense> GetByIdAsync(Guid id);
	Task<Expense> CreateAsync(ExpenseCreateDto expenseCreateDto);
	Task<Expense> UpdateStatusAsync(Expense expense);
	Task<bool> DeleteAsync(Guid id);
	Task<List<Expense>> GetByStatusAsync(ExpenseStatus status);
	Task<List<Expense>> GetByUserIdAsync(string userId);
	Task<List<Expense>> GetByFullNameAsync(string fullName);
	Task<List<Expense>> GetByCategoryAsync(string categoryName);
}
```

| Method              | Açıklama                                    |
|---------------------|---------------------------------------------|
| GetAllAsync         | Bütün masrafların listelenmesi              |
| GetByIdAsync        | Id'ye göre masraf bulma                     |
| CreateAsync         | Masraf oluşturma                            |
| UpdateStatusAsync   | Masraf durumunu güncelleme                  |
| DeleteAsync         | Masraf silme                                |
| GetByStatusAsync    | Masraf durumuna göre masrafları listeleme   |
| GetByUserIdAsync    | Kullanıcı Id'sine göre masrafları listeleme |
| GetByFullNameAsync  | Kullanıcı ismine göre masrafları listeleme  |
| GetByCategoryAsync  | Kategori ismine göre masrafları listeleme   |

💠 IMailService

```csharp
public interface IMailService
{
	Task SendMailAsync(MailRequest mailRequest);
	Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
	Task SendExpenseStatusUpdateMailAsync(string toEmail, string expenseStatus, string expenseId);
	Task SendExpenseCreatedMailAsync(string[] adminEmails, string userName, string categoryName, decimal amount, DateTime date, string expenseId);
}
```

| Method                           | Açıklama                                                             |
|----------------------------------|----------------------------------------------------------------------|
| SendMailAsync                    | Mail gönderme                                                        |
| SendPasswordResetMailAsync       | Şifre sıfırlama mail gönderimi                                       |
| SendExpenseStatusUpdateMailAsync | Admin masraf durumunu güncellediğinde masraf sahibine mail gönderimi |
| SendExpenseCreatedMailAsync      | Personel masraf oluşturduğunda Adminlere mail gönderimi              |

💠 IReportService

```csharp
public interface IReportService
{
	Task<IEnumerable<EmployeeRequestReportDto>> GetEmployeeRequestsAsync(string userId);
	Task<IEnumerable<CompanyPaymentDensityReportDto>> GetCompanyPaymentDensityAsync(DateTime startDate, DateTime endDate, string reportType);
	Task<IEnumerable<EmployeeExpenseDensityReportDto>> GetEmployeeExpenseDensityAsync(DateTime startDate, DateTime endDate, string reportType);
	Task<IEnumerable<ApprovalStatusReportDto>> GetExpenseApprovalStatusAsync(DateTime startDate, DateTime endDate, string reportType);
}
```

| Method                         | Açıklama                                                                             |
|--------------------------------|--------------------------------------------------------------------------------------|
| GetEmployeeRequestsAsync       | Personelin kendi işlem hareketlerini raporlama                                       |
| GetCompanyPaymentDensityAsync  | Şirketin günlük haftalık ve aylık ödeme yoğunluğu raporlama                          |
| GetEmployeeExpenseDensityAsync | Şirketin personel bazlı günlük haftalık ve aylık harcama yoğunluğunu raporlama       |
| GetExpenseApprovalStatusAsync  | Şirketin günlük haftalık aylık onaylanan ve red edilen masraf miktarlarını raporlama |

💠 IUserService

```csharp
public interface IUserService
{
    Task<UserCreateResponseDto> CreateAsync(UserCreateDto model);
    Task<AppUser> GetUserByIdAsync(string userId);
    Task<List<AppUser>> GetAllUsersAsync();
    Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
    Task<bool> DeleteUserAsync(string userId);
    Task AssignRoleToUserAsnyc(string userId, string role);
    Task<List<UserDto>> GetUsersByRoleAsync(string roleName);
	Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
}
```

| Method                   | Açıklama                                                                      |
|--------------------------|-------------------------------------------------------------------------------|
| CreateAsync              | Kulanıcı oluşturma                                                            |
| GetUserByIdAsync         | Id'ye göre kullanıcı bulma                                                    |
| GetAllUsersAsync         | Bütün kullanıcıları listeleme                                                 |
| UpdatePasswordAsync      | Şifre yenileme                                                                |
| DeleteUserAsync          | Kullanıcı silme (soft delete)                                                 |
| AssignRoleToUserAsnyc    | Kullanıcıya rol atama                                                         |
| GetUsersByRoleAsync      | Role göre kullanıcı bulma                                                     |
| UpdateRefreshTokenAsync  | Kullanıcıya ait refresh token’ı ve bu token’ın geçerlilik süresini güncelleme |

### Token

💠 ITokenService

```csharp
public interface ITokenService
{
	TokenDto CreateAccessToken(int second, AppUser appUser);
	string CreateRefreshToken();
}
```

| Method              | Açıklama                 |
|---------------------|--------------------------|
| CreateAccessToken   | Access Token oluşturma   |
| CreateRefreshToken  | Refresh Token oluşturma  |

## ✳️ Dtos

➜ Validation, Mapping işlemleri gibi işlemler için AuditLog, Expense, ExpenseCategory, Mail, PaymentSimulation, Report, Token ve User Dto'ları yer alır.

## ✳️ Features

➜ Uygulamadaki her bir işlevi (feature) ayrı bir klasör içinde gruplayarak CQRS (Command Query Responsibility Segregation) ve MediatR mimarisine uygun şekilde yapılandırır.

➜ Command klasöründe veri yazma işlemleri (örneğin kullanıcı ekleme)

➜ Query klasöründe veri okuma işlemleri (örneğin kullanıcı listeleme)

➜ Her biri için ilgili Request, Response ve Handler sınıfları yer alır.

Bu yapı sayesinde kod okunabilirliği artar, işlevler birbirinden ayrılır ve kolay test edilebilir hale gelir.

```mathematica
├── Commands/
│   ├── Expense/
│   │   ├── Create/
│   │   │   ├── CreateExpenseCommandHandler
│   │   │   ├── CreateExpenseCommandRequest
│   │   │   └── CreateExpenseCommandResponse
│   │   ├── Delete/
│   │   │   ├── DeleteExpenseCommandResponse
│   │   │   ├── DeleteExpenseCommandResponse
│   │   │   └── DeleteExpenseCommandResponse
│   │   └── UpdateStatus/
│   │       ├── UpdateExpenseStatusCommandResponse
│   │       ├── UpdateExpenseStatusCommandResponse
│   │       └── UpdateExpenseStatusCommandResponse
│   ├── ExpenseCategory/ 
│   │   ├── Create/
│   │   │   ├── CreateExpenseCategoryCommandHandler
│   │   │   ├── CreateExpenseCategoryCommandRequest
│   │   │   └── CreateExpenseCategoryCommandResponse
│   │   ├── Delete/
│   │   │   ├── DeleteExpenseCategoryCommandResponse
│   │   │   ├── DeleteExpenseCategoryCommandResponse
│   │   │   └── DeleteExpenseCategoryCommandResponse
│   │   └── Update/
│   │       ├── UpdateExpenseCategoryCommandResponse
│   │       ├── UpdateExpenseCategoryCommandResponse
│   │       └── UpdateExpenseCategoryCommandResponse
│   └── User/ 
│       ├── AssignRoleToUser/
│       │   ├── AssignRoleToUserCommandHandler
│       │   ├── AssignRoleToUserCommandRequest
│       │   └── AssignRoleToUserCommandResponse
│       ├── CreateUser/
│       │   ├── CreateUserCommandHandler
│       │   ├── CreateUserCommandRequest
│       │   └── CreateUserCommandResponse
│       ├── DeleteUser/
│       │   ├── DeleteUserCommandHandler
│       │   ├── DeleteUserCommandRequest
│       │   └── DeleteUserCommandResponse
│       ├── LoginUser/
│       │   ├── LoginUserCommandHandler
│       │   ├── LoginUserCommandRequest
│       │   └── LoginUserCommandResponse
│       ├── PasswordReset/
│       │   ├── PasswordResetCommandHandler
│       │   ├── PasswordResetCommandRequest
│       │   └── PasswordResetCommandResponse
│       ├── RefreshTokenLogin/
│       │   ├── RefreshTokenLoginCommandHandler
│       │   ├── RefreshTokenLoginCommandRequest
│       │   └── RefreshTokenLoginCommandResponse
│       ├── UpdatePassword/
│       │   ├── UpdatePasswordCommandHandler
│       │   ├── UpdatePasswordCommandRequest
│       │   └── UpdatePasswordCommandResponse
│       └── VerifyResetToken/
│           ├── VerifyResetTokenCommandHandler
│           ├── VerifyResetTokenCommandRequest
│           └── VerifyResetTokenCommandResponse
└──  Queries/
    ├── Expense/
    │   ├── GetAll/
    │   │   ├── GetAllExpensesQueryHandler
    │   │   ├── GetAllExpensesQueryRequest
    │   │   └── GetAllExpensesQueryResponse
    │   ├── GetByCategoryName/
    │   │   ├── GetExpensesByCategoryNameQueryHandler
    │   │   ├── GetExpensesByCategoryNameQueryRequest
    │   │   └── GetExpensesByCategoryNameQueryResponse
    │   ├── GetByFullName/
    │   │   ├── GetExpensesByFullNameQueryHandler
    │   │   ├── GetExpensesByFullNameQueryRequest
    │   │   └── GetExpensesByFullNameQueryResponse
    │   ├── GetById/
    │   │   ├── GetExpenseByIdQueryHandler
    │   │   ├── GetExpenseByIdQueryRequest
    │   │   └── GetExpenseByIdQueryResponse
    │   ├── GetByStatus/
    │   │   ├── GetExpensesByStatusQueryHandler
    │   │   ├── GetExpensesByStatusQueryRequest
    │   │   └── GetExpensesByStatusQueryResponse
    │   └── GetByUserId/
    │       ├── GetExpensesByUserIdQueryHandler
    │       ├── GetExpensesByUserIdQueryRequest
    │       └── GetExpensesByUserIdQueryResponse
    ├── ExpenseCategory/
    │   ├── GetAll/
    │   │   ├── GetAllExpensesCategoriesQueryHandler
    │   │   ├── GetAllExpensesCategoriesQueryRequest
    │   │   └── GetAllExpensesCategoriesQueryResponse
    │   └── GetById/
    │       ├── GetExpensesCategoryByIdQueryHandler
    │       ├── GetExpensesCategoryByIdQueryRequest
    │       └── GetExpensesCategoryByIdQueryResponse
    └── User/ 
        ├── GetAllUsers/
        │   ├── GetAllUsersQueryHandler
        │   ├── GetAllUsersQueryRequest
        │   └── GetAllUsersQueryResponse
        ├── GetUserById/
        │   ├── GetUserByIdQueryHandler
        │   ├── GetUserByIdQueryRequest
        │   └── GetUserByIdQueryResponse
        └── GetUsersByRole/
            ├── GetUsersByRoleQueryHandler
            ├── GetUsersByRoleQueryRequest
            └── GetUsersByRoleQueryResponse
```
