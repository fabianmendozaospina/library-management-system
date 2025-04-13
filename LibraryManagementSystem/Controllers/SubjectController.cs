using LibraryManagementSystem.BLL;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles = "Librarian")]
    public class SubjectController : Controller
    {
        private readonly SubjectService _service;

        public SubjectController(SubjectService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var subjects = _service.GetAll();
            return View(subjects);
        }

        public IActionResult Details(int id)
        {
            var subject = _service.GetById(id);
            if (subject == null)
                return NotFound();

            return View(subject);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _service.Create(subject);
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        public IActionResult Edit(int id)
        {
            var subject = _service.GetById(id);
            if (subject == null)
                return NotFound();

            return View(subject);
        }

        [HttpPost]
        public IActionResult Edit(int id, Subject subject)
        {
            if (id != subject.SubjectId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _service.Update(subject);
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        public IActionResult Delete(int id)
        {
            var subject = _service.GetById(id);
            if (subject == null)
                return NotFound();

            return View(subject);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
