using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model {get;set;}
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public CreateAuthorCommand(IMapper mapper, BookStoreDBContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author =_dbContext.Authors.SingleOrDefault(x=>x.Name== Model.Name && x.Surname==Model.Surname);
            if(author is not null)
                throw new InvalidOperationException("Yazar Zaten Mevcut.");

            author=_mapper.Map<Author>(Model);
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
    }
}