using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Book
    {
        //Auto Increment ID kolonunun eklenmesi
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// ıd otomatik artması için
        public int Id { get; set; }    
        public string Title { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; } // GenreId ile Genre.csdeki id arasındaki ilişkiyi kurduk FK.
        public int AuthorId { get; set; }
        public Author Author { get; set; }     
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }


    }

}