using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.UpdateOperation.Command.UpdateAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [InlineData("","")]
        [InlineData(" "," ")]
        [InlineData("name"," ")]
        [InlineData(" ","name")]
        [InlineData("nam","na ")]
        [InlineData(" name","nam")]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model= new UpdateAuthorModel(){Name=name,Surname=surname, DateOfBirth=(DateTime.Now.Date.AddYears(-10)).ToString()};

            UpdateAuthorCommandValidator validations = new UpdateAuthorCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [InlineData("Sümeyye","Coşkun")]
        [InlineData("Sümeyye","Cosk")]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldNotBeReturnErrors(string name, string surname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model= new UpdateAuthorModel(){Name=name,Surname=surname, DateOfBirth=(DateTime.Now.Date.AddYears(-10)).ToString()};

            UpdateAuthorCommandValidator validations = new UpdateAuthorCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}