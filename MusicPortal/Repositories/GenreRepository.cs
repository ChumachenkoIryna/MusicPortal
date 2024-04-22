using MusicPortal.Models;
using System.Collections;

namespace MusicPortal.Repositories
{
    public class GenreRepository:IGenreRepository
    {
        private readonly MusicContext _context;

        public GenreRepository(MusicContext context)
        {
            _context = context;
        }
        public  List<Genre> GetGenreList()
        {
            return  _context.Genres.ToList();
        }

        public IEnumerable GetValues()
        {
            return _context.Genres;
        }
        public  void Create(Genre g)
        {
             _context.Genres.Add(g);
            _context.SaveChanges();
        }
        public void Update(Genre g)
        {
            _context.Genres.Update(g);
            _context.SaveChanges();
        }
        public  void Delete(int id)
        {
            Genre? g =  _context.Genres.Find(id);
            if (g != null)
                _context.Genres.Remove(g);
            _context.SaveChanges();
        }

        public  void Save()
        {
            _context.SaveChanges();
        }

        public Genre GetGenre(int? id)
        {
            return _context.Genres.FirstOrDefault(a => a.Id == id);
        }
    }
}
