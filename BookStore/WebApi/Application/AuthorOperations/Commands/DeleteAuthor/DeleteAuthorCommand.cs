using System;
using FluentValidation;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperation.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int authorId {get; set;}
        private readonly BookStoreDBContext _context;
        public DeleteAuthorCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id == authorId);
            if(author is null)
               throw new InvalidOperationException("Silinecek yazar bulunamadı.");
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}