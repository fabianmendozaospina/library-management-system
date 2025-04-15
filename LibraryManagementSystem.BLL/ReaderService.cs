using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.BLL {
    public class ReaderService {
        private readonly ReaderRepository _readerRepository;

        public ReaderService(ReaderRepository readerRepository) {
            _readerRepository = readerRepository;
        }

        public async Task<List<Reader>> GetAllReaders() {
            return await _readerRepository.GetAllReaders();
        }

        public async Task<Reader> GetReaderById(int id) {
            return await _readerRepository.GetReaderById(id);
        }

        public async Task<Reader> AddReader(Reader reader) {
            return await _readerRepository.AddReader(reader);
        }

        public async Task UpdateReader(Reader reader) {
             await _readerRepository.UpdateReader(reader);
        }

        public async Task DeleteReader(int id) {
            Reader reader = await _readerRepository.GetReaderById(id);
            if (reader != null) {
                 await _readerRepository.DeleteReader(reader.ReaderId);
            }
        }

        public async Task<bool> ReaderExists(int id) {
            return await _readerRepository.ReaderExists(id);
        }
    }
}
