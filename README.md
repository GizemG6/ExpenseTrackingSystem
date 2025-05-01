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

### 1️⃣ Projeyi klonlayın:

```bash
git clone https://github.com/GizemG6/ExpenseTrackingSystem.git
cd ExpenseTrackingSystem
```

### 2️⃣ appsettings.json dosyasını yapılandırın:

Presentation/ExpenseTrackingSystem.API/appsettings.json içindeki bağlantı ve servis bilgilerini kendinize göre güncelleyin.

![image](https://github.com/user-attachments/assets/52c83ac9-8da6-475d-88d1-34c44ac597fb)

🔗 ConnectionStrings

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=....;Initial Catalog=ExpenseTrackingSystemDb;Trusted_Connection=True;TrustServerCertificate=true;"
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
  "SenderEmail": "......",
  "SenderPassword": "......",
  "EnableSSL": true
}
```

➜ Proje için gerçek mail adresi üzerinden uygulama şifresi oluşturuldu. Direkt bu kullanılabilir ya da kendi mailiniz ile uygulama şifresi oluşturarak bu yapılandırmayı değiştirebilirsiniz.

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

### 3️⃣ Migration'ı çalıştırarak veritabanını oluşturun:

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

### 4️⃣ Uygulamayı başlatın:

```bash
dotnet run --project Presentation/ExpenseTrackingSystem.API
```

ya da Visual Studio veya Rider üzerinden çalıştırabilirsiniz.

### 5️⃣ Swagger üzerinden test edin:

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
| UpdateDate	                | Kullanıcının güncellendiği tarih           |
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
| ReceiptFilePath	               | Fatura vb. için dosya yolu                  |
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
| SenderIban	    | Gönderenin IBAN'ı  |
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
| ActionDate  | İşlemin gerçekleştirildiği tarih         |

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

➜ Her biri için ilgili Request, Response ve Handler sınıfları yer alır. Burada Handler içinde Service'ler kullanılmıştır.

Bu yapı sayesinde kod okunabilirliği artar, işlevler birbirinden ayrılır ve kolay test edilebilir hale gelir.

```mathematica
├── Commands/
│   ├── Expense/
│   │   ├── Create/
│   │   │   └── CreateExpenseCommand(Request, Response, Handler)
│   │   ├── Delete/
│   │   │   └── DeleteExpenseCommand(Request, Response, Handler)
│   │   └── UpdateStatus/
│   │       └── UpdateExpenseStatusCommand(Request, Response, Handler)
│   ├── ExpenseCategory/ 
│   │   ├── Create/
│   │   │   └── CreateExpenseCategoryCommand(Request, Response, Handler)
│   │   ├── Delete/
│   │   │   └── DeleteExpenseCategoryCommand(Request, Response, Handler)
│   │   └── Update/
│   │       └── UpdateExpenseCategoryCommand(Request, Response, Handler)
│   └── User/ 
│       ├── AssignRoleToUser/
│       │   └── AssignRoleToUserCommand(Request, Response, Handler)
│       ├── CreateUser/
│       │   └── CreateUserCommand(Request, Response, Handler)
│       ├── DeleteUser/
│       │   └── DeleteUserCommand(Request, Response, Handler)
│       ├── LoginUser/
│       │   └── LoginUserCommand(Request, Response, Handler)
│       ├── PasswordReset/
│       │   └── PasswordResetCommand(Request, Response, Handler)
│       ├── RefreshTokenLogin/
│       │   └── RefreshTokenLoginCommand(Request, Response, Handler)
│       ├── UpdatePassword/
│       │   └── UpdatePasswordCommand(Request, Response, Handler)
│       └── VerifyResetToken/
│           └── VerifyResetTokenCommand(Request, Response, Handler)
└──  Queries/
    ├── Expense/
    │   ├── GetAll/
    │   │   └── GetAllExpensesQuery(Request, Response, Handler)
    │   ├── GetByCategoryName/
    │   │   └── GetExpensesByCategoryNameQuery(Request, Response, Handler)
    │   ├── GetByFullName/
    │   │   └── GetExpensesByFullNameQuery(Request, Response, Handler)
    │   ├── GetById/
    │   │   └── GetExpenseByIdQuery(Request, Response, Handler)
    │   ├── GetByStatus/
    │   │   └── GetExpensesByStatusQuery(Request, Response, Handler)
    │   └── GetByUserId/
    │       └── GetExpensesByUserIdQuery(Request, Response, Handler)
    ├── ExpenseCategory/
    │   ├── GetAll/
    │   │   └── GetAllExpensesCategoriesQuery(Request, Response, Handler)
    │   └── GetById/
    │       └── GetExpensesCategoryByIdQuery(Request, Response, Handler)
    └── User/ 
        ├── GetAllUsers/
        │   └── GetAllUsersQuery(Request, Response, Handler)
        ├── GetUserById/
        │   └── GetUserByIdQuery(Request, Response, Handler)
        └── GetUsersByRole/
            └── GetUsersByRoleQuery(Request, Response, Handler)
