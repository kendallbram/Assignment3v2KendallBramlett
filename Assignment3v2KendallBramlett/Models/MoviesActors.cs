using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3v2KendallBramlett.Models
{
    public class MoviesActors
    {
        public int MoviesActorsId { get; set; }
        [ForeignKey("Movies")]
        public int? MovieId { get; set; }
        public Movies? Movie { get; set; }
        [ForeignKey("Actors")]
        public int? ActorID { get; set; }
        public Actors? Actors { get; set; }

    }
}
