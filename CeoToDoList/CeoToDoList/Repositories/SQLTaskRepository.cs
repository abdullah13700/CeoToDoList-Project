using CeoToDoList.Data;
using CeoToDoList.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CeoToDoList.Repositories
{
    public class SQLTaskRepository: ITaskRepositories
    {
        private readonly CeoDbContext dbContext;

        public SQLTaskRepository(CeoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CeoTask> CreateAsync(CeoTask ceoTask)
        {
            var dublicate = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Title == ceoTask.Title);
            if (dublicate != null)
            {
                throw new InvalidOperationException("A Task with this title already exist");
            }
            else
            {
                await dbContext.AddAsync(ceoTask);
                await dbContext.SaveChangesAsync();
                return ceoTask;
            }
        }


        public async Task<CeoTask?> DeleteAsync(Guid id)
        {
            var result = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new InvalidOperationException("There is no Task with this Id to delete");
            }
            dbContext.Tasks.Remove(result);
            dbContext.SaveChanges();
            return result;
        }

        public async Task<CeoTask> GetTaskByIdAsync(Guid id)
        {
            var result = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new InvalidOperationException("There is no Task with this Id to find");

            }
            return result;
        }

        public async Task<CeoTask> UpdateAsync(Guid id, CeoTask ceoTask)
        {
            var oldTask = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (oldTask == null)
            {
                throw new InvalidOperationException("There is no Task with this Id to update");
            }

            oldTask.ListId = ceoTask.ListId;
            oldTask.Description = ceoTask.Description;
            oldTask.Title = ceoTask.Title;

            await dbContext.SaveChangesAsync();
            return ceoTask;
            
        }
        

        public async Task<CeoTask> UpdateCompletedAsync(Guid id, CeoTask ceoTask)
        {
            var oldTask = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (oldTask.Completed == ceoTask.Completed) 
            {
                throw new InvalidOperationException("The task is already the same value you enterd");

            }
            if (oldTask == null)
            {
                throw new InvalidOperationException("There is no List with this Id to update completed");
            }

            oldTask.Completed = ceoTask.Completed;

            await dbContext.SaveChangesAsync();
            return ceoTask;
        }
    }
}