```

### 🔎 Command ve Query incelemesi

Kurulan yapıyı anlamak için CreateExpenseCommand ve GetAllExpensesQuery incelemesi

#### CreateExpenseCommand

MediatR kullanarak Masraf oluşturmak için oluşturulmuştur.

##### CreateExpenseCommandRequest

```csharp
public class CreateExpenseCommandRequest : IRequest<CreateExpenseCommandResponse>
{
	public string UserId { get; set; }
	public int CategoryId { get; set; }
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
	public string Location { get; set; }
	public IFormFile? ReceiptFile { get; set; }
}
```

🔺 MediatR'dan gelen IRequest<> interface kullanılarak Masraf oluşturmak için gerekli olan propertyler eklenmiştir.

##### CreateExpenseCommandResponse

```csharp
public class CreateExpenseCommandResponse
{
	public bool Success { get; set; }
	public string Message { get; set; }
}
```

🔺 Masraf oluştuktan sonra Success(masrafın başarılı bir şekilde oluşması) ve Message(başarılı mesajı) propertyleri tutulmuştur.

##### CreateExpenseCommandHandler

```csharp
public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommandRequest, CreateExpenseCommandResponse>
{
	private readonly IExpenseService _expenseService;

	public CreateExpenseCommandHandler(IExpenseService expenseService)
	{
		_expenseService = expenseService;
	}

	public async Task<CreateExpenseCommandResponse> Handle(CreateExpenseCommandRequest request, CancellationToken cancellationToken)
	{
		var expense = new ExpenseCreateDto
		{
			Amount = request.Amount,
			CategoryId = request.CategoryId,
			Date = request.Date,
			Location = request.Location,
			UserId = request.UserId,
			ReceiptFile = request.ReceiptFile
		};

		var result = await _expenseService.CreateAsync(expense);

		return new CreateExpenseCommandResponse
		{
			Success = true,
			Message = "Expense created successfully"
		};
	}
}
```

🔺 MediatR'dan gelen IRequestHandler<> interface'i kullanılarak gelen CreateExpenseCommandRequest Handle sınıfında işlenmiş ve geriye CreateExpenseCommandResponse dönmüştür.

#### GetAllExpensesQuery

MediatR kullanılarak tüm masrafları almak için oluşturulmuştur.

##### GetAllExpensesQueryRequest

```csharp
public class GetAllExpensesQueryRequest : IRequest<List<GetAllExpensesQueryResponse>>
{
}
```

🔺 MediatR'dan gelen IRequest interface'i kullanılarak List şekilde response döner.

##### GetAllExpensesQueryResponse

```csharp
public class GetAllExpensesQueryResponse
{
	public string UserId { get; set; }
	public int CategoryId { get; set; }
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
	public string Location { get; set; }
	public ExpenseStatus Status { get; set; }
	public string? ReceiptFilePath { get; set; }
}
```

🔺 Response olarak masraf bilgilerine yer verilmiştir.

##### GetAllExpensesQueryHandler

```csharp
public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQueryRequest, List<GetAllExpensesQueryResponse>>
{
	private readonly IExpenseService _expenseService;

	public GetAllExpensesQueryHandler(IExpenseService expenseService)
	{
		_expenseService = expenseService;
	}

	public async Task<List<GetAllExpensesQueryResponse>> Handle(GetAllExpensesQueryRequest request, CancellationToken cancellationToken)
	{
		var expenses = await _expenseService.GetAllAsync();
		return expenses.Select(e => new GetAllExpensesQueryResponse
		{
			UserId = e.UserId,
			CategoryId = e.CategoryId,
			Amount = e.Amount,
			Date = e.Date,
			Location = e.Location,
			Status = e.Status,
			ReceiptFilePath = e.ReceiptFilePath
		}).ToList();
	}
}
```

🔺 MediatR'dan gelen IRequestHandler<> interface kullanılarak GetAllExpensesQueryRequest Handler sınıfında işlenmiş ve geriye GetAllExpensesQueryResponse(List) dönmüştür.

## ✳️ Helpers

Yardımcı sınıflar burada tutulmuştur.

### CustomEncoders

```csharp
public static class CustomEncoders
{
	public static string UrlEncode(this string value)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(value);
		return WebEncoders.Base64UrlEncode(bytes);
	}
	public static string UrlDecode(this string value)
	{
		byte[] bytes = WebEncoders.Base64UrlDecode(value);
		return Encoding.UTF8.GetString(bytes);
	}
}
```

🔺CustomEncoders sınıfı, string türüne özel extension method'lar tanımlar. Bu methodlar sayesinde bir string'i Base64 URL formatında şifreleyebilir (encode), şifrelenmiş değeri çözüp (decode) orijinal haline getirebiliriz.

### FileHelper

```csharp
public class FileHelper
{
	public static async Task<string?> SaveReceiptFileAsync(IFormFile? file)
	{
		if (file == null || file.Length == 0)
			return null;

		var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "receipts");
		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		var fileName = $"{Guid.NewGuid()}_{file.FileName}";
		var filePath = Path.Combine(folderPath, fileName);

		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}

		return Path.Combine("receipts", fileName).Replace("\\", "/");
	}
}
```

🔺 Bu sınıf kullanıcıdan alınan bir dosyayı (örneğin bir fatura görseli) sunucuda wwwroot/receipts klasörüne kaydetmek ve dosya yolunu döndürmek için kullanılır.

## ✳️ Mapper

Burada AutoMapper kullanılarak MapperConfig sınıfı içinde bütün mapleme işlemleri gerçekleştirilmiştir.

## ✳️ Repositories

Repository Design Pattern uygulanarak base IReadRepository ve IWriteRepository oluşturulmuştur. Burada Expense, ExpenseCategory, Payment klasörleri mevcut ve içlerinden IReadRepository ve IWriteRepository interfacelerinin implement edildiği interfaceleri mevcuttur.

### 📚 IReadRepository

Bu interface, bir generic entity için read-only işlemleri tanımlar. 

```csharp
public interface IReadRepository<T, TKey> where T : class
{
	Task<List<T>> GetAllAsync(bool tracking = true);
	IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
	Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
	Task<T> GetByIdAsync(TKey id, bool tracking = true);
	DbSet<T> Table { get; }
}
```

| Method          | Açıklama                                                                                                                       |
|-----------------|--------------------------------------------------------------------------------------------------------------------------------|
| GetAllAsync     | Veritabanındaki tüm kayıtları getirir. tracking parametresi sayesinde EF Core’un change tracker özelliği açılıp kapatılabilir. |
| GetWhere        | Belirli bir şarta (predicate) uyan kayıtları getirir.                                                                          |
| GetSingleAsync  | Belirli bir şarta uyan tek bir nesneyi getirir.                                                                                |
| GetByIdAsync    | Verilen id'ye sahip nesneyi getirir.                                                                                           |
| DbSet<T> Table  | EF Core üzerindeki DbSet<T>'e doğrudan erişim sağlar.                                                                          |

### ✍ IWriteRepository

Bu interface, generic bir entity için yazma (write) işlemlerini tanımlar. 

```csharp
public interface IWriteRepository<T, Tkey> where T : class
{
	Task<bool> AddAsync(T entity);
	Task<bool> AddRangeAsync(List<T> entities);
	Task<bool> RemoveAsync(Tkey id);
	Task<bool> RemoveRangeAsync(List<Tkey> ids);
	Task<bool> UpdateAsync(T entity);
	Task<bool> SaveChangesAsync();
}
```

| Method           | Açıklama                                                                                                                  |
|------------------|---------------------------------------------------------------------------------------------------------------------------|
| AddAsync         | Verilen tek bir entity'yi veritabanına ekler.                                                                             |
| AddRangeAsync    | Birden fazla entity'yi aynı anda ekler (toplu ekleme işlemi).                                                             |
| RemoveAsync      | Verilen id'ye sahip entity'yi veritabanından siler.                                                                       |
| RemoveRangeAsync | Birden fazla id'ye sahip entity'yi topluca siler                                                                          |
| UpdateAsync      | Verilen entity’yi günceller.                                                                                              |
| SaveChangesAsync | EF Core’un DbContext.SaveChangesAsync() fonksiyonunu tetikler, yapılan tüm işlemleri veritabanına kalıcı olarak kaydeder. |

## ✳️ Validators

Burada FluentValidation kullanılarak her entity için validasyon işlemleri gerçekleştirilmiştir.

Örnek olarak ExpenseValitator:

```csharp
public class ExpenseValidator : AbstractValidator<ExpenseCreateDto>
{
	public ExpenseValidator()
	{
		RuleFor(x => x.UserId)
			.NotEmpty();

		RuleFor(x => x.CategoryId)
			.GreaterThan(0);

		RuleFor(x => x.Amount)
			.GreaterThan(0);

		RuleFor(x => x.Date)
			.NotEmpty()
			.LessThanOrEqualTo(DateTime.Now);

		RuleFor(x => x.Location)
			.NotEmpty()
			.MaximumLength(100);
	}
}
```

## ✳️ ServiceRegistration.cs

Onion Architecture katmanlı mimari uygulandığı için her katmanın configuration'ları için ServiceRegistration sınıfı oluşturulmuştur. Buradaki amaç Program.cs'i sadeleştirmektir.

```csharp
public static class ServiceRegistration
{
	public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddControllers().AddFluentValidation(x =>
		{
			x.RegisterValidatorsFromAssemblyContaining<AuditLogValidator>();
			x.RegisterValidatorsFromAssemblyContaining<ExpenseCategoryValidator>();
			x.RegisterValidatorsFromAssemblyContaining<ExpenseValidator>();
			x.RegisterValidatorsFromAssemblyContaining<PaymentSimulationValidator>();
			x.RegisterValidatorsFromAssemblyContaining<UserValidator>();
		});

