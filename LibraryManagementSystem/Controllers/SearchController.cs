using LibraryManagementSystem.BLL;
using LibraryManagementSystem.Common.DTOs;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles = "Librarian,Reader")]
    public class SearchController : Controller
    {
        private readonly SearchService _searchService;
        private readonly BookService _bookService;

        public SearchController(SearchService searchService, BookService bookService)
        {
            _searchService = searchService;
            _bookService = bookService;
        }

        public IActionResult Search()
        {
            SetViewBag();

            return View(new SearchFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(SearchFormViewModel model)
        {
            SetViewBag();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            List<SearchResultDTO> resultDTO = await _searchService.SearchBooks(model.Value, model.Option);
            model.Results = resultDTO.Select(dto => new SearchResultViewModel
            {
                BookId = dto.BookId,
                Title = dto.Title,
                SubjectId = dto.SubjectId,
                Subject = dto.Subject,
                Synopsis = dto.Synopsis,
                Authors = dto.Authors,
                Available = dto.Available,
                Rate = dto.Rate,
                Comment = dto.Comment
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            Book? book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        private void SetViewBag()
        {
            ViewBag.Options = new List<string>() { "Title", "ISBN", "Author", "Editorial", "Edition Number", "Edition Year" };
        }
    }
}
