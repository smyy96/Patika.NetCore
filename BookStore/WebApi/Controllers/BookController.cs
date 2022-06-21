using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Application.BookOperation.Queries.GetBooks;
using WebApi.Application.BookOperation.Queries.GetBookDetail;
using WebApi.Application.BookOperation.Command.CreatBook;
using WebApi.Application.BookOperation.Command.UpdateBook;
using WebApi.Application.BookOperation.Command.DeleteBook;

namespace WebApi.AddControllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*private static List<Book> BookList = new List<Book>(){
            new Book{Id=1,Title="Lean Startup", GenreId=1, PageCount=200, PublishDate=new DateTime(2001,06,12)},
            new Book{Id=2,Title="HerLand", GenreId=2, PageCount=250, PublishDate=new DateTime(2001,06,12)},
            new Book{Id=3,Title="Dune", GenreId=2, PageCount=540, PublishDate=new DateTime(2018,06,12)}
        };*/


        [HttpGet]
        public IActionResult GetBooks() // Book listesindeki verileri alma 
        {
            GetBooksQuery query=new GetBooksQuery(_context,_mapper);
            var result=query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) // Book listesindeki id si verilen elemanı bulma
        {
            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailQueryValidator cv = new GetBookDetailQueryValidator(); //validator sınıfını calıştırma
            cv.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        /*[HttpGet]
        public Book Get([FromQuery]string id) //FromQuery ile Book listesindeki id si verilen elemanı bulma
        {
            var book=BookList.Where(x=>x.Id==int.Parse(id)).SingleOrDefault();//SingleOrDefault Tek bir deger döndürdügümüz için
            return book;
        }*/



        //Post
        [HttpPost]// ekleme
        public IActionResult AddBook([FromBody] CreatBookModel newBook)// dönüş degerleri badrequest, ok .. oldugu için IActionResult
        {
            CreatBookCommand command=new CreatBookCommand(_context, _mapper);
            //try
            //{
                command.Model = newBook;
                CreateBookCommandValidator cv = new CreateBookCommandValidator(); //validator sınıfını calıştırma
                cv.ValidateAndThrow(command); // hatayı yakalayıp catchdeki exceptiona atıyor hata mesajını
                command.Handle();


                // bu hata yakalama kısmını kullandıgımız zaman hatada olsa return OK dönüyordu o yüzden bunu yapmadık
                // if(!result.IsValid) // false ise 
                // {
                //     foreach(var item in result.Errors)// hataları ekrana yazdırma
                //     {
                //         Console.WriteLine("Özellik "+ item.PropertyName + " - Error Message: "+item.ErrorMessage);
                //     }
                // }
                // else
                //     command.Handle();
           // }

            //catch(Exception ex)
            //{
               // return BadRequest(ex.Message);
           // }
            
            
            return Ok();
        }


        //Put
        [HttpPut("{id}")] //güncelleme
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;

            UpdateBookCommandValidator cv = new UpdateBookCommandValidator();
            cv.ValidateAndThrow(command);
            command.Handle();


            return Ok();

        }

        [HttpDelete("{id}")] // Silme
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator cv = new DeleteBookCommandValidator(); //validator sınıfını calıştırma
            cv.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        
    }

}