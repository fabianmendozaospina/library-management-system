using LibraryManagementSystem.BLL;
using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly SubjectService _service;

        public SubjectsController(SubjectService service)
        {
            _service = service;
        }

        // GET: Subjects
        public IActionResult Index()
        {
            var subjects = _service.GetAll();
            return View(subjects);
        }

        // GET: Subjects/Details/5
        public IActionResult Details(int id)
        {
            var subject = _service.GetById(id);
            if (subject == null)
                return NotFound();

            return View(subject);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
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

        // GET: Subjects/Edit/5
        public IActionResult Edit(int id)
        {
            var subject = _service.GetById(id);
            if (subject == null)
                return NotFound();

            return View(subject);
        }

        // POST: Subjects/Edit/5
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

        // GET: Subjects/Delete/5
        public IActionResult Delete(int id)
        {
            var subject = _service.GetById(id);
            if (subject == null)
                return NotFound();

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
