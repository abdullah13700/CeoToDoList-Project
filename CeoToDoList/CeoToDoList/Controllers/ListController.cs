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

        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] AddListReqDto addListReqDto)
        {
            // Map DTO to domain 
            var listDomain = mapper.Map<CeoList>(addListReqDto);

            await listRepositories.CreateAsync(listDomain);

            return Ok(mapper.Map<AddListReqDto>(listDomain));

        }

        [HttpGet]
        public async Task<IActionResult> GetAllLists()
        {
            var result = await listRepositories.GetAllAsync();
            return Ok(mapper.Map<List<CeoListDto>>(result));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetListById([FromRoute] Guid id)
        {
            var listDomain = await listRepositories.GetByIdAsync(id);

            return Ok(mapper.Map<CeoListDto>(listDomain));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var listDomain = await listRepositories.DeleteAsync(id);
            return Ok(mapper.Map<CeoListDto>(listDomain));
        }

    }
}
