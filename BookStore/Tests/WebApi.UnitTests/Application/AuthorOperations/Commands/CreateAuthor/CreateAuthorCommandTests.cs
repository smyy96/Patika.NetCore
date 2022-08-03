using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenAlreadyExistAuthorNameSurnameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var author = new Author()
            {
                Name="Sümeyye",
                Surname="Coşkun",
                DateOfBirth=new DateTime(1996,05,08)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_mapper,_context);
            command.Model=new CreateAuthorModel()
            {
                Name= author.Name,
                Surname=author.Surname,
                DateOfBirth=(author.DateOfBirth).ToString()
            };

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Zaten Mevcut.");
        }


        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_mapper,_context);
            command.Model=new CreateAuthorModel()
            {
                Name = "Sigmund",
                Surname="Freud",
                DateOfBirth="06.05.1856"
            };

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var author=_context.Authors.SingleOrDefault(author=>author.Name == command.Model.Name && author.Surname == command.Model.Surname);
            author.Should().NotBeNull();
            author.DateOfBirth.Should().Be(Convert.ToDateTime(command.Model.DateOfBirth));
        }

    }
}