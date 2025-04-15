using LibraryManagementSystem.Common;
using LibraryManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL
{
    public class ReaderRepository
    {
        private readonly LibraryDbContext _context;

        public ReaderRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reader>> GetAllReaders()
        {
            return await _context.Readers
                .Include(r => r.Loans)
                .Include(r => r.Ratings)
                .ToListAsync();
        }

        public async Task<Reader> GetReaderById(int id)
        {
            return await _context.Readers
                .Include(r => r.Loans)
                .Include(r => r.Ratings)
                .SingleOrDefaultAsync(r => r.ReaderId == id);
        }

        public async Task<Reader> AddReader(Reader reader)
        {
            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();
            return reader;
        }


        public async Task UpdateReader(Reader reader)
        {
            Reader? existingReader = await _context.Readers
                                            .Include(r => r.Loans)
                                            .Include(r => r.Ratings)
                                            .FirstOrDefaultAsync(r => r.ReaderId == reader.ReaderId);

            if (existingReader == null)
            {
                throw new Exception(Constants.NOT_FOUND_ERROR);
            }

            existingReader.FirstName = reader.FirstName;
            existingReader.LastName = reader.LastName;
            existingReader.Email = reader.Email;
            existingReader.Phone = reader.Phone;
            existingReader.BirthDate = reader.BirthDate;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteReader(Reader? reader)
        {
            if (reader != null)
            {
                _context.Readers.Remove(reader);
                await _context.SaveChangesAsync();
            }
        }
    }
}
