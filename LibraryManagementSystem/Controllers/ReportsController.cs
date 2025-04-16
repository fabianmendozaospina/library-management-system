using System.Security.Claims;
using LibraryManagementSystem.BLL;
using LibraryManagementSystem.Common.DTOs;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ReportsService _reportsService;
        private readonly BookService _bookService;

        public ReportsController(ReportsService reportsService, BookService bookService)
        {
            _reportsService = reportsService;
            _bookService = bookService;
        }

        public IActionResult LoansPerReader()
        {
            LoansPerReaderFormViewModel model = new LoansPerReaderFormViewModel();

            if (!User.IsInRole("Librarian"))
            {
                model.Email = User.FindFirstValue(ClaimTypes.Email);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoansPerReader(LoansPerReaderFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            List<LoansPerReaderResultDTO> resultDTO = await _reportsService.LoansPerReader(model.ReaderFullName, model.Email);
            model.Results = resultDTO.Select(dto => new LoansPerReaderResultViewModel
            {
                ReaderFullName = dto.ReaderFullName,
                Email = dto.Email,
                Age = dto.Age,
                Loans = dto.Loans
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
    }
}
