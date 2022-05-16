using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
   
        //Initialize metot tanımlıyoruz verileri insert eden
        public static void Initialize(IServiceProvider serviceProvider) //program.cs baglayacagız uygulama ilk ayaga kalktıgında hep calısacak bir yapı serviceProvider sayesinde oluyor.
        {
            using (var context=new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                if(context.Books.Any())// Books  içinde veri varsa calıstırmayacak
                {
                    return;
                }
                
                //Veri yoksa veri ekleme işlemi
                context.Books.AddRange(
                    new Book{Title="Lean Startup", GenreId=1, PageCount=200, PublishDate=new DateTime(2001,06,12)},
                    new Book{Title="HerLand", GenreId=2, PageCount=250, PublishDate=new DateTime(2001,06,12)},
                    new Book{Title="Dune", GenreId=2, PageCount=540, PublishDate=new DateTime(2018,06,12)}
                );

                context.SaveChanges();// dbye yazılmasını saglıyoruz yani kaydetmeyi
            }    
        }
    }
}
