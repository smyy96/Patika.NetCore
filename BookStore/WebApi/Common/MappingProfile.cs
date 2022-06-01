using AutoMapper;
using WebApi.BookOperation.CreatBook;
using WebApi.BookOperation.GetBookDetail;
using WebApi.BookOperation.GetBooks;

namespace WebApi.Common
{
    public class MappingProfile : Profile // profile kalıtım aldık
    {
        public MappingProfile()
        {

            // CreateMap<Source,Target> parametreleri ile çalışır. Bu şu demek; kod içerisinde source ile belirtilen obje tipi target ile belirtilen obje tipine dönüştürülebilir.    


            CreateMap<CreatBookModel, Book>(); // CreatBookModel objesi Book objesine mapleme işlemi yaptık yani 
            /*
                createbookmodek içerisinde yaptıgımız

                book.Title=Model.Title;
                book.PublishDate=Model.PublishDate;
                book.PageCount=Model.PageCount;
                book.GenreId=Model.GenreId;
                
                bu işlemi direk olarak dönüştürebiliyoruz mapping sayesinde 
            */



            //Mapper ile obje özelliklerinin birbirine nasıl map'laneceğini de söyleyebiliriz.

            CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString())); // gelen verinin id ile degilde degerinin gelmesini istedigimiz için bu şekilde yazdık. formemberdan sonra ki kısım verileri kullanıcıya nasıl gösterecegimizi belirledigimiz yer 
            // dest=>dest.Genre bu kısmı, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()) burası ile mappleme (MapFrom)


            CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
        }

    }
}