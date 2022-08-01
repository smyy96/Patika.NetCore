using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperation.Queries.GetBookDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Queries
{
    public class GetBookDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenBookIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            
            GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(_context,_mapper);
            bookDetailQuery.BookId=0;
                        

            FluentActions.Invoking(() => bookDetailQuery.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Kitap bulunamadı.");

                
       }

        [Fact]
        public void WhenGivenBookIdIsinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(_context,_mapper);
            bookDetailQuery.BookId=1;
            

            FluentActions.Invoking(()=> bookDetailQuery.Handle()).Invoke();

            var book=_context.Books.SingleOrDefault(book=>book.Id == bookDetailQuery.BookId);
            book.Should().NotBeNull();
        }
    }
}