		services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MapperConfig())).CreateMapper());

		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
	}
}
```

🔺 Bu sınıfta FluentValidation, Mapping ve MediatR configuration'ları yapılandırılmıştır.

# 🧩 Infrastructure/Persistence Katmanı

Bu katman, bir uygulamanın data access işlemlerinin gerçekleştirildiği bölümdür. Bu katman genellikle Entity Framework, Dapper gibi ORM araçlarıyla çalışır ve uygulamanın veritabanıyla olan bağlantısını yönetir.

📚Kullanılan Paketler

Dapper

Microsoft.AspNetCore.Authentication.JwtBearer

Microsoft.EntityFrameworkCore.Design

Microsoft.EntityFrameworkCore.SqlServer

Microsoft.EntityFrameworkCore.Tools

StackExchange.Redis

## ✳️ Context

Veritabanı bağlantısını ve ilişkili tabloları yöneten ana yapıları içerir.

### ✳️ AppDbContext

Bu sınıf, projenin veritabanı ile olan ilişkisinin tanımlandığı ana sınıftır. IdentityDbContext sınıfından kalıtım alır; bu sayede AppUser ve AppRole ile kimlik yönetimi sağlanır.

### ✳️ AppDbContextFactory

Bu sınıf, design-timesenaryoları için kullanılır. EF Core Migrations işlemleri sırasında DbContext nesnesini çalıştırmak amacıyla oluşturulmuştur. Uygulama başlatılmadan önce, appsettings.json içindeki bağlantı bilgilerini okuyarak bir AppDbContext örneği oluşturur. "dotnet ef migrations add" ve "dotnet ef database update" gibi komutlar bu sınıfı kullanarak bağlanacağı veritabanını belirler.

## ✳️ Migrations

Burada InitialMigration dosyası yer alır. InitialMigration içinde "update-database" yapıldığında database'e default olarak gelecek 2 admin, 2 rol(Admin ve Employee) ve 3 adet kategori eklenmiştir.

Admin Bilgileri

| Property         | Admin 1                     | Admin 2                     |
|------------------|-----------------------------|-----------------------------|
| UserName         | gizemadmin                  | gunesadmin                  |
| Email            | 0.gizemgunes@gmail.com      | admin@example.com           |
| Password         | Admin123.                   | Admin234.                   |
| FullName         | Gizem Admin                 | Gunes Admin                 |
| Title            | System Admin                | System Admin                |
| IBAN             | TR12345123451234512345123   | TR12345123451234512345124   |
| PhoneNumber      | 5000000000                  | 5000000001                  |

MailService için Admin 1'in mail hesabı olarak gerçek mail hesabı kullanılmıştır. appsettings.json dosyası içinde ayrıca bahsedilecektir. Diğer bilgiler default bilgilerdir.

Kategori olarak 3 adet default kategori eklenmiştir (Yol, Yemek, Konaklama).

## ✳️ Repositories

Application katmanı içinde yer alan Repositories klasörü, Dependency Inversion Principle amacıyla interface barındırırken, burada onların concrete implementasyonları mevcuttur.

### 📚 ReadRepository

```csharp
public class ReadRepository<T, TKey> : IReadRepository<T, TKey> where T : class
{
	private readonly AppDbContext _context;

