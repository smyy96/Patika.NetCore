using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// ıd otomatik artması için
        
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

    }
}