using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperation.Queries.GetBookDetail;
using Xunit;

namespace Application.BookOperations.Queries
{
    public class GetBookDetailQueryValidatorTests:IClassFixture<CommonTestFixture>
    {

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-114)]
        [Theory]
        public void WhenInvalidBookidIsGiven_Validator_ShouldBeReturnErrors(int bookid)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId=bookid;

            GetBookDetailQueryValidator validations = new GetBookDetailQueryValidator();
            var result = validations.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [InlineData(1)]
        [InlineData(25)]
        [Theory]
        public void WhenInvalidBookidIsGiven_Validator_ShouldNotBeReturnErrors(int bookid)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId=bookid;

            GetBookDetailQueryValidator validations = new GetBookDetailQueryValidator();
            var result = validations.Validate(query);

            result.Errors.Count.Should().Be(0);
        }


    }
}