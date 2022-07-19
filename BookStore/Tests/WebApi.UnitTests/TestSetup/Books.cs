using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDBContext context)
        {
            context.Books.AddRange(
                new Book{Title="Lean Startup", GenreId=1, AuthorId=1, PageCount=200, PublishDate=new DateTime(2001,06,12)},
                new Book{Title="HerLand", GenreId=2,AuthorId=2, PageCount=250, PublishDate=new DateTime(2001,06,12)},
                new Book{Title="Dune", GenreId=2, AuthorId=3, PageCount=540, PublishDate=new DateTime(2018,06,12)}
            );
        }
    }
}