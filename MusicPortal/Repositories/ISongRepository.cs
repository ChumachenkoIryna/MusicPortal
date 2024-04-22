using MusicPortal.Models;
using System.Collections;
using System.Diagnostics;

namespace MusicPortal.Repositories
{
    public interface ISongRepository
    {
        List<Song> GetSongsList();
        //Task<List<Song>> GetSortedSongsList();
        //IQueryable<Song> GetSearchList(string name);
        Song GetSong(int? id);
        void Create(Song item);
        void Delete(int id);
        void Update(Song t);
        void Save();
    }
}
