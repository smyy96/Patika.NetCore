using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILoggerService _loggerservice;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerservice)
        {
            this.next = next;
            _loggerservice = loggerservice;
        }

        public async Task Invoke(HttpContext context)
        {
             var watch = Stopwatch.StartNew(); // ne kadar sürece yanıt döndügünü ögrenmek için süreyi aldık

               
            try
            {
                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path; // hangi path geldigini yazdırıyoru<.
                //Console.WriteLine(message);
                _loggerservice.Write(message);//bagımlılıklardan kurtulmak için bu şekilde yazdık.

                await next(context);//bir sonraki middleware cagırdık
                watch.Stop();//süreyi durdur

                message = "[Response] " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "ms"; // hangi response un döndüğünü ve ne kadar zaman içinde dönüş yaptgını yazdırıyoruz.
                //Console.WriteLine(message);
                _loggerservice.Write(message);
            }

            catch(Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex,watch);// controllerdaki try catch bloklarını kaldırdık ve kontrolleri bu middleware içinde kontrol ediyoruz
            }

        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType="application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            
            string message="[Error]   HTTP "+context.Request.Method+" - "+context.Response.StatusCode+" Error Message "+ex.Message+" in"+watch.Elapsed.TotalMilliseconds+"ms"; // hata mesajının içerigi
            //Console.WriteLine(message); // yazdırma
            _loggerservice.Write(message);
            
            var result= JsonConvert.SerializeObject(new {error=ex.Message},Formatting.None);
            
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}