using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperation.Command.DeleteBook;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidBookIdisGiven_Validator_ShouldBeReturnErrors(int bookid)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId=bookid;

            DeleteBookCommandValidator  validations= new DeleteBookCommandValidator();
            var result = validations.Validate(command);


            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenInvalidBookIdisGiven_Validator_ShouldNotBeReturnError(int bookid)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId=bookid;

            DeleteBookCommandValidator  validations= new DeleteBookCommandValidator();
            var result = validations.Validate(command);


            result.Errors.Count.Should().Be(0);

        }
    }
}