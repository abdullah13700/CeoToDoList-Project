using CeoToDoList.Models.Domain;

namespace CeoToDoList.Repositories
{
    public interface IListRepositories
    {
        Task<List<CeoList>> GetAllAsync();
        Task<CeoList> GetByIdAsync(Guid id);

        Task<CeoList> CreateAsync(CeoList ceoList);

        Task<CeoList> DeleteAsync(Guid id);
    }
}
