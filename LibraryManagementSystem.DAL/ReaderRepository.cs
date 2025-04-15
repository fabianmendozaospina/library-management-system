using LibraryManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DAL {
    public class ReaderRepository {
        private readonly LibraryDbContext _context;
        public ReaderRepository(LibraryDbContext context) {
            _context = context;
        }

        public async Task<List<Reader>> GetAllReaders() {
            return await _context.Readers
                .Include(r => r.Loans)
                .ToListAsync();
        }

        public async Task<Reader> GetReaderById(int id) {
            return await _context.Readers
                .Include(r => r.Loans)
                .SingleOrDefaultAsync(r => r.ReaderId == id);
        }

        public async Task<Reader> AddReader(Reader reader) {
            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();
            return reader;
        }

        public async Task UpdateReader(Reader reader) {
            Reader existingReader = await _context.Readers.FirstOrDefaultAsync(r => r.ReaderId == reader.ReaderId);
            if (existingReader == null) {
                throw new Exception("Reader not found");
            }
            existingReader.FirstName = reader.FirstName;
            existingReader.LastName = reader.LastName;
            existingReader.CoreId = reader.CoreId;
            existingReader.BirthDay = reader.BirthDay;
            existingReader.Loans = reader.Loans;
            existingReader.Ratings = reader.Ratings;
            _context.Readers.Update(existingReader);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReader(int id) {
            Reader reader = await _context.Readers.FindAsync(id);
            if (reader == null) {
                throw new Exception("Reader not found");
            }
            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ReaderExists(int id) {
            return await _context.Readers.AnyAsync(r => r.ReaderId == id);
        }
    }
}
