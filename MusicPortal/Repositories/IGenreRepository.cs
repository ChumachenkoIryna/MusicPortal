using MusicPortal.Models;
using System.Collections;

namespace MusicPortal.Repositories
{
    public interface IGenreRepository
    {
        List<Genre> GetGenreList();
        IEnumerable GetValues();

        Genre GetGenre(int? id);
        void Create(Genre item);
        void Update(Genre item);
        void Delete(int id);
        void Save();
    }
}
