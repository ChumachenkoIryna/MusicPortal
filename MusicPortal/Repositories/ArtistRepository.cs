using MusicPortal.Models;
using System.Collections;

namespace MusicPortal.Repositories
{
    public class ArtistRepository: IArtistRepository
    {
        private readonly MusicContext _context;

        public ArtistRepository(MusicContext context)
        {
            _context = context;
        }
        public  List<Artist> GetArtistList()
        {
            return  _context.Artists.ToList();
        }
        public IEnumerable GetValues()
        {
            return _context.Artists;
        }
        public  void Create(Artist a)
        {
             _context.Artists.Add(a);
            _context.SaveChanges();
        }

        public void Update(Artist a)
        {
            _context.Artists.Update(a);
            _context.SaveChanges();
        }
        public  void Delete(int id)
        {
            Artist? a =  _context.Artists.Find(id);
            if (a != null)
                _context.Artists.Remove(a);
            _context.SaveChanges();
        }

        public  void Save()
        {
             _context.SaveChanges();
        }

        public Artist GetArtist(int? id)
        {
            return _context.Artists.FirstOrDefault(a => a.Id == id);
        }
    }
}
