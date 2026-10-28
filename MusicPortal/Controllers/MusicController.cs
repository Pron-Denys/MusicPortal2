using Microsoft.AspNetCore.Mvc;

namespace MusicPortal.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using MusicPortal.BLL.DTO;
    using MusicPortal.BLL.Interfaces;
    using MusicPortal.BLL.Services;
    using MusicPortal.Filters;
    using MusicPortal.Models;
    
    [Culture]
    public class MusicController(IMusicService musicService, IGenreService genreService,IWebHostEnvironment app) : Controller
    {
        public async Task<IActionResult> Index(string? nameMusic, int idGenre, int page = 1, SortState sortorder = SortState.NameAsc)
        {
            HttpContext.Session.SetString("path", Request.Path);
            const int maxElementCount = 5;
            IEnumerable<MusicDTO>? musics = await musicService.GetIncludeToListMusics();
            musics = sortorder switch
            {
                SortState.NameAsc => musics?.OrderBy(m => m.Name).ToList(),
                SortState.NameDesc => musics?.OrderByDescending(m => m.Name).ToList(),
                SortState.SongwriterAsc => musics?.OrderBy(m => m.Songwriter).ToList(),
                SortState.SongwriterDesc => musics?.OrderByDescending(m => m.Songwriter).ToList(),
                _ => musics?.OrderBy(m => m.Name).ToList()
            };
            if (nameMusic != null)
                musics = musics?.Where(m => m.Name == nameMusic).ToList();
            if (idGenre != 0)
            {
                var genre = genreService.GetGenreFirhstOrDefault(idGenre);
                musics = musics?.Where(m => m.Genre == genre.Name).ToList();
            }
            int count = musics!.Count();
            musics = musics?.Skip((page - 1) * maxElementCount).Take(maxElementCount).ToList();
            var genres = await genreService.GetListGenresAsync();
            GenreDTO? selectGenre = genreService.GetGenreFirhstOrDefault(idGenre!);
            DataFilteringViewModel filter = new DataFilteringViewModel(nameMusic!, genres!, selectGenre! );
            PaginationViewModel pagination = new PaginationViewModel(page, count, maxElementCount);
            SortViewModel sort = new SortViewModel(sortorder);
            IndexViewModel model = new IndexViewModel { Page = pagination, Musics = musics!, Filter = filter, Sort = sort };
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            HttpContext.Session.SetString("path", Request.Path);
            ViewBag.Genres = await genreService.GetListGenresAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000_000)]
        public async Task<IActionResult> Create(MusicModel musicModel, IFormFile fileAudio, IFormFile filePoster)
        {
            if (!ModelState.IsValid || fileAudio == null || filePoster == null)
            {
                ViewBag.Genres = await genreService.GetListGenresAsync();
                if (fileAudio == null) ModelState.AddModelError("Audio", Resources.Resource.ChooseSong);
                if (filePoster == null) ModelState.AddModelError("Poster", Resources.Resource.ChoosePoster);
                return View(musicModel);
            }
            GenreDTO genre = genreService.GetGenreFirhstOrDefault(musicModel.Genre!);
            var pathAudio = Path.Combine(app.WebRootPath, "Music", fileAudio!.FileName);
            FileStream stream = new FileStream(pathAudio, FileMode.Create);
            fileAudio.CopyTo(stream);
            stream.Dispose();
            var pathPoster = Path.Combine(app.WebRootPath, "Poster", filePoster!.FileName);
            stream = new FileStream(pathPoster, FileMode.Create);
            filePoster!.CopyTo(stream);
            stream.Dispose();
            var music = new MusicDTO { Name = musicModel.Name, Songwriter = musicModel.Songwriter, Audio = fileAudio.FileName, Poster = filePoster.FileName, Genre = genre.Name, Year = musicModel.Year };
            musicService.AddMusic(music);
            return RedirectToAction("Index", "Music");
        }

        public async Task<IActionResult> Update(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            var musics = await musicService.GetIncludeToListMusics();
            MusicDTO music = null!;
            foreach (var m in musics!)
            {
                if (m.Id == id) music = m;
            }
            if (music == null) return RedirectToAction("Index", "Music");
            MusicModel model = new MusicModel { Id = music.Id, Name = music.Name, Genre = music?.Genre, Songwriter = music?.Songwriter, Year = music!.Year };
            ViewBag.Genres = await genreService.GetListGenresAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000_000)]
        public async Task<IActionResult> Update(MusicModel musicModel, IFormFile fileAudio, IFormFile filePoster)
        {
            if (musicModel.Songwriter == null || musicModel.Year == 0 || musicModel.Genre == null || musicModel.Name == null)
            {
                ViewBag.Genres = await genreService.GetListGenresAsync();
                return View(musicModel);
            }
            string audio = null!;
            string poster = null!;
            if (fileAudio == null || filePoster == null)
            {
                var temp_music = musicService.GetMusicFirstOrDefault(musicModel.Id);
                if (fileAudio == null) audio = temp_music?.Audio!;
                if (filePoster == null) poster = temp_music?.Poster!;
            }
            var musics = await musicService.GetIncludeToListMusics();
            MusicDTO music = null!;
            foreach (var m in musics!)
            {
                if (m.Id == musicModel.Id) music = m;
            }
            if (music == null) return RedirectToAction("Index", "Music");
            GenreDTO genre = genreService.GetGenreFirhstOrDefault(musicModel.Genre);
            if (fileAudio != null)
            {
                var pathAudio = Path.Combine(app.WebRootPath, "Music", fileAudio!.FileName);
                FileStream stream = new FileStream(pathAudio, FileMode.Create);
                fileAudio.CopyTo(stream);
                stream.Dispose();
                music.Poster = fileAudio.FileName;
            }
            if (filePoster != null)
            {
                var pathPoster = Path.Combine(app.WebRootPath, "Poster", filePoster!.FileName);
                FileStream stream = new FileStream(pathPoster, FileMode.Create);
                filePoster!.CopyTo(stream);
                stream.Dispose();
                music.Audio = filePoster.FileName;
            }         
            music.Name = musicModel.Name;
            music.Year = musicModel.Year;
            music.Genre = genre.Name;
            musicService.UpdateMusic(music);
            return RedirectToAction("Index", "Music");
        }

        public IActionResult Delete(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            var music = musicService.GetMusicFirstOrDefault(id);
            return View(music);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            var music = musicService.GetMusicFirstOrDefault(id);
            musicService.RemoveMusic(music);
            return RedirectToAction("Index", "Music");
        }
    }
}