	public ReadRepository(AppDbContext context)
	{
		_context = context;
	}

	public DbSet<T> Table => _context.Set<T>();

	public async Task<List<T>> GetAllAsync(bool tracking = true)
	{
		var query = Table.AsQueryable();
		if (!tracking)
			query = query.AsNoTracking();

		return await query.ToListAsync();
	}

	public async Task<T> GetByIdAsync(TKey id, bool tracking = true)
	{
		var entity = await Table.FindAsync(id);

		if (entity == null)
			return null;

		if (!tracking)
			_context.Entry(entity).State = EntityState.Detached;

		return entity;
	}

	public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
	{
		var query = Table.AsQueryable();
		if (!tracking)
			query = query.AsNoTracking();

		return await query.FirstOrDefaultAsync(method);
	}

	public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
	{
		var query = Table.Where(method);
		if (!tracking)
			query = query.AsNoTracking();

		return query;
	}
}
```

AppDbContext kullanılarak IReadRepository implement edilip içleri doldurulmuştur.

### ✍ WriteRepository

```csharp
public class WriteRepository<T, TKey> : IWriteRepository<T, TKey> where T : class
{
	private readonly AppDbContext _context;

	public WriteRepository(AppDbContext context)
	{
		_context = context;
	}
	public DbSet<T> Table => _context.Set<T>();

	public async Task<bool> AddAsync(T entity)
	{
		await Table.AddAsync(entity);
		return await SaveChangesAsync();
	}

	public async Task<bool> AddRangeAsync(List<T> entities)
	{
		await Table.AddRangeAsync(entities);
		return await SaveChangesAsync();
	}

	public async Task<bool> RemoveAsync(TKey id)
	{
		var entity = await Table.FindAsync(id);
		if (entity == null)
			return false;

		Table.Remove(entity);
		return await SaveChangesAsync();
	}

	public async Task<bool> RemoveRangeAsync(List<TKey> ids)
	{
		var entities = await Table.Where(e => ids.Contains(EF.Property<TKey>(e, "Id"))).ToListAsync();
		if (entities.Any())
		{
			Table.RemoveRange(entities);
		}
		return await SaveChangesAsync();
	}

	public async Task<bool> UpdateAsync(T entity)
	{
		Table.Update(entity);
		return await SaveChangesAsync();
	}

