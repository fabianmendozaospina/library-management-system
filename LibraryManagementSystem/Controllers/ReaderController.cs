using LibraryManagementSystem.BLL;
using LibraryManagementSystem.Common;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles = "Librarian")]
    public class ReaderController : Controller
    {
        private readonly ReaderService _readerService;

        public ReaderController(ReaderService readerService)
        {
            _readerService = readerService;
        }

        public async Task<IActionResult> Index()
        {
            List<Reader> readers = await _readerService.GetAllReaders();

            return View(readers);
        }

        public async Task<IActionResult> Details(int id)
        {
            Reader? reader = await _readerService.GetReaderById(id);

            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        public async Task<IActionResult> Create()
        {
            return View(new Reader());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reader reader)
        {
            if (!ModelState.IsValid)
            {
                return View(reader);
            }

            ServiceResult result = await _readerService.AddReader(reader);

            if (!result.Success)
            {
                if (result.Field != "") 
                    ModelState.AddModelError(result.Field, result.Message);
                else
                    TempData["Error"] = result.Message;

                return View(reader);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Reader? reader = await _readerService.GetReaderById(id);

            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Reader reader)
        {
            if (!ModelState.IsValid)
            {
                return View(reader);
            }

            ServiceResult result = await _readerService.UpdateReader(reader);

            if (!result.Success)
            {
                if (result.Field != "")
                    ModelState.AddModelError(result.Field, result.Message);
                else
                    TempData["Error"] = result.Message;

                return View(reader);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Reader? reader = await _readerService.GetReaderById(id);

            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Reader? reader = await _readerService.GetReaderById(id);
            ServiceResult result = await _readerService.DeleteReader(reader);

            if (!result.Success)
            {
                if (result.Field != "")
                    ModelState.AddModelError(result.Field, result.Message);
                else
                    TempData["Error"] = result.Message;

                return View(reader);
            }

            return RedirectToAction("Index");
        }
    }
}
