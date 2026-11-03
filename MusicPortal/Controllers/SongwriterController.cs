using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;

namespace MusicPortal.Controllers
{
    public class SongwriterController(ISongwriterService songwriterService, IWebHostEnvironment appEnvironment) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var songwriters = await songwriterService.GetListSongwritersAsync();
            return View(songwriters);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000_000)]
        public async Task<IActionResult> Create(SongwriterDTO songwriter, IFormFile filePhoto)
        {
            if (songwriter.Name == null || filePhoto == null)
            {
                return View();
            }
            string path = $"{appEnvironment.WebRootPath}/Songwriter Photo/{filePhoto.FileName}";
            FileStream stream = new FileStream(path, FileMode.Create);
            await filePhoto.CopyToAsync(stream);
            stream.Dispose();
            songwriter.Photo = filePhoto.FileName;
            songwriterService.AddSongwriter(songwriter);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var songwriter = songwriterService.GetSongwriter(id);
            return View(songwriter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000_000)]
        public async Task<IActionResult> Update(SongwriterDTO songwriter, IFormFile filePhoto)
        {
            if (songwriter.Name == null)
            {
                return View();
            }
            if (filePhoto != null)
            {
                string path = $"{appEnvironment.WebRootPath}/Songwriter Photo/{filePhoto.FileName}";
                FileStream stream = new FileStream(path, FileMode.Create);
                await filePhoto.CopyToAsync(stream);
                stream.Dispose();
                songwriter.Photo = filePhoto.FileName;
            }
            else
            {
                var tempSongwriter = songwriterService.GetSongwriter(songwriter.Id);
                songwriter.Photo = tempSongwriter.Photo;
            }
            songwriterService.UpdateSongwriter(songwriter);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var songwriter = songwriterService.GetSongwriter(id);
            return View(songwriter);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            var songwriter = songwriterService.GetSongwriter(id);
            songwriterService.RemoveSongwriter(songwriter);
            return RedirectToAction("Index");
        }
    }
}
