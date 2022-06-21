using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperation.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList=_dbContext.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> vm= _mapper.Map<List<BooksViewModel>>(bookList);//new List<BooksViewModel>();
            /*foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel(){
                    Title=book.Title,
                    Genre=((GenreEnum)book.GenreId).ToString(),
                    PublishDate=book.PublishDate.Date.ToString("dd/mm/yyyy"),
                    PageCount=book.PageCount
                });
            }*/
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public String PublishDate { get; set; }
        public string Genre { get; set; }

    }
}