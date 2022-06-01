using System;
using FluentValidation;

namespace WebApi.BookOperation.CreatBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreatBookCommand> // CreatBookCommand sınıfdaki objeleri valide eder.
    {
        public CreateBookCommandValidator() // validator yapıcı metot ile calısır
        {
            RuleFor(command=> command.Model.GenreId).GreaterThan(0);//Genreid nin sıfırdan daha büyük olması gerektiği kuralını ekledik
            RuleFor(command=> command.Model.PageCount).GreaterThan(0);//sayfaa sayısı sıfırdan büyük olsun
            RuleFor(command=> command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date); // tarih bugunden küçük olmalı command.Model.PublishDate.Date burada sadece tarihi aldık saati almadık
            RuleFor(command=> command.Model.Title).NotEmpty().MinimumLength(4);// kitap adı boş olmamalı ve min 4 karakter olmalı
        }

        

    }
}