using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>
            {
                opt.TokenValidationParameters=new TokenValidationParameters
                {
                    ValidateAudience=true, //bu token ı kimler kullanabilir
                    ValidateIssuer=true,//token ın saglayıcısı kim
                    ValidateLifetime= true, // lifetime ı kontrol et lifetime tamamlandıysa yetkilendirme erişilemez olsun
                    ValidateIssuerSigningKey=true, //tokeın cripto'layacagımız key kontrol et
                    ValidIssuer=Configuration["Token:Issuer"], // Bu tokenın yaratılırken ki ıssuer'ı yazdık
                    ValidAudience = Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),//SecurityKey şifreliyoruz.
                    ClockSkew=TimeSpan.Zero //tokenı üreten sunucunun client daki süre farkı oldugunda süre ekliyoruz

                }; //token ın nasıl valide edileceginin parametrelerini veriyoruz burada
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            //.Startup.cs içerisinde ConfigureServices() içerisinde DbContext'in servis olarak eklenmesi
            services.AddDbContext<BookStoreDBContext>(options=>options.UseInMemoryDatabase(databaseName:"BookStoreDB"));// database servisini enjecte etme

            services.AddScoped<IBookStoreDbContext>(provider=>provider.GetService<BookStoreDBContext>());

            services.AddAutoMapper(Assembly.GetExecutingAssembly()); // automapper ekleme
            services.AddSingleton<ILoggerService,ConsoleLogger>(); // dependency injection - addSingleton kullandık ve ILoggerService cagrıldıgında ConsoleLogger calıssın.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomExceptionMiddle(); // yazdıgımız middleware ekledik

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
