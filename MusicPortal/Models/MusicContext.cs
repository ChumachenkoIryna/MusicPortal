using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace MusicPortal.Models
{
    public class MusicContext:DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options)
                : base(options)
        {
            if (Database.EnsureCreated())
            {
                Genres.Add(new Genre { Name = "Pop" });
                Genres.Add(new Genre { Name = "Rock" });
                Genres.Add(new Genre { Name = "Jazz" });
                Genres.Add(new Genre { Name = "Blues" });
                Genres.Add(new Genre { Name = "House" });
                Artists.Add(new Artist { Name = "Armstrong" });
                Artists.Add(new Artist { Name = "Scorpions" });
                Artists.Add(new Artist { Name = "ABBA" });
                //  Songs.Add{ new Song { Name="Wind of change"}}
                SaveChanges();
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseLazyLoadingProxies();
        //}
    }
}
