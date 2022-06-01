using System;
using FluentValidation;

namespace WebApi.BookOperation.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand> // DeleteBookCommand sınıfdaki objeleri valide etti
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command=> command.BookId).GreaterThan(0);//Bookid nin sıfırdan daha büyük olması gerektiği 
        }
    }

}