# Patika .Net Core

### AutoMapper
```
Automapper farklı tipteki complex objeleri birbirlerine otomatik olarak dönüştüren kütüphanedir. 
Kod kirliliğinde bizi kurtarak birden fazla satırda her bir obje elemanını tek tek dönüştürmek 
yerine tek satırda objenin kendisini dönüştürmemize olanak sağlar.

1.Öncelikle Automapper kütüphanesinin projeye dahil edilmesi gerekir.
  - AutoMapper Dependecy Injection Paketini de eklenmeli.

2. Proje içerisinde AutoMappper'ı servis olarak kullanabilmemiz için Startup.cs dosyası içerisindeki 
Configure Service metoduna aşağıdaki kod satırının eklenmesi gerekir.

     services.AddAutoMapper(Assembly.GetExecutingAssembly());

3.Mapper Konfigürasyonu için Profile sınıfından kalıtım alan aşağıdaki gibi bir sınıf implemente 
etmemiz gerekir.

4.Eklemiş olduğumuz Dependency Injection paketi sayesinde Controller'ın kurucu fonksiyonunda 
mapper'ı kod içerisinde kullanılmak üzere dahil edebiliriz.

5.Artık kod içerisinde _mapper'ı kullanabiliriz.

```

### FluentValidation 

```
Fluent Validation kütüphanesi ile objelerin kontrol edilmesini sağlarız. Yani bir post işlemi 
yaptığımız zaman alınan değerlerin belli kritelerde olması gerekir doğum günü tarihinin 
bulunduğumuz günden küçük olması gerektiği gibi ya da tc kimlik numarasının sayısal ve 11 
haneli olması gibi kontrolleri bu kütüphane ile sağlamış oluyoruz.

1. FluentValidation'ı kullanabilmiz için öncelikle kütüphaneyi paket olarak uygulamamıza eklememiz gerekir.

dotnet add package FluentValidation

2. Validasyon sınıfı oluşturulur ve kurucu metot içerisine kurallar yazılır.

3. Controller validation nesnesini oluşturmak ve hataları yakalayıp ekrana yazdırmak.

```

### Middleware

```
Middleware yani ara katman client tarafından bir request gönderildiğinde request'e 
karşılık response dönene kadar geçen sürede yapılması gereken işlemler için process'in 
arasına girmeyi sağlayan yapılardır. Request ve response arasına girip işlem yapmamıza 
olanak sağlamasının yanında, bu aralığa çoklu işlemler de dahil edebiliriz. Bu işlemlerin 
hangi sırayla yapılacağını da belirleyebiliriz.
 
 1. Run Metodu
  Run metodundan sonraki işlemler gerçekleşmez. Pipelinenın kısa devre olmasına neden olur.
       
       app.Run(async context => Console.WriteLine("Middleware 1."));
       
 2. Use Metodu
  Devreye girdikten sonra kendinden sonraki middleware'i tetikleyebilir ve işi bittikten 
  sonra kaldığı yerden devam edilebilir bir yapı sunar.
  
  app.Use(async (context, next) =>
    {
        Consoel.WriteLine("Middleware 1 başladı.");
        await next.Invoke(); //  bir sonraki middleware çağıran komuttur.
        Console.WriteLine("Middleware 1 sonlandırılıyor.");
    });

    app.Run(async context =>
    {
        Console.WriteLine("Middleware 2 kısa devre yaptırıyor.");
    });
    
 3. Map Metodu
  Middleware lerin path bazından çalışmasını istediğimiz durumlarda kullanırız. 
  
    app.Map("/test", internalApp =>
        internalApp.Run(async context =>
            {
                Console.WriteLine("/test middleware tetiklendi.");
            }));
            
 4. MapWhen Metodu
  Map metodu ile sadece path'e bazında middleware yönetebilirken MapWhen ile 
  request'e bağlı olarak her türlü yönlendirmeyi yapabiliriz.
  
      app.MapWhen(x => x.Request.Method == "GET", internalApp =>  // HTTPGET olan requestlere özel çalışır
      {
        internalApp.Run(async context => await Console.WriteLine("MapWhen ile Middleware Tetiklendi."));
      });
      
 5. Custom Exception Middleware Yaratılmak
  Kendi yarattığımız middleware.


```

