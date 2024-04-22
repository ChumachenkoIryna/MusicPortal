using MusicPortal.Models;
using System.Collections;

namespace MusicPortal.Repositories
{
    public interface IArtistRepository
    {
        List<Artist> GetArtistList();
        IEnumerable GetValues();

        Artist GetArtist(int? id);
        void Create(Artist item);
        void Update(Artist item);
        void Delete(int id);

        void Save();
    }
}
