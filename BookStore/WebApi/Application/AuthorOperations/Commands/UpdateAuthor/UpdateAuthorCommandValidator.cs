using FluentValidation;

namespace WebApi.Application.UpdateOperation.Command.UpdateAuthor
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