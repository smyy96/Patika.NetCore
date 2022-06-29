using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperation.Command.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDBContext _dbContext;
        public int BookId {get;set;}
        public UpdateBookModel Model { get; set; }
        public UpdateBookCommand(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Handle()
        {
            var book=_dbContext.Books.SingleOrDefault(x=>x.Id==BookId);

           if(book is null)
           {
               throw new InvalidOperationException("Güncellenecek kitap bulunamadı.");
           }

            book.GenreId=Model.GenreId != default ? Model.GenreId: book.GenreId; // updatedBook.GenreId bilgisinin degiştirilip degiştirilmedigini default ile anlayıp, degiştirildiyse updatedBook.GenreId degerini  book.GenreId a atıyoruz degiştirilmediyse  book.GenreId bu degeri kendisie ekliyoruz.
            book.Title=Model.Title != default ? Model.Title : book.Title;

            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        

    }
}