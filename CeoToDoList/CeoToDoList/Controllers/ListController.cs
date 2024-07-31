
using AutoMapper;
using CeoToDoList.Models.Domain;
using CeoToDoList.Models.DTO;
using CeoToDoList.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CeoToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListRepositories listRepositories;
        private readonly IMapper mapper;

        public ListController(IListRepositories listRepositories, IMapper mapper)
        {
            this.listRepositories = listRepositories;
            this.mapper = mapper;
        }


        // Create list
        // POST: /api/lists
        /// <summary>
        /// Creates a new CEO To Do list based on the provided request data.
        /// </summary>
        /// <param name="addListReqDto">The data transfer object containing the details of the to-do list to be created. This object must include the Title.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation. If successful, returns an HTTP 200 OK status with the created to-do list data. If there are issues, returns an appropriate HTTP status code.
        /// </returns>
        /// <response code="200">Returns the newly created to-do list with a success message.</response>
        /// <response code="400">Returns a validation error if the input data is invalid.</response>
        /// <response code="500">Returns a server error if there is an unexpected issue during the list creation process.</response>
        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] AddListReqDto addListReqDto)
        {
            // Map DTO to domain 
            var listDomain = mapper.Map<CeoList>(addListReqDto);

            await listRepositories.CreateAsync(listDomain);

            return Ok(mapper.Map<AddListReqDto>(listDomain));

        }


        /// <summary>
        /// Retrieves all CEO To Do lists.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a list of to-do lists.
        /// If successful, returns an HTTP 200 OK status with the list of to-do lists.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllLists()
        {
            var result = await listRepositories.GetAllAsync();
            return Ok(mapper.Map<List<CeoListDto>>(result));
        }


        /// <summary>
        /// Retrieves a specific CEO To Do list by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the to-do list.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the to-do list.
        /// If successful, returns an HTTP 200 OK status with the to-do list.
        /// If the to-do list is not found, returns an HTTP 404 Not Found status.
        /// </returns>
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetListById([FromRoute] Guid id)
        {
            var listDomain = await listRepositories.GetByIdAsync(id);

            return Ok(mapper.Map<CeoListDto>(listDomain));
        }


        /// <summary>
        /// Deletes a specific to-do list by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the to-do list to delete.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If successful, returns an HTTP 200 OK status with the deleted to-do list.
        /// If the to-do list is not found, returns an HTTP 404 Not Found status.
        /// </returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var listDomain = await listRepositories.DeleteAsync(id);
            return Ok(mapper.Map<CeoListDto>(listDomain));
        }

    }
}
