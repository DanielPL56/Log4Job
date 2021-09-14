using AutoMapper;
using Log4Job.DTOs;
using Log4Job.Models;
using System;

namespace Log4Job.App_Start
{
    public class MappingProfile : Profile
    {
        private MapperConfiguration _config;

        public MappingProfile()
        {
            _config = new MapperConfiguration(cfg => 
            {
                cfg.AllowNullCollections = true;
                cfg.CreateMap<Project, ProjectDTO>().ReverseMap().ForMember(p => p.ProjectId, options => options.Ignore());
                cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap();
                cfg.CreateMap<TimeReport, TimeReportDTO>().ReverseMap();
            });

                _config.AssertConfigurationIsValid();
        }

        public static IMapper CreateMapper()
        {
            MappingProfile profile = new MappingProfile();
            return profile._config.CreateMapper();
        }
    }
}