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



