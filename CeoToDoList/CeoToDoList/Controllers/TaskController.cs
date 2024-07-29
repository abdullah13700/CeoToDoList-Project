using AutoMapper;
using CeoToDoList.Models.Domain;
using CeoToDoList.Models.DTO;
using CeoToDoList.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CeoToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITaskRepositories taskRepositories;

        public TaskController(IMapper mapper, ITaskRepositories taskRepositories )
        {
            this.mapper = mapper;
            this.taskRepositories = taskRepositories;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] AddTaskReqDto addTaskReqDto ) 
        {
            var taskDomain = mapper.Map<CeoTask>(addTaskReqDto);

            await taskRepositories.CreateAsync(taskDomain);

            return Ok(mapper.Map<CeoTaskDto>(taskDomain));
        
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            var taskDomain = await taskRepositories.DeleteAsync(id);

            return Ok(mapper.Map<CeoTaskDto>(taskDomain));
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] UpdateTaskReqDto updateTaskReqDto)
        {
            var taskDomain = mapper.Map<CeoTask>(updateTaskReqDto);

            taskDomain = await taskRepositories.UpdateAsync(id, taskDomain);

            return Ok(mapper.Map<CeoTaskDto>(taskDomain));
        }

        [HttpPut]
        [Route("complete/{id:guid}")]
        public async Task<IActionResult> UpdateTaskCompleted([FromRoute] Guid id, [FromBody] UpdateCompleteTaskDto updateCompleteTaskDto)
        {
            var taskDomain = mapper.Map<CeoTask>(updateCompleteTaskDto);

            taskDomain = await taskRepositories.UpdateCompletedAsync(id, taskDomain);

            return Ok(mapper.Map<CeoTaskDto>(taskDomain));
        }

    }
}
