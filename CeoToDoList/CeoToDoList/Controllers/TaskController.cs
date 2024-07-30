
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

        public TaskController(IMapper mapper, ITaskRepositories taskRepositories)
        {
            this.mapper = mapper;
            this.taskRepositories = taskRepositories;
        }


        /// <summary>
        /// Creates a new task with the provided details.
        /// </summary>
        /// <param name="addTaskReqDto">The data transfer object containing the details of the task to create.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> that represents the result of the operation. 
        /// If successful, returns an HTTP 200 OK status with the created task data.
        /// If there are issues with the request, returns an appropriate HTTP status code.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] AddTaskReqDto addTaskReqDto)
        {
            var taskDomain = mapper.Map<CeoTask>(addTaskReqDto);

            await taskRepositories.CreateAsync(taskDomain);

            return Ok(mapper.Map<CeoTaskDto>(taskDomain));

        }


        /// <summary>
        /// Deletes a task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to delete.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If successful, returns an HTTP 200 OK status with the deleted task data.
        /// If the task is not found, returns an HTTP 404 Not Found status.
        /// </returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            var taskDomain = await taskRepositories.DeleteAsync(id);

            return Ok(mapper.Map<CeoTaskDto>(taskDomain));
        }


        /// <summary>
        /// Updates a task with the given identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="updateTaskReqDto">The updated task data.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation. 
        /// If successful, returns an HTTP 200 OK status with the updated task data.
        /// If the task is not found, returns an appropriate HTTP status code.
        /// </returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] UpdateTaskReqDto updateTaskReqDto)
        {
            var taskDomain = mapper.Map<CeoTask>(updateTaskReqDto);

            taskDomain = await taskRepositories.UpdateAsync(id, taskDomain);

            return Ok(mapper.Map<CeoTaskDto>(taskDomain));
        }


        /// <summary>
        /// Updates the completion status of a task with the given identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="updateCompleteTaskDto">The data to update the task's completion status.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation. 
        /// If successful, returns an HTTP 200 OK status with the updated task data.
        /// If the task is not found, returns an appropriate HTTP status code.
        /// </returns>
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

