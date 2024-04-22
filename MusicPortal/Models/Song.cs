namespace MusicPortal.Models
{
    public class Song
    {

        public int Id { get; set; } 
        public string? Name { get; set; }
        public virtual Genre? Genre { get; set; }
        public int? GenreId { get; set; }
        public virtual Artist? Artist { get; set; }
        public int? ArtistId { get; set; }
        public string? Path {  get; set; }

        public string? Image { get; set; }
    }
}
