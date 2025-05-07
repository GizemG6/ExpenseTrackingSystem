# ExpenseTrackingSystem 

📝 Description

This project is designed specifically for companies to track and manage expense items of field employees. It is an expense tracking system where users can monitor their expenses based on categories, roles are clearly separated, and a secure API infrastructure is provided. A layered and maintainable architecture has been established using Onion Architecture.

🔎 Project Details

Field employees can instantly enter their expenses into the system, and employers can monitor and approve these expenses without delay, allowing immediate reimbursement. Employees will no longer need to collect physical receipts and documents, and even during long periods in the field, they will receive timely payments.

The application serves two roles within the company: Admin and Employee.

Field employees can only submit expense entries and request reimbursements.

They can view their existing requests and track their status, including those pending approval.

Company users with admin privileges can view, approve, or reject these requests.

For approved requests, instant payments are processed via bank integration, and the related amount is transferred to the employee's account via EFT. For rejected requests, a comment field is available to provide a reason, which the employee can see to understand why their expense was denied.

## 🛠️ Technologies Used

| Teknoloji            | Açıklama                                  |
|----------------------|-------------------------------------------|
| .NET 8               | Framework                                 |
| EntityFramework Core | Code First data access                    |
| MediatR              | CQRS and handler structure                |
| FluentValidation     | Model validation                          |
| Identity             | User identity management                  |
| JWT	               | Authentication using JSON Web Tokens      |
| Redis                | Caching                                   |
| RabbitMQ             | Message queue and asynchronous operations |
| Serilog              | Logging                                   |
| Hangfire             | Background job processing                 |
| Dapper               | Performance-focused micro ORM             |
| SQL Server           | Database                                  |
| xUnit	               | Unit testing framework                    |
| FluentAssertions     | More readable test assertions             |
| Moq	               | Mocking library                           |
| Swagger	       | API documentation and testing tool        |

## 🛠️ Architecture and Design Patterns Used

| Architecture	       | Design Pattern                        |
|----------------------|---------------------------------------|
| Onion Architecture   | Service and Repository Design Pattern |

📌  For more detailed information about Onion Architecture:

