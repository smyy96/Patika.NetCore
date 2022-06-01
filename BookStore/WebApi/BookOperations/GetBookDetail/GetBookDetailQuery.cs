using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperation.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId {get;set;}
        public GetBookDetailQuery(BookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book=_dbContext.Books.Where(x=>x.Id==BookId).SingleOrDefault();
            if(book is null){
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
            BookDetailViewModel vs = _mapper.Map<BookDetailViewModel>(book); //new BookDetailViewModel();
            /*vs.Title=book.Title;
            vs.PageCount=book.PageCount;
            vs.Genre=((GenreEnum)book.GenreId).ToString();
            vs.PublishDate=book.PublishDate.Date.ToString("dd/mm/yyyy");*/
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