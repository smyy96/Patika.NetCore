using FluentValidation;
using WebApi.BookOperation.GetBookDetail;

namespace WebApi.BookOperation.DeleteBook
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery> // DeleteBookCommand sınıfdaki objeleri valide etti
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query=> query.BookId).GreaterThan(0);
        }
    }
}