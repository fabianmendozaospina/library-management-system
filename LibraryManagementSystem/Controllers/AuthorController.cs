﻿using System.Diagnostics;
using LibraryManagementSystem.BLL;
using LibraryManagementSystem.Common;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<Author> authors = await _authorService.GetAllAuthors();

            return View(authors);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            Author? author = await _authorService.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [Authorize(Roles = "Librarian")]
        public IActionResult Create()
        {
            return View(new Author());
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            ServiceResult result = await _authorService.AddAuthor(author);

            if (!result.Success)
            {
                if (result.Field != "") 
                    ModelState.AddModelError(result.Field, result.Message);
                else
                    TempData["Error"] = result.Message;

                return View(author);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Edit(int id)
        {
            Author? author = await _authorService.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            ServiceResult result = await _authorService.UpdateAuthor(author);

            if (!result.Success)
            {
                if (result.Field != "")
                    ModelState.AddModelError(result.Field, result.Message);
                else
                    TempData["Error"] = result.Message;

                return View(author);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Delete(int id)
        {
            Author? author = await _authorService.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Author? author = await _authorService.GetAuthorById(id);
            ServiceResult result = await _authorService.DeleteAuthor(author);

            if (!result.Success)
            {
                if (result.Field != "")
                    ModelState.AddModelError(result.Field, result.Message);
                else
                    TempData["Error"] = result.Message;

                return View(author);
            }

            return RedirectToAction("Index");
        }
    }
}