	public async Task<bool> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}
}
```

AppDbContext kullanılarak IWriteRepository implement edilip içleri doldurulmuştur.

Aynı işlemler Expense, ExpenseCategory ve Payment için gerçekleştirilmiştir.

Örnek olarak:

### 💲 ExpenseReadRepository

```csharp
public class ExpenseReadRepository : ReadRepository<Expense, Guid>, IExpenseReadRepository
{
	public ExpenseReadRepository(AppDbContext context) : base(context) { }
}
```

Bu sınıf, Expense verilerini okuma işlemleri için oluşturulmuş özel bir repository sınıfıdır. ExpenseReadRepository sınıfı, ReadRepository<Expense, Guid> sınıfından kalıtım alır. IExpenseReadRepository arayüzünü uygular. 

## ✳️ Services

Burada Application katmanında yer alan interface servislerini implement eden servisler vardır. Dependency Inversion ve Interface Segregation uygulanmıştır. Bağımlılığı azaltmak amaçlanmıştır.

### ⭐ AuditLogService

Bu servis sınıfı içinde Dapper, Logging, Redis(GetAuditLogsAsync cache'te tutma) kullanılmıştır. IAuditLogService implement edilerek GetAuditLogsAsync ve LogActionAsync methotları doldurulmuştur.

### ⭐ AuthService

Burada IConfiguration, UserManager<AppUser>, ITokenService, SignInManager<AppUser>, IUserService, IMailService ve IAuditLogService kullanılarak Authentication işlemleri gerçekleştirilmiştir. IAuthService implement edilmiştir.

| Method                 | Açıklama                                                                             |
|------------------------|--------------------------------------------------------------------------------------|
| VerifyResetTokenAsync  | Şifre sıfırlama işlemi için sağlanan token'ı doğrulamak amacıyla kullanılır          |
| LoginAsync             | Kullanıcı maili ve şifre ile yapılan giriş işlemini yönetir                          |
| RefreshTokenLoginAsync | Mevcut bir refresh token ile yeni bir access token'ı almayı sağlar                   |
| LoginAsync             | Kullanıcıların şifrelerini sıfırlayabilmesi için bir şifre sıfırlama isteği başlatır |

### ⭐ ExpenseCategoryService

IExpenseCategoryService implement edilerek IExpenseCategoryReadRepository, IExpenseCategoryWriteRepository ve IMapper kullanılarak GetByIdAsync, GetAllAsync, CreateAsync, UpdateAsync, DeleteAsync methotları doldurulmuştur.

### ⭐ ExpenseService

Bu servis sınıfı IExpenseService implement ederek masraf ile ilgili işlemleri gerçekleştirir.

#### 🔎 ExpenseService sınıfı içindeki CreateAsync ve UpdateStatusAsync methotların incelenmesi

#### CreateAsync

```csharp
public async Task<Expense> CreateAsync(ExpenseCreateDto expenseCreateDto)
{
	var user = await _userManager.FindByIdAsync(expenseCreateDto.UserId);
	if (user == null)
		throw new Exception("User not found");

	var category = await _expenseCategoryReadRepository.GetByIdAsync(expenseCreateDto.CategoryId);
	if (category == null)
		throw new Exception("Category not found");

	string? receiptPath = await FileHelper.SaveReceiptFileAsync(expenseCreateDto.ReceiptFile);

	var expense = new Expense
	{
		Id = Guid.NewGuid(),
		UserId = expenseCreateDto.UserId,
		CategoryId = expenseCreateDto.CategoryId,
		Amount = expenseCreateDto.Amount,
		Date = expenseCreateDto.Date,
		Location = expenseCreateDto.Location,
		Status = ExpenseStatus.Pending,
		ReceiptFilePath = receiptPath
	};

	await _expenseWriteRepository.AddAsync(expense);
	await _expenseWriteRepository.SaveChangesAsync();
	await _auditLogService.LogActionAsync(expense.UserId, "Create", "Expense", expense.Id.ToString());

	await SendExpenseCreatedMailAsync(user, category, expense);

	return expense;
}

private async Task SendExpenseCreatedMailAsync(AppUser user, ExpenseCategory category, Expense expense)
{
	var adminEmails = await GetAdminEmailsAsync();

	await _mailService.SendExpenseCreatedMailAsync(
		adminEmails.ToArray(),
		user.UserName,
		category.Name,
		expense.Amount,
		expense.Date,
		expense.Id.ToString()
	);
}
```
🔺 Bu methot ExpenseCreateDto kullanarak masraf oluşturup expense döner. Öncelikle expenseCreateDto içinden yer alan UserId'ye göre Identity'den gelen UserManager ile kullanıcıyı tespit eder.

🔺 2. adım olarak IExpenseCategoryReadRepository'den oluşturulan _expenseCategoryReadRepository(dependency injection ile) içindeki GetByIdAsync methodunu kullanarak Id'ye göre kategori bulur.

🔺 Daha sonra kullanıcın ekleyeceği fatura dosyası için Application katmanında yer alan FileHelper methodu ile eklenen dosyanın dosya yolunu alır.

🔺 Bütün bu işlemlerden sonra oluşan masrafı IExpenseWriteRepository'den oluşturulan _expenseWriteRepository ile veritabanına ekleyip kaydeder.

🔺 IAuditLogService'den oluşturulan _auditLogService ile bu kayıt AuditLog'a kaydedilir.

🔺 En son SendExpenseCreatedMailAsync ile oluşan masraf bilgileri Admin'e mail olarak gönderilir.

#### UpdateStatusAsync

```csharp
public async Task<Expense> UpdateStatusAsync(Expense expense)
{
	var existingExpense = await _expenseReadRepository.GetByIdAsync(expense.Id)
		?? throw new Exception("Expense not found");

	ValidateRejectionReason(expense);
	await UpdateExpenseStatusAsync(existingExpense, expense);

	if (expense.Status == ExpenseStatus.Approved)
		await CreatePaymentSimulationAsync(existingExpense);

	var user = await _userManager.FindByIdAsync(existingExpense.UserId);
	if (user != null)
	{
		string expenseStatus = expense.Status.ToString();
		_mailService.SendExpenseStatusUpdateMailAsync(user.Email, expenseStatus, expense.Id.ToString());
	}

	await _auditLogService.LogActionAsync(expense.UserId, "UpdateStatus", "Expense", expense.Id.ToString());

	return existingExpense;
}

private void ValidateRejectionReason(Expense expense)
{
	if (expense.Status == ExpenseStatus.Rejected && string.IsNullOrWhiteSpace(expense.RejectionReason))
		throw new Exception("Rejection reason is required when status is Rejected.");
}

private async Task UpdateExpenseStatusAsync(Expense existingExpense, Expense updatedExpense)
{
	existingExpense.Status = updatedExpense.Status;
	existingExpense.RejectionReason = updatedExpense.Status == ExpenseStatus.Rejected
		? updatedExpense.RejectionReason
		: null;

	await _expenseWriteRepository.UpdateAsync(existingExpense);
	await _expenseWriteRepository.SaveChangesAsync();
}

