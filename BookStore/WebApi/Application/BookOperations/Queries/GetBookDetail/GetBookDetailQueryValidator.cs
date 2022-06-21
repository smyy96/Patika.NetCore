using FluentValidation;
using WebApi.Application.BookOperation.Queries.GetBookDetail;

namespace WebApi.Application.BookOperation.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery> // DeleteBookCommand sınıfdaki objeleri valide etti
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query=> query.BookId).GreaterThan(0);
        }
    }
}