using LibraryManagementSystem.BLL;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers {
    public class ReaderController : Controller {
        private readonly ReaderService _readerService;
        private readonly LoanService _loanService;

        public ReaderController(ReaderService readerService, LoanService loanService) {
            _readerService = readerService;
            _loanService = loanService;
        }

        public async Task<IActionResult> Index() {
            List<Reader> readers = await _readerService.GetAllReaders();
            return View(readers);
        }

        public async Task<IActionResult> Details(int id) {
            Reader reader = await _readerService.GetReaderById(id);
            if (reader == null)
                return NotFound();

            List<Loan> allLoans = await _loanService.GetAllLoans();
            List<Loan> readerLoans = allLoans.Where(l => l.ReaderId == id).ToList();

            ReaderLoan viewModel = new ReaderLoan {
                Reader = reader,
                Loans = readerLoans
            };

            return View(viewModel);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reader reader) {
            if (!ModelState.IsValid)
                return View(reader);

            await _readerService.AddReader(reader);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Edit(int id) {
            Reader reader = await _readerService.GetReaderById(id);
            if (reader == null)
                return NotFound();

            return View(reader);
        }

        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Edit(int id, Reader reader) {
            if (id != reader.ReaderId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(reader);

            await _readerService.UpdateReader(reader);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Delete(int id) {
            Reader reader = await _readerService.GetReaderById(id);
            if (reader == null)
                return NotFound();

            return View(reader);
        }

        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Delete(int id, IFormCollection form) {
            Reader reader = await _readerService.GetReaderById(id);
            if (reader == null)
                return NotFound();

            await _readerService.DeleteReader(reader.ReaderId);
            return RedirectToAction(nameof(Index));
        }
    }
}
