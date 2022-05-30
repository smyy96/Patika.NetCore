using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperation.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDBContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book=_dbContext.Books.SingleOrDefault(x=>x.Id==BookId);

           if(book is null)
           {
               throw new InvalidOperationException("Silinecek kitap bulunamadı.");
           }
           _dbContext.Books.Remove(book);
           _dbContext.SaveChanges();
        }
    }
}