# userIdentity ve Entity Framework ile C# Örnek Projesi

Bu proje, `userIdentity` kütüphanesi ve `Entity Framework` kullanarak kullanıcı kimlik doğrulama ve yönetimi işlemlerini gerçekleştiren bir C# uygulamasıdır. Proje, temel işlevler olarak kullanıcı kayıt olma, giriş yapma ve kimlik doğrulama süreçlerini içerir.

## Özellikler
- Kullanıcı kayıt sistemi
- Giriş yapma ve kimlik doğrulama
- Kullanıcı bilgilerini güncelleme
- Güvenli oturum yönetimi

## Gereksinimler
Bu projeyi çalıştırmak için aşağıdaki yazılımların sisteminizde kurulu olması gerekir:

- **.NET 6 veya üzeri**
- Visual Studio 2022 veya Rider
- SQL Server (veya başka bir desteklenen veritabanı)

## Kurulum
1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/kullaniciadi/userIdentity-entityframework.git
   cd userIdentity-entityframework
   ```

2. Veritabanını oluşturmak için `Entity Framework` Migration'ları uygulayın:
   ```bash
   dotnet ef database update
   ```

3. Gerekli yapılandırma ayarlarını yapın (örneğin, `appsettings.json`):
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=UserIdentityDb;Trusted_Connection=True;"
     },
     "Jwt": {
       "Key": "your-secret-key",
       "Issuer": "your-app",
       "Audience": "your-app-users"
     }
   }
   ```

## Kullanım
1. Uygulamayı başlatın:
   ```bash
   dotnet run
   ```

2. Tarayıcınızdan API uç noktalarına erişin:
   ```
   https://localhost:5001/swagger
   ```

3. Kullanıcı işlemlerini gerçekleştirin:
   - **Kayıt Ol:** Yeni bir kullanıcı oluşturmak için `/api/auth/register` uç noktasını kullanın.
   - **Giriş Yap:** Giriş yapmak için `/api/auth/login` uç noktasını kullanın.
   - **Profil Güncelle:** Kullanıcı bilgilerini güncellemek için `/api/users/update` uç noktasını kullanın.

## Klasör Yapısı
```
userIdentity-entityframework/
├── Controllers/       # API denetleyicileri
├── Data/              # Veritabanı bağlamı ve yapılandırmalar
├── Models/            # Veri modelleri
├── Services/          # İş mantığı ve servisler
├── appsettings.json   # Uygulama ayarları
├── Program.cs         # Ana giriş noktası
├── Startup.cs         # Uygulama yapılandırması
├── README.md          # Proje açıklaması
```

## Örnek Kod
`userIdentity` ve `Entity Framework` ile kullanıcı oluşturma işlemi aşağıdaki gibi yapılabilir:

```csharp
using UserIdentity.Services;
using UserIdentity.Models;

// Kullanıcı servisini DI (Dependency Injection) ile alın
var userService = serviceProvider.GetRequiredService<IUserService>();

// Yeni bir kullanıcı kaydı oluştur
var user = new User {
    Username = "kullanici",
    Email = "ornek@mail.com",
    Password = "Sifre123!"
};
await userService.RegisterAsync(user);

// Kullanıcı kimlik doğrulama
var isAuthenticated = await userService.AuthenticateAsync("kullanici", "Sifre123!");
Console.WriteLine($"Kimlik Doğrulandı: {isAuthenticated}");
```

## Katkıda Bulunma
1. Projeyi fork edin.
2. Yeni bir dal oluşturun: `git checkout -b ozellik-adi`
3. Değişikliklerinizi commit edin: `git commit -m 'Yeni özellik eklendi'`
4. Dalınızı push edin: `git push origin ozellik-adi`
5. Bir Pull Request açın.

## Lisans
Bu proje [MIT Lisansı](LICENSE) ile lisanslanmıştır.
