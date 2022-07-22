using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperation.Command.CreatBook;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        // burada fact kullansaydık her kosul için tek tek kodlama yapacaktık, theory ile alt alta kontrol edecegimiz degerleri tanımlayabiliyoruz. herbir koşul için ayrı ayrı kontrolu bu şekilde yapmış oluyoruz.
        [Theory]
        [InlineData("Lean Startup",0,0)]
        [InlineData("Lean Startup",0,1)]
        [InlineData("Lean Startup",100,0)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("",0,1)]
        [InlineData("Lea",100,1)]
        [InlineData("Lean",100,0)]
        [InlineData("Lean",0,1)]
        [InlineData(" ",100,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int pagecount, int genreid) // validator invalid input verildiyse validator hata döndürsün
        {
            //arrang   
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model=new CreateBookModel()
            {
                Title=title,
                PageCount=pagecount,
                PublishDate=System.DateTime.Now.Date.AddYears(-1),
                GenreId=genreid
            };
            //act
            CreateBookCommandValidator  validations= new CreateBookCommandValidator();
            var result = validations.Validate(command);

            //assert   
            result.Errors.Count.Should().BeGreaterThan(0);// hata objenin countu 0dan büyük olmalı     
        }

        [Fact] // tarih ayrı kontrol ediyoruz
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError() // tarih bugune eşit ise hata döndür
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model=new CreateBookModel()
            {
                // sadece dateTime ı kontrol ettiğimiz için title pagecount genreid degerlerini basarılı veriyoruz ki test sonucunu etkilemesin
                Title="Lean Startup",
                PageCount=100,
                PublishDate=System.DateTime.Now.Date, //
                GenreId=1
            };

             CreateBookCommandValidator  validations= new CreateBookCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError() // geriye hata döndürmediği durumları kontrol etme
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model=new CreateBookModel()
            {
                // sadece dateTime ı kontrol ettiğimiz için title pagecount genreid degerlerini basarılı veriyoruz ki test sonucunu etkilemesin
                Title="Lean Startup",
                PageCount=100,
                PublishDate=System.DateTime.Now.Date.AddYears(-2),
                GenreId=1
            };

             CreateBookCommandValidator  validations= new CreateBookCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}