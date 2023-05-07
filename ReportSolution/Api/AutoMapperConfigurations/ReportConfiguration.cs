using Api.Models;
using AutoMapper;
using Model.Entities;

namespace Api.AutoMapperConfigurations
{
    public class ReportConfiguration : Profile
    {
        public ReportConfiguration()
        {
            CreateMap<Report, ReportListDto>().ReverseMap();
        }
    }
}
