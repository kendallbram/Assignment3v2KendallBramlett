using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3v2KendallBramlett.Models
{
    public class Actors
    {
       public int Id { get; set; }
        public string? Name { get; set; } 
       public string? Gender { get; set; }
       public int Age { get; set; }

        [Url]
       public string? IMBDLink { get; set; }
       public byte[]? Headshot { get; set; }

        [ForeignKey("Movies")]
        public int? MovieId { get; set; }
        

    }
}
