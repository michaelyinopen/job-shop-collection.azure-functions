using AutoMapper;
using job_shop_collection.Data.Models;

namespace job_shop_collection.AzureFunctions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JobSet, JobSetHeaderDto>();
            CreateMap<JobSet, JobSetDto>();
            CreateMap<NewJobSetDto, JobSet>();
            CreateMap<UpdateJobSetDto, JobSet>();
        }
    }
}
