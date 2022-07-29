using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperation.Command.UpdateBook;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [InlineData(1,1,"abc")]
        [InlineData(0,1,"abcaa")]
        [InlineData(1,-1,"abcdef")]
        [InlineData(0,0,"abc")]
        [InlineData(-1,-1,"abcdefg")]
        [InlineData(1,1," ")]
        [InlineData(1,1,"")]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookid, int genreid, string title)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title =title,
                GenreId = genreid                
            };
            command.BookId=bookid;

            UpdateBookCommandValidator validations=new UpdateBookCommandValidator();
            var result = validations.Validate(command);

             result.Errors.Count.Should().BeGreaterThan(0);
        }


        [InlineData(1,1,"abcd")]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int bookid, int genreid, string title)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title =title,
                GenreId = genreid                
            };
            command.BookId=bookid;

            UpdateBookCommandValidator validations=new UpdateBookCommandValidator();
            var result = validations.Validate(command);

             result.Errors.Count.Should().Be(0);
        }
    }
}