using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repositories;

namespace MusicPortal.Controllers
{
    public class SongController : Controller
    {
        ISongRepository repo;
        IGenreRepository genreRepo;
        IArtistRepository artistRepo;
        IWebHostEnvironment _appEnvironment;


        public SongController(ISongRepository r, IGenreRepository gR,IArtistRepository aR, IWebHostEnvironment appEnvironment)
        {
            repo = r;
            genreRepo = gR;
            artistRepo = aR;
            _appEnvironment = appEnvironment;
        }

        public ActionResult Songs()
        {

            List<Song> songs = repo.GetSongsList();
            return View(songs);
        }

        public IActionResult Create()
        {
            ViewData["Artist"] = new SelectList(artistRepo.GetValues(), "Id", "Name");
            ViewData["Genre"] = new SelectList(genreRepo.GetValues(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ArtistId,GenreId,Path")] Song song, IFormFile uploadedFile, IFormFile uploadedImage)
        {
            if (ModelState.IsValid)
            {
                if (!uploadedFile.FileName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Path", "Only mp3 files are allowed.");
                    return View(song);
                }
                var postedFileExtension = Path.GetExtension(uploadedImage.FileName);
                if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Image", "Only image files are allowed.");
                    return View(song);
                }
                if (uploadedFile != null)
                {
                    string path = "/songs/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    song.Path = path;
                  
                }
                if(uploadedImage != null) {
                    string path = "/img/" + uploadedImage.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedImage.CopyToAsync(fileStream);
                    }
                    song.Image = path;

                }
                song.Artist = artistRepo.GetArtist(song.ArtistId);
                song.Genre = genreRepo.GetGenre(song.GenreId);
                repo.Create(song);
                return RedirectToAction(nameof(Songs));
            }
            return View(song);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = repo.GetSong(id);
            return View(song);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            repo.Delete(id);

            return RedirectToAction(nameof(Songs));
        }

      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = repo.GetSong(id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

       

    }
}
