using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperation.CreatBook;
using WebApi.BookOperation.GetBooks;
using WebApi.DBOperations;

namespace WebApi.AddControllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDBContext _context;

        public BookController(BookStoreDBContext context)
        {
            _context=context;
        }

        /*private static List<Book> BookList = new List<Book>(){
            new Book{Id=1,Title="Lean Startup", GenreId=1, PageCount=200, PublishDate=new DateTime(2001,06,12)},
            new Book{Id=2,Title="HerLand", GenreId=2, PageCount=250, PublishDate=new DateTime(2001,06,12)},
            new Book{Id=3,Title="Dune", GenreId=2, PageCount=540, PublishDate=new DateTime(2018,06,12)}
        };*/


        [HttpGet]
        public IActionResult GetBooks() // Book listesindeki verileri alma 
        {
            GetBooksQuery query=new GetBooksQuery(_context);
            var result=query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Book GetById(int id) // Book listesindeki id si verilen elemanı bulma
        {
            var book=_context.Books.Where(x=>x.Id==id).SingleOrDefault();//SingleOrDefault Tek bir deger döndürdügümüz için
            return book;
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
            CreatBookCommand command=new CreatBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
            return Ok();
        }


        //Put
        [HttpPut("{id}")] //güncelleme
        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
        {
            var book=_context.Books.SingleOrDefault(x=>x.Id==id);

           if(book is null)
           {
               return BadRequest();
           }

            book.GenreId=updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId; // updatedBook.GenreId bilgisinin degiştirilip degiştirilmedigini default ile anlayıp, degiştirildiyse updatedBook.GenreId degerini  book.GenreId a atıyoruz degiştirilmediyse  book.GenreId bu degeri kendisie ekliyoruz.
            book.PageCount=updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate=updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title=updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok();
           
        }

        [HttpDelete("{id}")] // Silme
        public IActionResult DeleteBook(int id)
        {
            var book=_context.Books.SingleOrDefault(x=>x.Id==id);

           if(book is null)
           {
               return BadRequest();
           }
           _context.Books.Remove(book);
           _context.SaveChanges();
           return Ok();
        }

        
    }

}