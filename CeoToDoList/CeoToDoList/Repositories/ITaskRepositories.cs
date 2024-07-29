using CeoToDoList.Models.Domain;

namespace CeoToDoList.Repositories
{
    public interface ITaskRepositories
    {
        Task<CeoTask> GetTaskByIdAsync(Guid id);
        Task<CeoTask> CreateAsync(CeoTask ceoTask);
        Task<CeoTask?> DeleteAsync(Guid id);
        Task<CeoTask> UpdateCompletedAsync(Guid id, CeoTask ceoTask);
        Task<CeoTask> UpdateAsync(Guid id, CeoTask ceoTask);
    }
}
