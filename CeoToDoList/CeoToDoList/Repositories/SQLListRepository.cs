using CeoToDoList.Data;
using CeoToDoList.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CeoToDoList.Repositories
{
    public class SQLListRepository : IListRepositories
    {
        private readonly CeoDbContext dbContext;

        public SQLListRepository(CeoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CeoList> CreateAsync(CeoList ceoList)
        {
            var dublicate = await dbContext.Lists.FirstOrDefaultAsync(x => x.Title == ceoList.Title);
            if (dublicate != null)
            {
                throw new InvalidOperationException("A list with this title already exist");
            }
            else if (ceoList.Title.IsNullOrEmpty())
            {
                throw new InvalidOperationException("The Title can't be Null or Empty");
            }
            else 
            {
                await dbContext.AddAsync(ceoList);
                await dbContext.SaveChangesAsync();
                return ceoList;
            }

        }

        public async Task<List<CeoList>> GetAllAsync()
        {
            return await dbContext.Lists.ToListAsync();
        }

        public async Task<CeoList> DeleteAsync(Guid id)
        {
            var result = await dbContext.Lists.Include(list=>list.Tasks).FirstOrDefaultAsync(x => x.Id == id);
            
            if (result == null)
            {
                throw new InvalidOperationException("There is no List with this Id");
            }

            var isAllTaskCompleted = result.Tasks.All(x => x.Completed);
            if (result.Tasks.IsNullOrEmpty() ||  isAllTaskCompleted)
            {
                dbContext.Lists.Remove(result);
                dbContext.SaveChanges();
                return result;
            }
            throw new InvalidOperationException("This list is not completed");
        }

        public async Task<CeoList> GetByIdAsync(Guid id)
        {
            var result = await dbContext.Lists.Include("Tasks").FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new InvalidOperationException("There is no List with this Id");

            }
            return result;
        }
    }
}
