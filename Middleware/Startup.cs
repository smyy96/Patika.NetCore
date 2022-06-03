using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Middleware
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Middleware", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Middleware v1"));
            }

            //middleware isimleri use ile baslar kural gibi.
            // sıralama önemli
            //middlewareler async calısır

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.Run(); // Run middleware indan ki satırlarr çalışmaz kısa devre yapar
            //app.Run(async context =>  Console.WriteLine("middleware 1"));


            //app.Use(); //sonucu beklemek gerekiyorsa async metodunu awaitlemeliyiz. sonuc geldikten sonra alt satırdaki kodlar calısır
            // app.Use(async(context,next)=>{
            //      Console.WriteLine("middleware 1 basladı.");
            //      await next.Invoke();// sonrasındaki middleware teetikledik.
            //      Console.WriteLine("middleware 1 sonlandırıldı.");
            // }); 

            // app.Use(async(context,next)=>{
            //      Console.WriteLine("middleware 2 basladı.");
            //      await next.Invoke();
            //      Console.WriteLine("middleware 2 sonlandırıldı.");
            // });


            app.Use(async(context,next)=>{
                 Console.WriteLine("use middleware tetiklendi");
                  await next.Invoke();
             });


            //app.Map();            
            // /example route na istek gelirse bu middleware calıstır.
            // route a göre middlewareları yönetmemizi saglıyor 
            app.Map("/example", internalApp  =>
            internalApp.Run(async context =>
             {
                 Console.WriteLine("/example middleware tetiklendi.");
                 await context.Response.WriteAsync("/example middleware tetikelndi");
             }));


            //app.MapWhen();
            app.MapWhen(x=>x.Request.Method=="GET",internalApp=>
            {
                internalApp.Run(async context=> 
                {
                        Console.WriteLine("MapWhen middleware tetiklendi.");
                        await context.Response.WriteAsync("MapWhen middleware tetiklendi.");
                    
                });
            });





            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
