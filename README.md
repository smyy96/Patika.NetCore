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
