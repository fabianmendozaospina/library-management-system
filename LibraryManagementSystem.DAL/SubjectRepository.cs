using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.DAL
{
    public class SubjectRepository
    {
        private readonly LibraryDbContext _context;

        public SubjectRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public List<Subject> GetAll()
        {
            return _context.Subjects.ToList();
        }

        public Subject GetById(int id)
        {
            return _context.Subjects.FirstOrDefault(s => s.SubjectId == id);
        }

        public void Create(Subject subject)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();
        }

        public void Update(Subject subject)
        {
            _context.Subjects.Update(subject);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                _context.SaveChanges();
            }
        }
    }
}
