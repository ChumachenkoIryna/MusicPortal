using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using System.Diagnostics;

namespace MusicPortal.Repositories
{
    public class SongRepository: ISongRepository
    {
        private readonly MusicContext _context;

        public SongRepository(MusicContext context)
        {
            _context = context;
        }
        public List<Song> GetSongsList()
        {
            List<Song> songs = _context.Songs.Include(p => p.Artist).Include(p => p.Genre).ToList();
            foreach(Song song in songs)
            {
                Console.WriteLine(song.Artist.Name);
                Console.WriteLine(song.Genre.Name);
            }
            return songs;

        }

        //public IQueryable<Song> GetSearchList(string name)
        //{
        //    return _context.Songs.Include(p => p.Artist).Include(p => p.Genre).Where(a => a.Name == name);
        //}
        public void Create(Song t)
        {
            _context.Songs.Add(t);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Song? t = _context.Songs.Find(id);
            if (t != null)
                _context.Songs.Remove(t);
            _context.SaveChanges();
        }

        public void Save()
        {
             _context.SaveChanges();
        }

        public Song GetSong(int? id)
        {
            var SongsContext = _context.Songs.Include(p => p.Artist).Include(p => p.Genre);
            Song song = _context.Songs.FirstOrDefault(a => a.Id == id);
            Console.WriteLine(song.Artist.Name);
            Console.WriteLine(song.Genre.Name);
            return song;
        }

        public void Update(Song t)
        {
            _context.Entry(t).State = EntityState.Modified;
        }

        //public async Task<List<Song>> GetSortedSongsList()
        //{
        //    var Songs = _context.Songs.Include(p => p.Artist).ToList();
        //    for (int i = 0; i < Songs.Count; i++)
        //    {
        //        for (int j = 0; j < Songs.Count - 1 - i; j++)
        //        {
        //            if (Songs[j].Name.CompareTo(Songs[j + 1].Name)>=1)
        //            {
        //                Song tmp = Songs[j];
        //                Songs[j] = Songs[j + 1];
        //                Songs[j + 1] = tmp;
        //            }
        //        }
        //    }
        //    return Songs;
        //}
    }
}
