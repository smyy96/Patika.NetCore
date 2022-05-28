using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperation.CreatBook
{
    public class CreatBookCommand
    {
        public CreatBookModel Model {get;set;}
        private readonly BookStoreDBContext _dbContext;

        public CreatBookCommand(BookStoreDBContext dBContext)
        {
            _dbContext=dBContext;
        }

        public void Handle()
        {
            var book=_dbContext.Books.SingleOrDefault(x=>x.Title==Model.Title);//listede aynı isimden veri var mı diye bakıyor.

           if(book is not null)
           {
               throw new InvalidOperationException("Kitap Zaten Mevcut");
           }
           book= new Book();
           book.Title=Model.Title;
           book.PublishDate=Model.PublishDate;
           book.PageCount=Model.PageCount;
           book.GenreId=Model.GenreId;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();//kaydetme işlemi için, save etmek       
        }
    }

    public class CreatBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }

    }
}