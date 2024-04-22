using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using MusicPortal.Repositories;

namespace MusicPortal.Controllers
{
    public class ArtistController : Controller
    {
        IArtistRepository repo;


        public ArtistController(IArtistRepository r)
        {
            repo = r;
        }

        public ActionResult Artists()
        {

            List<Artist> artists = repo.GetArtistList();
            return View(artists);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Artist artist)
        {
            if (ModelState.IsValid)
            {
                repo.Create(artist);
                return RedirectToAction(nameof(Artists));
            }
            return View(artist);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = repo.GetArtist(id);
            return View(artist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            repo.Delete(id);

            return RedirectToAction(nameof(Artists));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var artist = repo.GetArtist(id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Artist artist)
        {

            if (ModelState.IsValid)
            {
                repo.Update(artist);
                return RedirectToAction(nameof(Artists));
            }
            return View(artist);
        }


    }
}