private async Task CreatePaymentSimulationAsync(Expense expense)
{
	var receiver = await _userManager.FindByIdAsync(expense.UserId)
		?? throw new Exception("Receiver (AppUser) not found for the expense.");

	var currentUser = _httpContextAccessor.HttpContext?.User;

	var sender = await _userManager.GetUserAsync(currentUser)
		?? throw new Exception("Current admin user not found.");

	var payment = new PaymentSimulation
	{
		Id = Guid.NewGuid(),
		PaymentDate = DateTime.UtcNow,
		BankReferenceNo = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
		Expense = expense,
		PaidAmount = expense.Amount,
		SenderFullName = sender.FullName,
		SenderIban = sender.IBAN,
		ReceiverFullName = receiver.FullName,
		ReceiverIban = receiver.IBAN
	};

	await _paymentWriteRepository.AddAsync(payment);
	await _paymentWriteRepository.SaveChangesAsync();
}

private async Task<IEnumerable<string>> GetAdminEmailsAsync()
{
	var adminRole = await _roleManager.FindByNameAsync("Admin");

	if (adminRole == null)
	{
		return Enumerable.Empty<string>();
	}

	var users = await _userManager.Users.ToListAsync();

	var adminEmails = new List<string>();

	foreach (var user in users)
	{
		if (await _userManager.IsInRoleAsync(user, adminRole.Name))
		{
			adminEmails.Add(user.Email);
		}
	}

	return adminEmails;
}
```

🔺 Bu methot ilk olarak IExpenseReadRepository içindeki GetByIdAsync ile Id'ye göre masrafı bulur.

🔺 ValidateRejectionReason methodu ile Expense reddedildiğinde (Status = Rejected) mutlaka bir reddetme sebebi (RejectionReason) girilmiş olmasını kontrol eder.

🔺 UpdateExpenseStatusAsync methodu ile ExpenseStatus güncellenir.

🔺 Eğer masraf onaylandıysa CreatePaymentSimulationAsync methodu ile ödeme gerçekleşir. Bu methot Admin tarafından masrafı oluşturan personele ödeme yapar.

### ⭐ ReportService

Bu servis sınıfı IReportService sınıfını implement edip Dapper ve Redis(raporlama yaparken cache'te tutmak için) kullanılmıştır. 

### ⭐ TokenService

ITokenService sınıfı implement edilerek CreateAccessToken ve CreateRefreshToken methotları doldurulmuştur.

### ⭐ UserService

Bu sınıf IUserService'ini implement eder. AssignRoleToUserAsnyc, CreateAsync, DeleteUserAsync(soft delete), GetAllUsersAsync, GetUserByIdAsync, GetUsersByRoleAsync, UpdatePasswordAsync ve UpdateRefreshTokenAsync methotları doldurulmuştur.

İçinde 2 ayrı private oluşturulmuş yardımcı methotlar var.

```csharp
private string GenerateValidUsername(string fullName)
{
	var normalized = fullName
		.ToLower()
		.Replace("ç", "c").Replace("ğ", "g")
		.Replace("ı", "i").Replace("ö", "o")
		.Replace("ş", "s").Replace("ü", "u");

	var username = new string(normalized.Where(char.IsLetterOrDigit).ToArray());

	return string.IsNullOrWhiteSpace(username) ? "user" + Guid.NewGuid().ToString("N").Substring(0, 6) : username;
}

private static class RoleConstants
{
	public const string Admin = "Admin";
	public const string Employee = "Employee";

