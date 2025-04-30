# ExpenseTrackingSystem

Bu proje, şirket özelinde sahada çalışan personel için masraf kalemlerinin takibi ve yönetimi için oluşturulmuştur. Kullanıcıların masraflarını kategori bazlı takip edebildiği, rollerin ayrıldığı ve güvenli bir API altyapısı sunan bir masraf takip sistemidir. Onion Architecture kullanılarak katmanlı ve sürdürülebilir bir yapı kurulmuştur.

Proje Detayları

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

| Mimari	             | Onion Architecture                  |
| Design Pattern	     | Repository Design Pattern           |

Onion Architecture daha detaylı bilgi için Medium yazım
https://medium.com/@0.gizemgunes/onion-architecture-nedir-ve-yazılımda-nasıl-kullanılır-c77a4a8cf18f

## 🛠️ Kurulum

Projeyi çalıştırmak için aşağıdaki adımları izleyin:

1- Projeyi klonlayın:

git clone https://github.com/GizemG6/ExpenseTrackingSystem.git
cd ExpenseTrackingSystem

2- appsettings.json dosyasını yapılandırın:

Presentation/ExpenseTrackingSystem.API/appsettings.json içindeki bağlantı ve servis bilgilerini kendinize göre güncelleyin:

ConnectionStrings:SqlServer

Redis

RabbitMQ

JWT Ayarları

3- Migration'ı çalıştırarak veritabanını oluşturun:

dotnet ef database update --startup-project Persistence/ExpenseTrackingSystem.API

4- Uygulamayı başlatın:

dotnet run --project Presentation/ExpenseTrackingSystem.API

5- Swagger üzerinden test edin:

Uygulama çalıştığında açılan Swagger sayfası üzerinden API endpointlerini test edebilirsiniz.

