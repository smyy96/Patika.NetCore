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


```