[Onion Architecture](https://medium.com/@0.gizemgunes/onion-architecture-nedir-ve-yaz%C4%B1l%C4%B1mda-nas%C4%B1l-kullan%C4%B1l%C4%B1r-c77a4a8cf18f)

## ⚙️ Requirements (Prerequisites):

✅ .NET 8 SDK

✅ Docker Desktop (to run services like RabbitMQ and Redis in containers)

✅ SQL Server 2022 Developer Edition or a SQL Server container via Docker

✅ Visual Studio 2022+ or Rider

## 🛠️ Installation

Follow the steps below to run the project:

### 1️⃣ Clone the project:

```bash
git clone https://github.com/GizemG6/ExpenseTrackingSystem.git
cd ExpenseTrackingSystem
```

### 2️⃣ Configure the appsettings.json file:

Update the connection strings and service configurations in Presentation/ExpenseTrackingSystem.API/appsettings.json according to your environment.

![Ekran görüntüsü 2025-05-03 172821](https://github.com/user-attachments/assets/e71a114c-53a0-441e-beff-860b39bce550)

🔗 ConnectionStrings

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=....;Initial Catalog=ExpenseTrackingSystemDb;Trusted_Connection=True;TrustServerCertificate=true;"
}
```

➜ Change the Server field to match your SQL Server instance (e.g., DESKTOP-XXXX\SQLEXPRESS).

➜ The other fields can remain the same.

🔐 Token

➜ The values used for JWT can be used in the initial setup. You can modify them based on your security requirements.

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

➜ An application password has been created using a real email address for the project. This can be used directly, or you can create an application password with your own email and modify this configuration accordingly.

➜ You can replace the SenderEmail and SenderPassword fields with your Gmail address and application password.

➜ To send emails via Gmail, two-factor authentication and an application password are required.

🧠 Redis

```json
"Redis": {
  "ConnectionString": "localhost:1453"
}
```

➜ You can use Docker for Redis. Example command:

```bash
docker run -d -p 1453:6379 --name redis redis
```

➜ Adjust the ConnectionString value according to the container port.

🐰 RabbitMQ

```json
"RabbitMQ": {
  "Host": "localhost",
  "Port": 5672,
  "Username": "guest", 
  "Password": "guest"
}
```

➜ You can also use Docker for RabbitMQ. Example command:

```bash
docker run -d --hostname my-rabbit --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

➜ Access the interface at: http://localhost:15672 (Username/Password: guest/guest)

➜ Update the Host, Port, Username, and Password values according to your setup.

### 3️⃣ Run the migration to create the database:

➜ Through .NET CLI

```bash
dotnet ef database update --project Infrastructure/ExpenseTrackingSystem.Persistence --startup-project Presentation/ExpenseTrackingSystem.API
```

This command will create the database tables and add user and role information along with the seed data from the InitialMigration file.

➜ Through Visual Studio

![image](https://github.com/user-attachments/assets/b678d269-3071-4e02-bcab-79cfca8bb1d5)

-- Open the Package Manager Console.

-- Select the Infrastructure.Persistence layer as the Default Project.

![image](https://github.com/user-attachments/assets/2ea9c145-42e1-4584-a231-b64380951526)

-- Make sure the startup project is ExpenseTrackingSystem.API.

-- Run the following command:

```bash
update-database
```

This process will also create the database and initial data in the same way.

### 4️⃣ Run the application:

```bash
dotnet run --project Presentation/ExpenseTrackingSystem.API --launch-profile https
```

Then, open your browser and go to:

👉 https://localhost:7136/swagger

This will open the Swagger UI for the API.

Alternatively, you can run it through Visual Studio or Rider.

### 5️⃣ Test it through Swagger:

Once the application is running, you can test the API endpoints through the Swagger page that opens.

## 📁 Layer Structure (Onion Architecture)

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

# 🧩 Domain Layer

This layer contains the core entities. It operates completely isolated from external factors such as data access, API, and UI.

📚 NuGet Packages

Microsoft.AspNetCore.Identity.EntityFrameworkCore

## ✳️ Entities

💠 AppUser

Represents an application user. It is derived from the Microsoft.AspNetCore.Identity.IdentityUser class and has additional fields as follows:

| Property                      | Description                                    |
|-------------------------------|------------------------------------------------|
| Id                            | Identity-String                                |
| FullName                      | User's full name                               |
| Title                         | Job title within the company                   |
| IBAN                          | User's IBAN                                    |
| IsActive                      | User's active status                           |
| CreatedDate                   | Date the user was added                        |
| UpdateDate	                | Date the user was updated                      |
| RefreshToken                  | Token refresh (additional security)            |
| RefreshTokenEndDate           | Expiry date of the refresh token               |
| ICollection<Expense> Expenses | One-to-many relationship for employee expenses |

💠 AppRole

The roles are derived from the IdentityRole class. Roles such as Admin, Employee, etc., that can be assigned to users are defined here.

💠 Expense

Represents expenses associated with users.

| Property                             | Description                                       |
|--------------------------------------|---------------------------------------------------|
| Id                                   | Guid                                              |
| Amount                               | The amount of the expense                         |
| Date                                 | The date the expense request was created          |
| Location                             | The location of the expense request               |
| RejectionReason                      | Reason for rejection, if the expense was rejected |
| ReceiptFilePath	               | File path for receipts, etc.                      |
| UserId, AppUser User                 | The user who created the expense                  |
| ExpenseCategory Category, CategoryId | Expense category relationship                     |
| ExpenseStatus Status                 | Expense status (Pending, Approved, Rejected)      |

```csharp
public enum ExpenseStatus
{
	Pending = 1,
	Approved,
	Rejected
}
```

💠 ExpenseCategory

Stores the categories of expenses.

| Property                      | Description                                                |
|-------------------------------|------------------------------------------------------------|
| Id                            | int                                                        |
| Name                          | Category Name                                              |
| ICollection<Expense> Expenses | A single category can be associated with multiple expenses |

💠 PaymentSimulation

Contains payment simulations by the admin.

| Property          | Description             |
|-------------------|-------------------------|
| Id                | Guid                    |
| PaymentDate       | Payment date            |
| BankReferenceNo   | Transaction number      |
| PaidAmount        | Amount paid             |
| SenderFullName    | Name of the sender      |
| SenderIban	    | IBAN of the sender      |
| ReceiverFullName  | Name of the receiver    |
| ReceiverIban      | IBAN of the receiver    |
| Expense Expense   | Related expense details |

💠 AuditLog

Used for logging actions performed within the application.

| Property    | Description                                |
|-------------|--------------------------------------------|
| Id          | Guid                                       |
| UserId      | ID of the user who performed the action    |
| Action      | Type of action performed                   |
| Entity      | Entity on which the action was performed   |
| EntityId    | ID of the entity on which action was taken |
| ActionDate  | Date and time of the action                |

# 🧩 Application Layer

The Application layer contains business logic and data processing operations. It utilizes the entities from the Domain layer to provide services externally, enabling interaction with other layers such as the UI or API.

📚 NuGet Packages

AutoMapper.Extensions.Microsoft.DependencyInjection

FluentValidation.AspNetCore

FluentValidation.DependencyInjectionExtensions

MediatR

## ✳️ Abstractions

The interfaces under the Services and Token folders within Abstractions are abstractions of the services provided by the application to the outside world.

### Services

This is where the service interfaces that implement the business logic are located.

💠 IAuditLogService

```csharp
public interface IAuditLogService
{
	Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(string userId = null, DateTime? startDate = null, DateTime? endDate = null);
	Task LogActionAsync(string userId, string action, string entity, string entityId);
}
```

| Method            | Description                                                                                     |
|-------------------|-------------------------------------------------------------------------------------------------|
| GetAuditLogsAsync | Retrieves the audit logs from the database                                                      |
| LogActionAsync    | Creates and saves a new audit log entry when an action is performed (e.g., create a new expense)|

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

| Method                 | Description                                                                |
|------------------------|----------------------------------------------------------------------------|
| VerifyResetTokenAsync  | Used to validate the token provided for the password reset process         |
| LoginAsync             | Handles the login process using the user's email and password              |
| RefreshTokenLoginAsync | Allows obtaining a new access token using an existing refresh token        |
| LoginAsync             | Initiates a password reset request to allow users to reset their passwords |

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

| Method       | Description                     |
|--------------|---------------------------------|
| CreateAsync  | Creates a new category          |
| UpdateAsync  | Updates an existing category    |
| DeleteAsync  | Deletes a category              |
| GetByIdAsync | Retrieves a category by its Id  |
| GetAllAsync  | Lists all categories            |

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

| Method              | Description                           |
|---------------------|---------------------------------------|
| GetAllAsync         | Lists all expenses                    |
| GetByIdAsync        | Gets an expense by its Id             |
| CreateAsync         | Creates a new expense                 |
| UpdateStatusAsync   | Updates the status of an expense      |
| DeleteAsync         | Deletes an expense                    |
| GetByStatusAsync    | Lists expenses based on their status  |
| GetByUserIdAsync    | Lists expenses by user Id             |
| GetByFullNameAsync  | Lists expenses by user's full name    |
| GetByCategoryAsync  | Lists expenses by category name       |

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

| Method                           | Description                                                                   |
|----------------------------------|-------------------------------------------------------------------------------|
| SendMailAsync                    | Sends an email                                                                |
| SendPasswordResetMailAsync       | Sends a password reset email                                                  |
| SendExpenseStatusUpdateMailAsync | Sends an email to the expense owner when the admin updates the expense status |
| SendExpenseCreatedMailAsync      | Sends an email to the admins when a staff member creates an expense           |

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

| Method                         | Description                                                                                             |
|--------------------------------|---------------------------------------------------------------------------------------------------------|
| GetEmployeeRequestsAsync       | Reports the employee's own transaction activities                                                       |
| GetCompanyPaymentDensityAsync  | Reports the company's daily, weekly, and monthly payment density                                        |
| GetEmployeeExpenseDensityAsync | Reports the company's employee-based daily, weekly, and monthly spending density                        |
| GetExpenseApprovalStatusAsync  | Reports the approved and rejected expense amounts for the company on a daily, weekly, and monthly basis |

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

| Method                   | Description                                                |
|--------------------------|------------------------------------------------------------|
| CreateAsync              | Create a new user                                          |
| GetUserByIdAsync         | Get a user by Id                                           |
| GetAllUsersAsync         | List all users                                             |
| UpdatePasswordAsync      | Update the user's password                                 |
| DeleteUserAsync          | Soft delete a user                                         |
| AssignRoleToUserAsnyc    | Assign a role to a user                                    |
| GetUsersByRoleAsync      | Get users by their role                                    |
| UpdateRefreshTokenAsync  | Update the refresh token and its expiration time for a use |
| UpdateUserByTitleAsync   | Update the user's title                                    |
| UpdateUserIbanAsync      | Update the user's IBAN                                     |

### Token

💠 ITokenService

```csharp
public interface ITokenService
{
	TokenDto CreateAccessToken(int second, AppUser appUser);
	string CreateRefreshToken();
}
```

| Method              | Description              |
|---------------------|--------------------------|
| CreateAccessToken   | Create an Access Token   |
| CreateRefreshToken  | Create a Refresh Token   |

## ✳️ Dtos

➜ The application includes Dto's for AuditLog, Expense, ExpenseCategory, Mail, PaymentSimulation, Report, Token, and User to handle operations like validation and mapping.

## ✳️ Features

➜ Each feature in the application is grouped into its own folder and structured according to CQRS (Command Query Responsibility Segregation) and MediatR architecture.

➜ The Command folder contains actions that modify data (e.g., create a user).

➜ The Query folder handles data retrieval operations (e.g., listing users).

➜ For each operation, there are related Request, Response, and Handler classes. Services are used within the Handlers.

This structure enhances code readability, decouples functionalities, and makes testing easier.

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
│       ├── UpdateUserByIban/
│       │   └── UpdateUserByIbanCommand(Request, Response, Handler)
│       ├── UpdateUserByTitle/
│       │   └── UpdateUserByTitleCommand(Request, Response, Handler)
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

### 🔎 Command ve Query Review

To understand the structure, let's look at CreateExpenseCommand and GetAllExpensesQuery.

#### CreateExpenseCommand

The CreateExpenseCommand is designed to create a new expense record in the system.

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

🔺 In the MediatR pattern, the IRequest<T> interface is used to define a request for which a response will be returned. The properties required to create expenses have been added.

##### CreateExpenseCommandResponse

```csharp
public class CreateExpenseCommandResponse
{
	public bool Success { get; set; }
	public string Message { get; set; }
}
```

🔺 After the expense is created, the properties Success (indicating whether the expense was successfully created) and Message (containing the success message) are stored.

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

🔺 The IRequestHandler<> interface from MediatR is used, and the incoming CreateExpenseCommandRequest is processed in the Handle class, returning a CreateExpenseCommandResponse.

#### GetAllExpensesQuery

It has been created using MediatR to get all expenses.

##### GetAllExpensesQueryRequest

```csharp
public class GetAllExpensesQueryRequest : IRequest<List<GetAllExpensesQueryResponse>>
{
}
```

🔺 Using the IRequest interface from MediatR, it returns the response in the form of a list.

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

🔺 The response includes the expense details.

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

🔺 Using the IRequestHandler<> interface from MediatR, the GetAllExpensesQueryRequest is processed in the handler class, and the response returns a GetAllExpensesQueryResponse (List).

## ✳️ Helpers

Helper classes are stored here.

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

🔺 The CustomEncoders class defines extension methods specific to the string type. With these methods, you can encode a string into Base64 URL format and decode the encrypted value back to its original form.

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

🔺 This class is used to save a file (such as an invoice image) received from the user to the wwwroot/receipts folder on the server and return the file path.

## ✳️ Mapper

Here, all mapping operations are performed within the MapperConfig class using AutoMapper.

## ✳️ Repositories

The Repository Design Pattern has been applied, and base IReadRepository and IWriteRepository interfaces have been created. In this structure, there are Expense, ExpenseCategory, and Payment folders, each containing interfaces that implement the IReadRepository and IWriteRepository interfaces.

### 📚 IReadRepository

This interface defines read-only operations for a generic entity.

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

| Method          | Description                                                                                                                    |
|-----------------|--------------------------------------------------------------------------------------------------------------------------------|
| GetAllAsync     | Retrieves all records from the database. The tracking parameter allows enabling or disabling EF Core's change tracker feature. |
| GetWhere        | Retrieves records that match a specific condition (predicate)                                                                  |
| GetSingleAsync  | Retrieves a single entity that matches a specific condition.                                                                   |
| GetByIdAsync    | Retrieves the entity with the given ID.                                                                                        |
| DbSet<T> Table  | Provides direct access to the DbSet<T> in EF Core.                                                                             |

### ✍ IWriteRepository

This interface defines write operations for a generic entity. 

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

| Method           | Description                                                                                               |
|------------------|-----------------------------------------------------------------------------------------------------------|
| AddAsync         | Adds a single entity to the database.                                                                     |
| AddRangeAsync    | Adds multiple entities at once (bulk insert).                                                             |
| RemoveAsync      | Deletes the entity with the given ID from the database.                                                   |
| RemoveRangeAsync | Deletes multiple entities with given IDs in bulk.                                                         |
| UpdateAsync      | Updates the given entity.                                                                                 |
| SaveChangesAsync | Triggers EF Core's DbContext.SaveChangesAsync() function, saving all changes to the database permanently. |

## ✳️ Validators

Here, validation operations for each entity have been performed using FluentValidation.

For example, the ExpenseValidator:

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

Since Onion Architecture layered architecture is applied, a ServiceRegistration class has been created for the configuration of each layer. The purpose here is to simplify the Program.cs.

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

🔺  In this class, FluentValidation, Mapping, and MediatR configurations are set up.

# 🧩 Infrastructure/Persistence Layer

This layer is responsible for data access operations in an application. It typically works with ORM tools like Entity Framework, Dapper, etc., and manages the connection between the application and its database.

📚 NuGet Packages

Dapper

Microsoft.AspNetCore.Authentication.JwtBearer

Microsoft.EntityFrameworkCore.Design

Microsoft.EntityFrameworkCore.SqlServer

Microsoft.EntityFrameworkCore.Tools

StackExchange.Redis

## ✳️ Context

Contains the main structures that manage the database connection and related tables.

### ✳️ AppDbContext

This class is the main class where the relationship with the project's database is defined. It inherits from the IdentityDbContext class, enabling identity management with AppUser and AppRole.

### ✳️ AppDbContextFactory

This class is used for design-time scenarios. It is created to run the DbContext object during EF Core Migrations. Before the application starts, it reads the connection information from appsettings.json and creates an instance of AppDbContext. Commands like "dotnet ef migrations add" and "dotnet ef database update" use this class to determine the database they will connect to.

## ✳️ Migrations

Here, the InitialMigration file is located. Inside the InitialMigration, when "update-database" is executed, it adds 2 default admins, 2 roles (Admin and Employee), and 3 categories to the database.

Admin Information in InitialMigration

| Property         | Admin 1                     | Admin 2                     |
|------------------|-----------------------------|-----------------------------|
| UserName         | gizemadmin                  | gunesadmin                  |
| Email            | 0.gizemgunes@gmail.com      | admin@example.com           |
| Password         | Admin123.                   | Admin234.                   |
| FullName         | Gizem Admin                 | Gunes Admin                 |
| Title            | System Admin                | System Admin                |
| IBAN             | TR12345123451234512345123   | TR12345123451234512345124   |
| PhoneNumber      | 5000000000                  | 5000000001                  |

For the MailService, the real email account of Admin 1 has been used. This will be further explained in the appsettings.json file. The other information is set to default values.

Three default categories have been added as follows: Yol, Yemek, Konaklama

## ✳️ Repositories

The Repositories folder within the Application layer contains interfaces for the purpose of adhering to the Dependency Inversion Principle. The concrete implementations of these interfaces are located here.

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

The IReadRepository has been implemented using AppDbContext, and its methods have been populated accordingly.

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

The IWriteRepository has been implemented using AppDbContext, and its methods have been populated accordingly. 

The same operations have been performed for Expense, ExpenseCategory, and Payment entities as well.

For example:

### 💲 ExpenseReadRepository

```csharp
public class ExpenseReadRepository : ReadRepository<Expense, Guid>, IExpenseReadRepository
{
	public ExpenseReadRepository(AppDbContext context) : base(context) { }
}
```

This class is a specialized repository for reading Expense data. The ExpenseReadRepository class inherits from ReadRepository<Expense, Guid> and implements the IExpenseReadRepository interface.

## ✳️ Services

Here, services that implement interface services from the Application layer are present. Dependency Inversion and Interface Segregation have been applied to reduce dependencies.

### ⭐ AuditLogService

This service class uses Dapper, Logging, and Redis (to store GetAuditLogsAsync in cache). IAuditLogService is implemented, and the GetAuditLogsAsync and LogActionAsync methods are defined.

### ⭐ AuthService

In this service, IConfiguration, UserManager<AppUser>, ITokenService, SignInManager<AppUser>, IUserService, IMailService, and IAuditLogService are used to perform Authentication operations. IAuthService is implemented.

| Method                 | Description                                                                |
|------------------------|----------------------------------------------------------------------------|
| VerifyResetTokenAsync  | Used to validate the token provided for the password reset process         |
| LoginAsync             | Handles the login process using the user's email and password              |
| RefreshTokenLoginAsync | Allows obtaining a new access token using an existing refresh token        |
| LoginAsync             | Initiates a password reset request to allow users to reset their passwords |

### ⭐ ExpenseCategoryService

This service class implements IExpenseCategoryService, and uses IExpenseCategoryReadRepository, IExpenseCategoryWriteRepository, and IMapper to implement the methods GetByIdAsync, GetAllAsync, CreateAsync, UpdateAsync, and DeleteAsync.

### ⭐ ExpenseService

This service class implements IExpenseService and handles operations related to expenses.

#### 🔎 Review of CreateAsync and UpdateStatusAsync Methods in the ExpenseService Class

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
🔺 This method creates an expense using ExpenseCreateDto and returns an expense. First of all, it detects the user with the UserManager coming from the Identity according to the UserId in the expenseCreateDto.

🔺 The second step, find the category by Id using the GetByIdAsync method in the _expenseCategoryReadRepository(with dependency injection) created from IExpenseCategoryReadRepository.

🔺 Next, it retrieves the file path of the invoice uploaded by the user using the FileHelper method from the Application layer.

🔺 After completing all these steps, the created expense is saved to the database using _expenseWriteRepository from IExpenseWriteRepository.

🔺 The created record is then logged in the Audit log using _auditLogService from IAuditLogService.

🔺 Finally, an email is sent to the Admin with the created expense details via the SendExpenseCreatedMailAsync method.

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

🔺 This method first finds the expense by its Id using GetByIdAsync from IExpenseReadRepository.

🔺 The ValidateRejectionReason method checks if a rejection reason (RejectionReason) is provided when the expense is rejected (Status = Rejected).

🔺 The UpdateExpenseStatusAsync method is used to update the status of the expense.

🔺 If the expense is approved, the CreatePaymentSimulationAsync method simulates the payment process. This method makes a payment to the employee who created the expense, as performed by the Admin.

### ⭐ ReportService

This service class implements the IReportService interface and utilizes Dapper and Redis (for caching during reporting).

### ⭐ TokenService

The ITokenService interface is implemented, and the methods CreateAccessToken and CreateRefreshToken are defined.

### ⭐ UserService

This class implements the IUserService interface. The following methods are implemented:

AssignRoleToUserAsync, CreateAsync, DeleteUserAsync (soft delete), GetAllUsersAsync, GetUserByIdAsync, GetUsersByRoleAsync, UpdatePasswordAsync, UpdateUserByTitleAsync, UpdateUserIbanAsync and UpdateRefreshTokenAsync.

Additionally, there are two private helper methods created within the class.

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

🔺 The GenerateValidUsername method is created to automatically generate a UserName when a user is created.

🔺 RoleConstants is a static class created to centrally and consistently define the roles used in the system.

## ✳️ ServiceRegistration.cs

It ensures the injection of services and components from the Persistence layer into the application via Dependency Injection. Redis connection, DbContext connection, Identity configurations, and all service and repository injections are handled here.

# 🧩 Infrastructure/Infrastructure Layer

It contains the infrastructure services that enable the application's integration with external systems (such as email services, message queue systems, etc.).

📚 NuGet Package

Hangfire.AspNetCore

Hangfire.Core

Hangfire.Redis.StackExchange

RabbitMQ.Client

## ✳️ Services

Here, the service classes such as MailService, RabbitMqService, and RabbitMqBackgroundService are present.

### ✳️ MailService

This service class implements the IMailService method and performs email operations using RabbitMqService.

### ✳️ RabbitMqService

A service class that handles message sending (publish) and message listening (consume) operations with RabbitMQ. It implements the IDisposable interface and contains three methods.

| Method          | Description                                                                              |
|-----------------|------------------------------------------------------------------------------------------|
| PublishMessage  | Sends a message to the specified queue name in RabbitMQ.                                 |
| ListenToQueue   | Listens to the specified queue.                                                          |
| Dispose         | Properly closes connections when the application shuts down or the service is disposed.  |

### ✳️ RabbitMqBackgroundService

This is a background service definition that listens to the RabbitMQ queue and uses the MailService to send email requests. The service class uses HangFire to execute jobs in the background. It contains the ExecuteAsync method, which listens to the emailQueue RabbitMQ queue. When a message is received, it is deserialized from JSON format into a MailRequest object. If there is a mail request, the email sending is queued in the background using the HangFire library.

## ✳️ ServiceRegistration.cs

This ensures the integration of services in the Infrastructure layer into the project via Dependency Injection. It includes configurations and injections for Hangfire, RabbitMQ, and MailService.

# 🧩 Presentation/API Layer

his layer contains the API that handles incoming HTTP requests from the client. It serves as the communication point between the application and the outside world.

📚 NuGet Package

Serilog

Serilog.AspNetCore

Serilog.Sinks.MSSqlServer

Microsoft.EntityFrameworkCore.Design

## ✳️ Controllers

It contains the API Controller classes. The AuthsController, ExpenseCategoriesController, ExpensesController, ReportController, and UsersController are located here.

🔎 Controller Review

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

🔺 This API Controller creates API endpoints using MediatR.

🔺 Some endpoints are restricted by the [Authorize(Roles = "Admin")] attribute, which limits access to users with the "Admin" role.

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

🔺 This is a middleware configuration class used to set up a custom global exception handling mechanism in the application.

🔺 When an error occurs, this method sets the response status code to 500 Internal Server Error and sets the response type to application/json. 

🔺 Details of the error can be accessed through IExceptionHandlerFeature. The error message is logged, and the error details are returned to the client in JSON format.

🔺 This setup provides a user-friendly error message to the client.

## ✳️ appsettings.json

Here, the configuration settings for the database connection (ConnectionStrings), Token, Mail, Redis, and RabbitMQ are defined.

### ✳️ ConnectionStrings

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Initial Catalog=ExpenseTrackingSystemDb;Trusted_Connection=True;TrustServerCertificate=true;"
}
```

🔺 The necessary ConnectionStrings are configured. The setup details are mentioned in the installation section.

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

🔺 A real email and application password have been created and used for the application. It can be configured as desired.


### ✳️ Redis

```json
"Redis": {
  "ConnectionString": "localhost:1453"
}
```

🔺 The necessary configuration for Redis is done here. It has been mentioned in the installation section.

### ✳️ RabbitMQ

```json
"RabbitMQ": {
  "Host": "localhost",
  "Port": 5672,
  "Username": "guest", 
  "Password": "guest"
}
```

🔺 The necessary configuration for RabbitMQ is done here. It is mentioned in the installation section.

## ✳️ Program.cs

This file is the entry point of the application and is where all the services, middleware, and configurations are set up. It ensures that necessary services and configurations are injected into the application during startup.

### 🧰 Services

```csharp
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
```

🔺 In a layered architecture, services from different layers (Application, Persistence, Infrastructure) are added to the Dependency Injection (DI) system to ensure proper separation of concerns and maintainability. 

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

🔺 With Serilog, logs are written to the console, to a file (logs/log.txt), and to the SQL Server database. A table named Logs is automatically created for logs in SQL.

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

🔺 With Swagger, all API endpoints of the application are automatically documented. For JWT authentication, the Swagger interface allows you to enter tokens.

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

🔺 The application ensures security using JWT (JSON Web Tokens). 

🔺 Values such as Token:Audience, Token:Issuer, and Token:SecurityKey are retrieved from the appsettings.json file.

🔺 The validity of the token (such as expiration time, signature, and intended user) is verified.

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

🔺 Error handling is configured through a custom middleware.

🔺 The Hangfire interface is accessible via the /hangfire route.

🔺 Authentication and Authorization are applied sequentially.

# 🧩 Test 

In this project, the xUnit test framework is used to verify the correctness of business layers. Tests are located in a separate "Test" layer, and unit tests check the expected behavior of services.

## ✳️ AuthServiceTests

This test class ensures that when a valid username and password are provided, the AuthService.LoginAsync method returns a valid JWT access token and refresh token.

## ✳️ ExpenseServiceTests

This test class checks if the expense creation process via the ExpenseService occurs correctly and as expected.

## ✳️ MailServiceTests

This test class ensures that the MailService component correctly sends email notifications to administrators via the RabbitMQ queue when an expense is created.

## ✳️ UserServiceTests

This test class is written to test whether the CreateAsync method in UserService behaves correctly during the user creation process, either succeeding or failing as expected.

# 🎯 API Endpoints

## ✳️ Swagger

### 🤖 Auths

#### 🟢 POST api/Auths/Login

This endpoint logs the user in with email and password and generates a token. This token will be used in many endpoints for the admin. Please make sure that you enter the user email and password that exist in the database.

#### 🟢 POST api/Auths/RefreshTokenLogin

Allows obtaining a new access token using an existing refresh token. The refresh token created in the AspNetUsers table of the logged in user can be used for testing.

#### 🟢 POST api/Auths/verify-reset-token

This enpoint resets token provides verification. Please make sure that you have entered the reset token and correct userId received in the mail.

#### 🟢 POST api/Auths/password-reset

This endpoint sends a Reset Token to the email address you entered, sent by 0.gizemgunes@gmail.com (or another email address if you have set it in appsettings.json). Please make sure that the email settings in the appsettings.json file are correct and that the email address you are requesting is in the database.

### 🔐 Authorization for Admin-Only Endpoints
Some of the endpoints in this API are restricted to users with the Admin role. In order to access these endpoints via the Swagger UI, you must first authenticate and provide a valid JWT access token.

Open the Swagger UI in your browser.

In the top-right corner, click on the Authorize button (🔐 icon).

Navigate to the POST /api/Auths/Login endpoint and send a valid login request using Admin credentials. Copy the token.

Enter the jwt token here and authorize:

![Ekran görüntüsü 2025-05-04 004631](https://github.com/user-attachments/assets/34de8c0a-68f7-4271-b9c8-5973d5108eef)

### 📚 ExpenseCategories

#### 🔵 GET api/ExpenseCategories

This endpoint gets all categories. Please make sure to enter the Admin token.

#### 🟢 POST api/ExpenseCategories

This endpoint creates a category. Only admins can create categories. Please make sure to enter the Admin token.

#### 🟠 PUT api/ExpenseCategories

This endpoint updates the category. Only admins can update the category. Please make sure to enter the Admin token and make sure the category exists in the database.

#### 🔵 GET api/ExpenseCategories/{Id}

This endpoint gets category by id. Please make sure to enter the Admin token.

#### 🔴 DELETE api/ExpenseCategories/{Id}

This endpoint deletes a category based on its id. Please make sure to enter the Admin token and make sure the category exists in the database.

### 💵 Expenses

#### 🔵 GET api/Expenses

This endpoint gets all expenses. Please make sure to enter the Admin token.

#### 🟢 POST api/Expenses

This endpoint creates a new expense. Make sure that the UserId and CategoryId exist in the database.

#### 🟠 PUT api/Expenses

Only admin can update the expense status. So make sure to enter the Admin token. Make sure the Id is the existing ExpenseId. Status must be 2 or 3(Approved, Rejected). If expense is rejected, please make sure to enter rejectionReason.

#### 🔵 GET api/Expenses/{Id}

This endpoint returns expense information based on ExpenseId. 

#### 🔵 GET api/Expenses/by-status

This endpoint gets expenses according to the status. Status can be 1, 2 or 3.

#### 🔵 GET api/Expenses/by-userId

This endpoint gets expense information based on UserId. Please make sure that the Id you entered is the UserId that exists in the database.

#### 🔵 GET api/Expenses/by-full-name

This endpoint gets expenses according to the full name.

#### 🔵 GET api/Expenses/by-category-name

This endpoint geta the expenses according to the request category name. Please make sure that the category name you entered exists in the database.

#### 🔴 DELETE api/Expenses/Id

This endpoint deletes the Expense according to the entered ExpenseId. Only admin can do this. Please make sure to enter the Admin token and ExpenseId that exists in database.

### 👩🏻‍💻👨🏻‍💻 Users

#### 🔵 GET api/Users

This enpoint gets all users. Only admin can do this. Please make sure to enter the Admin token.

#### 🟢 POST api/Users

This endpoint creates a user. Only admin can do this. Please make sure to enter the Admin token. Password must be at least 4 characters. Required non alphanumeric, digit, lowercase and uppercase.

1- Email address must be in email format.

2- IBAN must start with TR and contain 23 numbers.

3- Phone number can be:

+905000000000

05000000000

5000000000

#### 🔵 GET api/Users/{Id}

This endpoint gets User by Id. Only admin can do this. Please make sure to enter the Admin token.

#### 🔴 DELETE api/Users/{Id}

This endpoint deletes User by Id. Make sure that the Id is the UserId that exists in the database. Only admin can do this. Please make sure to enter the Admin token.

#### 🔵 GET api/Users/role/{RoleName}

This endpoint gets Users by RoleName(Admin or Employee). Only admin can do this. Please make sure to enter the Admin token.

#### 🟢 POST api/Users/assign-role

This endpoint assigns the role of the existing user. Make sure that the Id entered is an existing UserId and the role is Admin or Employee. Only admin can do this. Please make sure to enter the Admin token.

#### 🟢 POST api/Users/update-password

This endpoint used for password update. The resetToken created after sending an email to an existing email address with the Auths/password-reset request is used here. Password must be at least 4 characters. Required non alphanumeric, digit, lowercase and uppercase. Make sure that password and passwordConfirm are the same. Only admin can do this. Please make sure to enter the Admin token.

#### 🟠 PUT api/Users/update-title

This endpoint used for title update. Only admin can do this. Please make sure to enter the Admin token.

#### 🟠 PUT api/Users/update-IBAN

This endpoint used for IBAN update. Only admin can do this. Please make sure to enter the Admin token.

### 📈 Report

#### 🔵 GET api/Report/personal-requests/{userId}

This endpoint reports the employee's own transaction activities.

#### 🔵 GET api/Report/company-payment-density

The endpoint reports the company's daily, weekly, and monthly payment density. Set the StartDate and EndDate as desired. ReportType can be daily weekly monthly. Only admin can do this. Please make sure to enter the Admin token.

#### 🔵 GET api/Report/employee-expense-density

The endpoint reports the company's employee-based daily, weekly, and monthly spending density. Set the StartDate and EndDate as desired. ReportType can be daily weekly monthly. Only admin can do this. Please make sure to enter the Admin token.

#### 🔵 GET api/Report/expense-approval-status

The endpoint reports the approved and rejected expense amounts for the company on a daily, weekly, and monthly basis. Set the StartDate and EndDate as desired. ReportType can be daily weekly monthly. Only admin can do this. Please make sure to enter the Admin token.

#### 🔵 GET api/Report/api/auditlogs

The endpoint retrieves the audit logs from the database. Make sure that the UserId is a UserId that exists in the database. Set the StartDate and EndDate as desired.

## 🟠 Postman

You can access the public Postman documentation of this API using the following link:

[Postman](https://documenter.getpostman.com/view/29631051/2sB2j689Yc)

If you'd like to use this collection in the Postman desktop application, follow these steps:

1- Open the public documentation link above.

2- Click on the "Run in Postman" button at the top right.

3- Choose "Postman Desktop App" when prompted.

4- The collection will be imported into your Postman application.

5- Set up the required environment variables (e.g., base URL, access token) if needed.

6- You're now ready to test the API directly from Postman!

## 📸 Some Screenshots

### Auths/Login

![Ekran görüntüsü 2025-05-05 000247](https://github.com/user-attachments/assets/405b9add-0b89-4acc-99a9-2489fc885db7)

### POST ExpenseCategories

![Ekran görüntüsü 2025-05-05 000334](https://github.com/user-attachments/assets/0c564b3c-7b97-4f48-b940-e2ec48600bc8)

### GET Expenses

![Ekran görüntüsü 2025-05-05 000345](https://github.com/user-attachments/assets/27fbcee9-e351-4bcc-99af-c3bb4041e9ce)

### POST Expenses

![Ekran görüntüsü 2025-05-05 000445](https://github.com/user-attachments/assets/e00fd940-5fbb-4fc0-ac8b-e397a3dca775)

![Ekran görüntüsü 2025-05-05 000452](https://github.com/user-attachments/assets/be783925-d6fc-428f-9556-0d52a641b086)

### PUT Expenses

![Ekran görüntüsü 2025-05-05 000525](https://github.com/user-attachments/assets/bcf6df29-620b-4d1f-accf-51d849fe1c71)

### GET Expenses/by-status

![Ekran görüntüsü 2025-05-05 000536](https://github.com/user-attachments/assets/0ff42c20-4838-46dd-9602-37bd52e747cc)

### GET Expenses/by-full-name

![Ekran görüntüsü 2025-05-05 000609](https://github.com/user-attachments/assets/d824c1cf-e994-46be-b2ab-b823061ab59d)

### POST Users

To demonstrate validations

![Ekran görüntüsü 2025-05-05 000650](https://github.com/user-attachments/assets/1ea752b2-6a3e-4dab-9a52-b6012c71c90e)

![Ekran görüntüsü 2025-05-05 000656](https://github.com/user-attachments/assets/6765465f-4bef-490f-8ed6-bad5139ef414)

### GET Users/role/{RoleName}

![Ekran görüntüsü 2025-05-05 000723](https://github.com/user-attachments/assets/55293f28-122d-453a-98b7-4f2fe4a23bdc)

### PUT Users/update-title

![Ekran görüntüsü 2025-05-05 000750](https://github.com/user-attachments/assets/eb55bbaf-5604-4e20-8383-89a7204306a8)

### GET Users

![Ekran görüntüsü 2025-05-05 000834](https://github.com/user-attachments/assets/29890af4-d7b5-4b21-88cf-e17f7cb7447e)

### GET Report/api/auditLogs

![Ekran görüntüsü 2025-05-05 000921](https://github.com/user-attachments/assets/385c5f0d-828e-4ffb-b335-532be0368011)

