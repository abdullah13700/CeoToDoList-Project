using AutoMapper;
using CeoToDoList.Models.Domain;
using CeoToDoList.Models.DTO;

namespace CeoToDoList.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AddListReqDto,CeoList>().ReverseMap();
            CreateMap<AddTaskReqDto,CeoTask>().ReverseMap();
            CreateMap<CeoListDto, CeoList>().ReverseMap();
            CreateMap<CeoTaskDto, CeoTask>().ReverseMap();
            CreateMap<UpdateTaskReqDto, CeoTask>().ReverseMap();
            CreateMap<UpdateCompleteTaskDto, CeoTask>().ReverseMap();

        }
    }
}
