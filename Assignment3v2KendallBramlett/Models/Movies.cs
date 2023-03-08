using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3v2KendallBramlett.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        [Url]
        public string? IMBDLink { get; set; }
        public string? Genre { get; set; }
        public int ReleaseYear { get; set; }
        public byte[]? MoviePoster { get; set; }

        [ForeignKey("Actor")]
        public int? ActorID { get; set; }
    }
}
