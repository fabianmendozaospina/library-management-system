using LibraryManagementSystem.Common;
using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.BLL
{
    public class ReaderService
    {
        private readonly ReaderRepository _readerRepository;

        public ReaderService(ReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        public async Task<List<Reader>> GetAllReaders()
        {
            return await _readerRepository.GetAllReaders();
        }

        public async Task<Reader?> GetReaderById(int id)
        {
            Reader reader = await _readerRepository.GetReaderById(id);

            if (reader == null)
            {
                return null;
            }

            return reader;
        }

        public async Task<ServiceResult> AddReader(Reader reader)
        {
            try
            {
                Reader createdReader = await _readerRepository.AddReader(reader);

                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                (string message, string field) = Helper.GetMessage(this.GetType().Name, ex.Message, ex.InnerException);

                return ServiceResult.Fail($"{message}", field);
            }
        }

        public async Task<ServiceResult> UpdateReader(Reader reader)
        {
            try
            {
                await _readerRepository.UpdateReader(reader);

                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(Constants.NOT_FOUND_ERROR))
                {
                    return ServiceResult.Fail($"Reader {Constants.NOT_FOUND_ERROR}");
                }

                (string message, string field) = Helper.GetMessage(this.GetType().Name, ex.Message, ex.InnerException);

                return ServiceResult.Fail($"{message}", field);
            }
        }

        public async Task<ServiceResult> DeleteReader(Reader? reader)
        {
            try
            {
                await _readerRepository.DeleteReader(reader);

                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                (string message, string field) = Helper.GetMessage(this.GetType().Name, ex.Message, ex.InnerException);

                return ServiceResult.Fail($"{message}", field);
            }
        }
    }
}