### Dependency Injection

```
Nesne yönelimli programlama dilleri ile uygulama geliştirirken, kullandığımız nesneler arasında bir
iletişim kurarız. Bu iletişimin bir sonucu olarak da nesneler arasında bir bağımlılık (dependency) 
oluşmuş olur.

Dependency Injection tekniği uygulayarak bağımlılıkları sınıf içerisinde yönetmek yerine dışarıdan 
verilmesini sağlarız. Bu sayede bağımlı olunan nesnenin oluşturulması ve yönetimi sınıf dışında yapılmış 
olur ve bağımlılığın bir kısmı azaltılmış olur.


Dependency Injection tekniğini 3 farklı yöntem ile uygulayabiliriz.
1 - Constructor (Yapıcı Method) ile : Bu yöntemde bağımlı olunan nesneler yapıcı methodda belirtilir 
ve dışarıdan beklenir. Yukarıdaki örnek bu yönteme bir örnektir. 
2 - Setter Method/Property ile : Bu yöntemde bağımlı olunan nesneler bir method/property aracılığı 
ile dışardan beklenir.
3 - Metot ile : Bu yöntemde bağımlı olunan nesneler yalnızca kullanıldığı methodlarda dışarıdan beklenir.


••• Dependency Injection Container

  İhtiyacımız olan sınıfa ait bir nesneye; bağımlılıkları dışarıdan verilmiş kullanıma hazır bir şekilde 
  rahatlıkla ulaşarak kullanabiliriz. Böylece ihtiyacımız olan bir nesneyi oluştururken bağımlı olduğu 
  nesnelerin de yaratılması işlemlerinden kurtulmuş oluruz.
  
  .Net Core içerisinde hazır bulunan containerı Startup'daki ConfigureServices metodu içerisinde 
  kullanırız. Bu methodun IServiceCollection tipinde services adıyla aldığı parametre aslında container
  nesnesidir diye düşünebiliriz.
  
    public void ConfigureServices("""""IServiceCollection""""" services)
    {
        services.Add... //Register services
    }
  
  .Net Core DI Container'a bir sınıf kayıt ederken bu sınıfa ait nesnenin yaşam süresini de belirtmemiz 
  gerekir. 
 
  .Net Core Container Yaşam Süreleri

  • Singleton Service
    Bu yaşam süresine sahip nesne uygulamanın çalışmaya başladığı andan duruncaya kadar geçen 
    tüm süre boyunca yalnızca bir kez oluşturulur ve her zaman aynı nesne kullanılır.
  
    services.AddSingleton<ClassAdı>()
  
  • Scoped Service
    Bir HTTP request boyunca yalnızca bir kez oluşturulur ve response oluşana kadar her zaman 
    aynı nesne kullanılır.
  
    services.AddScoped<ClassAdı>()
  
  • Transient Service
    Bu yaşam süresine sahip nesne, Container tarafından her seferinde yeniden oluşturuluyor.
  
    services.AddTransient<ClassAdı>()
    
 Loosely Coupled (gevşek bağımlı) 
   Eğer kayıt edilecek servis bir interface implemente ediyor ve bu interface aracılığı ile 
   kullanılıyor ise; kayıt sırasında hem interface tipini hem de bu interface'i implemente eden 
   sınıfı belirtmemiz gerekir. Bu  şekilde bağımlı olunan nesnenin sınıfını bilmemize gerek kalmadan 
   bir interface yardımı ile ihtiyaç duyduğumuz iletişimi sağlamış oluruz.
```

















