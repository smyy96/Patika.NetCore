using AutoMapper;
using WebApi.Application.AuthorOperation.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperation.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.BookOperation.Command.CreatBook;
using WebApi.Application.BookOperation.Queries.GetBookDetail;
using WebApi.Application.BookOperation.Queries.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile // profile yi kalıtım aldık
    {
        public MappingProfile()
        {

            // CreateMap<Source,Target> parametreleri ile çalışır. Bu şu demek; kod içerisinde source ile belirtilen obje tipi target ile belirtilen obje tipine dönüştürülebilir.    


            CreateMap<CreateBookModel, Book>(); // CreatBookModel objesi Book objesine mapleme işlemi yaptık yani 
            /*
                createbookmodek içerisinde yaptıgımız

                book.Title=Model.Title;
                book.PublishDate=Model.PublishDate;
                book.PageCount=Model.PageCount;
                book.GenreId=Model.GenreId;
                
                bu işlemi direk olarak dönüştürebiliyoruz mapping sayesinde 
            */



            //Mapper ile obje özelliklerinin birbirine nasıl map'laneceğini de söyleyebiliriz.

            CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>src.Genre.Name)); // gelen verinin id ile degilde degerinin gelmesini istedigimiz için bu şekilde yazdık. formemberdan sonra ki kısım verileri kullanıcıya nasıl gösterecegimizi belirledigimiz yer 
            // dest=>dest.Genre bu kısmı, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()) burası ile mappleme (MapFrom)
            //(src=>((GenreEnum)src.GenreId).ToString()) ---- degiştirdik src=>src.Genre.Name    yaptık.

            //CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Author, opt=>opt.MapFrom(src=>src.Author.Name+" "+src.Author.Surname));


            CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>src.Genre.Name));
            //CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Author, opt=>opt.MapFrom(src=>src.Author.Name+" "+src.Author.Surname));
            
            CreateMap<Genre,GenresViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();


            CreateMap<Author,AuthorsViewModel>();
            CreateMap<Author,AuthorsDetailViewModel>();


            CreateMap<CreateAuthorModel, Author>();
        
        
        }

    }
}