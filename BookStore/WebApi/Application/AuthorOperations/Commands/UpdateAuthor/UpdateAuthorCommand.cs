using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperation.Command.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int Authorid {get;set;}
        public UpdateAuthorModel Model {get;set;}
        private readonly IBookStoreDbContext _dbContext;
        public UpdateAuthorCommand(IBookStoreDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x=>x.Id== Authorid);
             if(author is null)
                throw new InvalidOperationException("Yazar Bulunamadı.");
            
            author.Name = Model.Name == default ? author.Name : Model.Name;
            author.Surname = Model.Surname == default ? author.Surname : Model.Surname;
            author.DateOfBirth=Convert.ToDateTime(Model.DateOfBirth);
            
            _dbContext.Authors.Update(author);
            _dbContext.SaveChanges();

        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
    }
}