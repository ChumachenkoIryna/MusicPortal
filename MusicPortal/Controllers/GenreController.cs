using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repositories;

namespace MusicPortal.Controllers
{
    public class GenreController : Controller
    {
        IGenreRepository repo;


        public GenreController(IGenreRepository r)
        {
            repo = r;
        }

        public ActionResult Genres()
        {

            List<Genre> genres =repo.GetGenreList();
            return View(genres);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                repo.Create(genre);
                return RedirectToAction(nameof(Genres));
            }
            return View(genre);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = repo.GetGenre(id);
            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            repo.Delete(id);

            return RedirectToAction(nameof(Genres));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = repo.GetGenre(id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Genre genre)
        {

            if (ModelState.IsValid)
            {
                repo.Update(genre);
                return RedirectToAction(nameof(Genres));
            }
            return View(genre);
        }


    }
}
