using System;
using FluentValidation;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;

namespace WebApi.Application.AuthorOperation.Commands.CreatAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command=> command.Model.Surname).NotEmpty().MinimumLength(3);
        }
    }
}