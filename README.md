ASP.NET Core Sipariş Yönetim API
Bu proje, temel e-ticaret işlevlerini yerine getiren bir ASP.NET Core Web API'sidir. Proje; yeni sipariş ekleme, mevcut siparişleri listeleme, sipariş detayını getirme ve sipariş silme gibi temel CRUD (Create, Read, Update, Delete) operasyonlarını destekler.

Teknolojiler ve Mimari
Framework: ASP.NET Core 8

Dil: C#

Mimari: Temel Katmanlı Mimari (Presentation -> Business -> Data Access)

Veri Erişimi: Entity Framework Core 8

Veritabanı: In-Memory Database (Bellek İçi Veritabanı) 

Neden In-Memory Veritabanı Kullanıldı?
Bu projenin amacı, bir API'nin temel iş mantığını ve yapısını göstermektir. Bu bağlamda, In-Memory Database (Bellek İçi Veritabanı) kullanılmasının sebepleri şunlardır:

Kurulum Kolaylığı: Geliştiricinin bilgisayarına SQL Server, PostgreSQL gibi harici bir veritabanı sunucusu kurma zorunluluğunu ortadan kaldırır.

Hızlı Test: Proje her çalıştığında sıfırdan, temiz bir veritabanı ile başlar. Bu, test süreçlerini oldukça basitleştirir ve hızlandırır.

Bağımlılıkları Azaltma: Proje, ek bir servis veya konfigürasyon (connection string vb.) gerektirmeden dotnet run komutuyla doğrudan çalıştırılabilir.

Not: In-Memory veritabanı, verileri kalıcı olarak saklamaz. Uygulama her durdurulup başlatıldığında veriler sıfırlanır. Bu yüzden sadece geliştirme ve test aşamaları için uygundur.

Projeyi Çalıştırma
Bu projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyebilirsiniz.

Ön Gereksinimler
.NET 8 SDK veya daha güncel bir sürüm.

Git.

Kurulum Adımları
Projeyi Klonlayın:
Bu depoyu bilgisayarınıza klonlayın.

git clone <BU_PROJENIN_GITHUB_URL_ADRESI>

Proje Dizinine Gidin:

cd SiparisYonetimAPI

Bağımlılıkları Yükleyin:
Gerekli NuGet paketlerini yükleyin. (Genellikle dotnet run bu adımı otomatik olarak yapar.)

dotnet restore

Uygulamayı Çalıştırın:
Projeyi başlatmak için aşağıdaki komutu kullanın.

dotnet run

Uygulama başladığında, terminalde https://localhost:XXXX benzeri bir adres göreceksiniz.

API'yi Test Edin:
Tarayıcınızda https://localhost:XXXX/swagger adresine giderek Swagger arayüzüne ulaşabilirsiniz. Bu arayüz üzerinden tüm API endpoint'lerini interaktif olarak test edebilirsiniz.
