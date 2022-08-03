using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperation.Commands.DeleteAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenBookIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.authorId=0;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek yazar bulunamadı.");
        }


        [Fact]
        public void WhenGivenBookIdIsinDB_InvalidOperationException_ShouldBeReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.authorId=1;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var author=_context.Authors.SingleOrDefault(author=>author.Id == command.authorId);
            author.Should().Be(null);
        }
    }
}