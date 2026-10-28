using Microsoft.AspNetCore.Mvc;

namespace MusicPortal.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using MusicPortal.BLL.DTO;
    using MusicPortal.BLL.Interfaces;
    using MusicPortal.Filters;
    using MusicPortal.Models;
    [Culture]
    public class GenreController(IGenreService genreService, IWebHostEnvironment app) : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("path", Request.Path);
            var genres = await genreService.GetListGenresAsync();
            return View(genres);
        }

        public IActionResult Create()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000_000)]
        public IActionResult Create(GenreDTO genreModel, IFormFile filePoster)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (!ModelState.IsValid || filePoster == null)
            {
                if (filePoster == null) ModelState.AddModelError("Poster", Resources.Resource.ChoosePoster);
                return View(genreModel);
            }
            var pathPoster = Path.Combine(app.WebRootPath, "Genre Poster", filePoster.FileName);
            FileStream stream = new FileStream(pathPoster, FileMode.Create);
            filePoster.CopyTo(stream);
            stream.Dispose();
            var genre = new GenreDTO { Name = genreModel.Name, Poster = filePoster.FileName };
            genreService.AddGenre(genre);
            return Redirect(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            var genre = genreService.GetGenreFirhstOrDefault(id);
            if (genre == null) return View();
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000)]
        public IActionResult Update(GenreDTO genreModel, IFormFile filePoster)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (genreModel.Name == null) return View(genreModel);
            var genre = genreService.GetGenreFirhstOrDefault(genreModel.Id);
            if (genre == null) return NotFound();
            genre.Name = genreModel.Name;
            if (filePoster != null)
            {
                var pathPoster = Path.Combine(app.WebRootPath, "Genre Poster", filePoster.FileName);
                FileStream stream = new FileStream(pathPoster, FileMode.Create);
                filePoster.CopyTo(stream);
                stream.Dispose();
                genre.Poster = filePoster.FileName;
            }
            genreService.UpdateGenre(genre);
            return RedirectToAction("Index", "Genre");
        }

        public IActionResult Delete(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            var genre = genreService.GetGenreFirhstOrDefault(id);
            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            var genre = genreService.GetGenreFirhstOrDefault(id);
            if (genre == null) return NotFound();
            genreService.RemoveGenre(genre);
            return RedirectToAction("Index", "Genre");
        }
    }
}
