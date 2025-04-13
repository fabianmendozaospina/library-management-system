using LibraryManagementSystem.Common;
using LibraryManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL
{
    public class AuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _context.Authors
            .ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _context.Authors
                .SingleOrDefaultAsync(r => r.AuthorId == id);
        }

        public async Task<Author> AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }


        public async Task UpdateAuthor(Author author)
        {
            Author existingAuthor = await _context.Authors.FirstOrDefaultAsync(r => r.AuthorId == author.AuthorId);

            if (existingAuthor == null)
            {
                throw new Exception(Constants.NOT_FOUND_ERROR);
            }

            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.BirthDate = author.BirthDate;
            existingAuthor.DateOfDeath = author.DateOfDeath;
            existingAuthor.Biography = author.Biography;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(Author? author)
        {
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }
    }
}
