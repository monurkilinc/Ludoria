# Ludoria

Ludoria, oyun veritabanı ve yönetim sistemi olarak tasarlanmış bir web uygulamasıdır. Bu proje, kullanıcılara kapsamlı bir oyun koleksiyonu yönetim aracı sunmayı amaçlamaktadır.

## Özellikler

- Oyun ekleme, düzenleme ve silme işlevselliği
- Kategori bazlı oyun sınıflandırma sistemi
- Platform bilgileri yönetimi
- Oyun görsellerini yükleme ve görüntüleme imkanı
- Kullanıcı dostu arayüz tasarımı

## Teknoloji Altyapısı

- ASP.NET Core MVC
- Entity Framework Core
- C#
- HTML/CSS
- JavaScript

## Sistem Gereksinimleri

Projenin çalıştırılabilmesi için aşağıdaki yazılımların sisteminizde kurulu olması gerekmektedir:

- .NET 6.0 SDK veya daha yeni bir sürüm
- Visual Studio 2022 veya Visual Studio Code
- SQL Server (LocalDB veya tam sürüm)

## Kurulum ve Çalıştırma Talimatları

1. Projeyi klonlayın:
   ```
   git clone https://github.com/monurkilinc/Ludoria.git
   ```

2. Proje dizinine gidin:
   ```
   cd Ludoria
   ```

3. Gerekli bağımlılıkları yükleyin:
   ```
   dotnet restore
   ```

4. `appsettings.json` dosyasını açın ve veritabanı bağlantı dizesini konfigüre edin:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Ludoria;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

5. Veritabanını oluşturun:
   ```
   dotnet ef database update
   ```

6. Uygulamayı derleyin:
   ```
   dotnet build
   ```

7. Uygulamayı çalıştırın:
   ```
   dotnet run
   ```

8. Web tarayıcınızda `https://localhost:5001` veya `http://localhost:5000` adresine giderek uygulamayı görüntüleyin.

## Visual Studio ile Geliştirme

1. `Ludoria.sln` dosyasını Visual Studio ile açın.
2. NuGet Paket Yöneticisi Konsolu'nu açın ve aşağıdaki komutu çalıştırın:
   ```
   Update-Database
   ```
3. F5 tuşuna basarak veya "IIS Express" seçeneği ile uygulamayı başlatın.

## Hata Giderme

- "The SDK 'Microsoft.NET.Sdk.Web' specified could not be found" hatası alınması durumunda, .NET Core SDK'nın doğru şekilde yüklendiğinden emin olun.
- Veritabanı bağlantı hatası alınması durumunda, `appsettings.json` dosyasındaki bağlantı dizesini kontrol edin.
- Migrasyon hataları için, öncelikle `migrations` klasörünü silin, ardından aşağıdaki komutları sırasıyla çalıştırın:
  
  dotnet ef migrations add InitialCreate
  dotnet ef database update

## Katkıda Bulunma

1. Projeyi fork edin.
2. Yeni bir özellik branch'i oluşturun (`git checkout -b feature/YeniOzellik`).
3. Değişikliklerinizi commit edin (`git commit -m 'Yeni özellik eklendi'`).
4. Branch'inizi push edin (`git push origin feature/YeniOzellik`).
5. Bir Pull Request oluşturun.

## İletişim

M.Onur Kılınç - [GitHub](https://github.com/monurkilinc)
                [LinkedIn](https://www.linkedin.com/in/monurkilinc00)
                
Proje Bağlantısı:(https://github.com/monurkilinc/Ludoria)

Herhangi bir soru, öneri veya geri bildiriminiz için lütfen iletişime geçmekten çekinmeyin.
