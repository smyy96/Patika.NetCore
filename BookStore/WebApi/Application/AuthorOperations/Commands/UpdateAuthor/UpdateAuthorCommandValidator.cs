using FluentValidation;
using WebApi.Application.BookOperation.Command.UpdateAuthor;

namespace WebApi.Application.BookOperation.Command.UpdateBook
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(command=> command.Model.Surname).NotEmpty().MinimumLength(4);
        }
    }
}