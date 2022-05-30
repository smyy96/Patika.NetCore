using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperation.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDBContext _dbContext;
        public int BookId {get;set;}
        public GetBookDetailQuery(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book=_dbContext.Books.Where(x=>x.Id==BookId).SingleOrDefault();
            if(book is null){
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
            BookDetailViewModel vs= new BookDetailViewModel();
            vs.Title=book.Title;
            vs.PageCount=book.PageCount;
            vs.Genre=((GenreEnum)book.GenreId).ToString();
            vs.PublishDate=book.PublishDate.Date.ToString("dd/mm/yyyy");
            return vs;
        }
    }


    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        

    }
}