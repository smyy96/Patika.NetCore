using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperation.Command.CreatBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            var book = new Book(){Title="Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount=100,PublishDate=new System.DateTime(1990,01,10), GenreId=1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command= new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel(){Title= book.Title};
            
            //act and assert (çalıştırma - dogrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Zaten Mevcut");

            
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
            CreateBookCommand command= new CreateBookCommand(_context,_mapper);
            CreateBookModel model = new CreateBookModel(){
                Title="Hobbit",
                PageCount=100,
                PublishDate=System.DateTime.Now.Date.AddYears(-10),
                GenreId=1};
            command.Model=model;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // should veya invoke demezsek metodu çalıştırmaz. ınvoke metodu çalıştırır, should ile dönüşü konytol ettigimiz için otomatik olarak çalıştırmısta oluyor.

            //assert
            var book=_context.Books.SingleOrDefault(book=>book.Title== model.Title); // kitap mevcutmu kontrol ediyor.
            book.Should().NotBeNull();//book null olmamalı 
            book.PageCount.Should().Be(model.PageCount);// book pageount modeldeki pagecounta eşit olmalı
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}