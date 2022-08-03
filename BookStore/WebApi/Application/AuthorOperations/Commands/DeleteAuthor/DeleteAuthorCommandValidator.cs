using FluentValidation;
using WebApi.Application.AuthorOperation.Commands.DeleteAuthor;

namespace WebApi.Application.AuthorOperation.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command=> command.authorId).GreaterThan(0).NotEmpty();
        }
    }
}