using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperation.Commands.CreatAuthor;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","")]
        [InlineData(" "," ")]
        [InlineData("","asd")]
        [InlineData("aa","aaa")]
        [InlineData("aa","a ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model=new CreateAuthorModel()
            {
                Name=name,
                Surname=surname,
                DateOfBirth="08.05.1996"
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


       
        [Theory]
        [InlineData("Süm","Coskun")]
        [InlineData("Sumeyye","Cos")]
        [InlineData("Sum","Cos")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(string name, string surname)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model=new CreateAuthorModel()
            {
                Name=name,
                Surname=surname,
                DateOfBirth="08.05.1996"
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}