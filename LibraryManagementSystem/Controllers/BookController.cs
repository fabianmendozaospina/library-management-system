using LibraryManagementSystem.BLL;
using LibraryManagementSystem.Common;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<Book> books = await _bookService.GetAllBooks();

            return View(books);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            Book? book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Librarian")]
        public IActionResult Create()
        {
            SetViewBag();

            return View(new Book());
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            SetViewBag();

            if (!ModelState.IsValid)
            {
                return View(book);
            }

            ServiceResult result = await _bookService.AddBook(book);

            if (!result.Success)
            {
                if (result.Field != "")
                    ModelState.AddModelError(result.Field, result.Message);
                else
                    TempData["Error"] = result.Message;

                return View(book);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Edit(int id)
        {
            SetViewBag();

            Book? book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            SetViewBag();

            if (!ModelState.IsValid)
            {
                return View(book);
            }

            ServiceResult result = await _bookService.UpdateBook(book);

            if (!result.Success)
            {
                if (result.Field != "")
                    ModelState.AddModelError(result.Field, result.Message);
                else
                    TempData["Error"] = result.Message;

                return View(book);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Delete(int id)
        {
            Book? book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Book? book = await _bookService.GetBookById(id);
            ServiceResult result = await _bookService.DeleteBook(book);

            if (!result.Success)
            {
                if (result.Field != "")
                    ModelState.AddModelError(result.Field, result.Message);
                else
                    TempData["Error"] = result.Message;

                return View(book);
            }

            return RedirectToAction("Index");
        }

        private void SetViewBag()
        {
            ViewBag.Subjects = new List<Subject>() { new Subject() { SubjectId = 1, Name = "Subject Temporal1" }, new Subject() { SubjectId = 2, Name = "Subject Temporal2" } };
            ViewBag.Editorials = new List<Editorial>() { new Editorial() { EditorialId = 1, Name = "Editorial Temporal1" }, new Editorial() { EditorialId = 2, Name = "Editorial Temporal2" } };
        }
    }
}