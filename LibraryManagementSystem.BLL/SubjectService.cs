using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.BLL
{
    public class SubjectService
    {
        private readonly SubjectRepository _repository;

        public SubjectService(SubjectRepository repository)
        {
            _repository = repository;
        }

        public List<Subject> GetAll() => _repository.GetAll();

        public Subject GetById(int id) => _repository.GetById(id);

        public void Create(Subject subject) => _repository.Create(subject);

        public void Update(Subject subject) => _repository.Update(subject);

        public void Delete(int id) => _repository.Delete(id);
    }
}