	public static readonly string[] AllRoles = { Admin, Employee };
}
```

🔺 GenerateValidUsername methodu kullanıcı create edilirken UserName'i otomatik oluşturmak amaçlı yapılmıştır.

🔺 RoleConstants sistemde kullanılacak rolleri merkezi ve sabit bir şekilde tanımlamak için oluşturulmuş bir static class'tir. 

## ✳️ ServiceRegistration.cs

Dependency Injection yoluyla Persistence katmanındaki servis ve bileşenlerin uygulamaya eklenmesini sağlar. Redis connection, DbContext connection, Identity configuration'ları ve tüm service ve repository enjeksiyonları burada yer alır.

# 🧩 Infrastructure/Infrastructure Katmanı

Uygulamanın dış sistemlerle (örneğin e-posta servisi, mesaj kuyruğu sistemi vb.) entegrasyonunu sağlayan altyapı servislerini içerir.

📚Kullanılan Paketler

Hangfire.AspNetCore

Hangfire.Core

Hangfire.Redis.StackExchange

RabbitMQ.Client

## ✳️ Services

Burada MailService, RabbitMqService ve RabbitMqBackgroundService service sınıfı yer alır.

### ✳️ MailService

Bu servis sınıfı IMailService methodunu implement eder, RabbitMqService kullanarak mail işlemleri gerçekleştirir.

### ✳️ RabbitMqService

RabbitMQ ile mesaj gönderme (publish) ve dinleme (consume) işlemlerini gerçekleştiren bir servis sınıfıdır. IDisposable interface'ini implement eder. 3 adet methot içerir.

| Method          | Açıklama                                                                           |
|-----------------|------------------------------------------------------------------------------------|
| PublishMessage  | RabbitMQ'da belirtilen kuyruk ismine mesaj gönderir.                               |
| ListenToQueue   | Belirtilen kuyruğu dinler.                                                         |
| Dispose         | Uygulama kapanırken veya servis dispose edilirken bağlantılar düzgünce kapatılır.  |

### ✳️ RabbitMqBackgroundService

Arka planda çalışan bir servis tanımıdır ve RabbitMQ kuyruğunu dinleyip gelen mail isteklerini MailService aracılığıyla göndermek için kullanılır. Bu service sınıfı içinde job'ları arka planda yürütmesi için HangFire kullanılmıştır. İçinde ExecuteAsync methodu yer alır. Bu methot emailQueue adındaki RabbitMQ kuyruğunu dinler. Kuyruğa bir mesaj geldiğinde JSON formatındaki mesaj MailRequest nesnesine deserialize edilir. Gelen mail isteği varsa, Hangfire kütüphanesi kullanılarak mail gönderimi arka planda kuyruklanır.

## ✳️ ServiceRegistration.cs

Infrastructure katmanındaki servislerin Dependency Injection ile projeye eklenmesini sağlar. Hangfire, RabbitMQ, MailService configuration'ları ve enjeksiyonları burada yer alır.

# 🧩 Presentation/API Katmanı

Bu katman, client gelen HTTP isteklerini karşılayan API'yi içerir. Uygulamanın dış dünya ile olan iletişim noktasıdır.

📚Kullanılan Paketler

Serilog

Serilog.AspNetCore

Serilog.Sinks.MSSqlServer

Microsoft.EntityFrameworkCore.Design

## ✳️ Controllers

API Controller sınıflarını içerir. AuthsController, ExpenseCategoriesController, ExpensesController, ReportController ve UsersController burada yer alır.

🔎 Örnek inceleme

### ExpensesController

```csharp
[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
	private readonly IMediator _mediator;

	public ExpensesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> GetAllExpenses()
	{
		var response = await _mediator.Send(new GetAllExpensesQueryRequest());
		return Ok(response);
	}

	[HttpGet("{Id}")]
	public async Task<IActionResult> GetExpenseById([FromRoute]GetExpenseByIdQueryRequest getExpenseByIdQueryRequest)
	{
		GetExpenseByIdQueryResponse response = await _mediator.Send(getExpenseByIdQueryRequest);
		return Ok(response);
	}

	[HttpGet("by-status")]
	public async Task<IActionResult> GetExpensesByStatus([FromQuery]GetExpensesByStatusQueryRequest getExpensesByStatusQueryRequest)
	{
		List<GetExpensesByStatusQueryResponse> response = await _mediator.Send(getExpensesByStatusQueryRequest);
		return Ok(response);
	}

	[HttpGet("by-userId")]
	public async Task<IActionResult> GetExpensesByUserId([FromQuery]GetExpensesByUserIdQueryRequest getExpensesByUserIdQueryRequest)
	{
		List<GetExpensesByUserIdQueryResponse> response = await _mediator.Send(getExpensesByUserIdQueryRequest);
		return Ok(response);
	}

	[HttpGet("by-full-name")]
	public async Task<IActionResult> GetExpensesByFullName([FromQuery]GetExpensesByFullNameQueryRequest getExpensesByFullNameQueryRequest)
	{
		List<GetExpensesByFullNameQueryResponse> response = await _mediator.Send(getExpensesByFullNameQueryRequest);
		return Ok(response);
	}

	[HttpGet("by-category-name")]
	public async Task<IActionResult> GetExpensesByCategoryName([FromQuery]GetExpensesByCategoryNameQueryRequest getExpensesByCategoryNameQueryRequest)
	{
		List<GetExpensesByCategoryNameQueryResponse> response = await _mediator.Send(getExpensesByCategoryNameQueryRequest);
		return Ok(response);
	}

	[HttpPost]
	public async Task<IActionResult> CreateExpense([FromForm]CreateExpenseCommandRequest createExpenseCommandRequest)
	{
		CreateExpenseCommandResponse response = await _mediator.Send(createExpenseCommandRequest);
		return Ok(response);
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> UpdateExpenseStatus([FromBody]UpdateExpenseStatusCommandRequest updateExpenseStatusCommandRequest)
	{
		UpdateExpenseStatusCommandResponse response = await _mediator.Send(updateExpenseStatusCommandRequest);
		return Ok(response);
	}

	[HttpDelete("Id")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> DeleteExpense([FromBody]DeleteExpenseCommandRequest deleteExpenseCommandRequest)
	{
		DeleteExpenseCommandResponse response = await _mediator.Send(deleteExpenseCommandRequest);
		return Ok(response);
	}
}
```

🔺 Bu API Controller MediatR kullanarak API endpointlerini oluşturuyor.

🔺 Bazı endpointlere [Authorize(Roles = "Admin")] attribute ile erişim sınırı verilmiştir.

## ✳️ Middlewares

### ConfigureExceptionHandlerMiddleware

```csharp
public static class ConfigureExceptionHandlerMiddleware
{
	public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
	{
		application.UseExceptionHandler(builder =>
		{
			builder.Run(async context =>
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = MediaTypeNames.Application.Json;

				var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
				if (contextFeature != null)
				{
					logger.LogError(contextFeature.Error.Message);

					await context.Response.WriteAsync(JsonSerializer.Serialize(new
					{
						StatusCode = context.Response.StatusCode,
						Message = contextFeature.Error.Message,
						Title = "Error!"
					})); ;
				}
			});
		});
	}
}
```

🔺 Uygulamada özel bir global exception handling mekanizması kurmak için kullanılan bir middleware yapılandırma sınıfıdır.

🔺 Bu method bir hata oluştuğunda yanıtın durum kodunu 500 Internal Server Error olarak ayarlar. Yanıt tipini application/json yapar. 

🔺 Oluşan hataya ait detaylara IExceptionHandlerFeature aracılığıyla ulaşılır. Hata mesajı loglanır. Hata bilgileri JSON formatında istemciye döndürülür.

🔺 Bu yapı sayesinde kullanıcı dostu bir hata mesajı alınır.

## ✳️ appsettings.json

Burada veritabanı bağlantısı(ConnectionStrings), Token, Mail, Redis ve RabbitMQ temel yapılandırma ayarları yer alır.

### ✳️ ConnectionStrings

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Initial Catalog=ExpenseTrackingSystemDb;Trusted_Connection=True;TrustServerCertificate=true;"
}
```

🔺 Gerekli ConnectionStrings yapılır. Kurulum kısmında bahsedilmiştir.

### ✳️ MailSettings

```json
"MailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SenderEmail": "...",
  "SenderPassword": "...",
  "EnableSSL": true
},
```

🔺 Uygulama için gerçek bir mail ve uygulama şifresi oluşturulup kullanılmıştır. İsteğe göre yapılandırılabilir.


### ✳️ Redis

```json
"Redis": {
  "ConnectionString": "localhost:1453"
}
```

🔺 Redis için gerekli yapılandırma burada yapılır. Kurulum kısmında bahsedilmiştir.

### ✳️ RabbitMQ

```json
"RabbitMQ": {
  "Host": "localhost",
  "Port": 5672,
  "Username": "guest", 
  "Password": "guest"
}
```

🔺 RabbitMQ için gerekli yapılandırma burada yapılır. Kurulum kısmında bahsedilmiştir.

## ✳️ Program.cs

Bu dosya, uygulamanın başlangıç noktasıdır ve tüm servislerin, middleware’lerin ve yapılandırmaların ayarlandığı yerdir. 

### 🧰 Services

```csharp
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
```

🔺 Uygulamanın katmanlı mimarideki servislerini (Application, Persistence, Infrastructure) DI (Dependency Injection) sistemine ekler.

### 📜 Serilog Loglama Yapılandırması

```csharp
var columnOptions = new ColumnOptions();

Logger log = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File("logs/log.txt")
	.WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
	sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
	columnOptions: columnOptions)
	.Enrich.FromLogContext()
	.MinimumLevel.Information()
	.CreateLogger();

builder.Host.UseSerilog(log);
```

🔺 Serilog ile loglar: console'a, dosyaya (logs/log.txt), SQL Server veritabanına yazılır. SQL’de loglar için Logs adında bir tablo otomatik oluşturulur.

### 📘 Swagger (API Dokümantasyonu)

```csharp
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
					  "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
					  "Example: \"Bearer 12345abcdef\""
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});
```

🔺 Swagger ile uygulamanın tüm API endpointleri otomatik olarak belgelenir. JWT authentication için Swagger arayüzünde token girme imkanı tanınır.

### 🔐 JWT Authentication Ayarları

```csharp
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new()
		{
			ValidateAudience = true,
			ValidateIssuer = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,

			ValidAudience = builder.Configuration["Token:Audience"],
			ValidIssuer = builder.Configuration["Token:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
			LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
			{
				return expires != null && expires > DateTime.UtcNow;
			},
			NameClaimType = ClaimTypes.Name
		};
	});
```

🔺 Uygulama JWT token kullanarak güvenliği sağlar. 

🔺 Token:Audience, Token:Issuer, Token:SecurityKey gibi değerler appsettings.json üzerinden alınır. 

🔺 Token’ın geçerliliği (süre, imza, hedef kullanıcı) kontrol edilir.

### ⚙️ Middleware ve Uygulama Çalıştırma

```csharp
app.ConfigureExceptionHandler(...);
app.UseHttpsRedirection();
app.UseHangfireDashboard("/hangfire");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

🔺 Hata yönetimi özel bir middleware ile yapılandırılır.

🔺 Hangfire arayüzü /hangfire yolu üzerinden erişilebilir olur.

🔺 Authentication ve Authorization işlemleri sırayla uygulanır.

# 🧩 Test 

Bu projede, iş katmanlarının doğruluğunu kontrol etmek için xUnit test framework’ü kullanılmaktadır. Testler Test isimli ayrı bir katmanda yer alır ve birim testler ile servislerin beklenen davranışları test edilir.

## ✳️ AuthServiceTests

Kullanıcı adı ve şifresi doğru girildiğinde AuthService.LoginAsync metodunun geçerli bir JWT access token ve refresh token döndürmesini test eder.

## ✳️ ExpenseServiceTests

Bu test sınıfı, masraf (expense) oluşturma işleminin ExpenseService üzerinden doğru şekilde gerçekleşip gerçekleşmediğini test eder.

## ✳️ MailServiceTests

Bu test sınıfı, MailService bileşeninin harcama oluşturulduğunda yöneticilere e-posta bildirimini RabbitMQ kuyruğuna doğru şekilde gönderip göndermediğini test eder.

## ✳️ UserServiceTests

Bu sınıf, UserService içerisinde yer alan CreateAsync metodunun kullanıcı oluşturma sürecinde başarılı mı yoksa hatalı mı davrandığını test etmek için yazılmıştır.

