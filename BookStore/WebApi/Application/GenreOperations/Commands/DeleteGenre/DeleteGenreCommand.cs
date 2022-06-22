using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDBContext _context;

        public DeleteGenreCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre=_context.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if(genre is not null)
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut.");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}