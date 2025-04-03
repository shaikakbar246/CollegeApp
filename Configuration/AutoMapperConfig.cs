
using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace CollegeApp.Configuration
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap<Student, StudentDTO>();
            //CreateMap<StudentDTO, Student>();
            //same property name in bothe the classess StudentDTO,Student
            //CreateMap<StudentDTO, Student>().ReverseMap(); 
            //it is "before Reverse map" Different property name in bothe the classess StudentDTO,Student. some cases it will not work
            //CreateMap<StudentDTO, Student>().ForMember(n=>n.StudentName,opt=>opt.MapFrom(x=>x.name)).ReverseMap();
            //it is "After Reverse map method" Different property name in bothe the classess StudentDTO,Student
            //CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n => n.name, opt => opt.MapFrom(x => x.StudentName));
            //ignore property
            //it is "ignore method" for Same property names but if we wnat to ignore mapping perticulaer property
            //CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n => n.StudentName, opt => opt.Ignore());
            //config transforming some property
            //CreateMap<StudentDTO, Student>().ReverseMap().AddTransform<string>(n => string.IsNullOrEmpty(n) ? "No Address Found" : n);
            CreateMap<StudentDTO, Student>().ReverseMap()
                .ForMember(n => n.Address, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address) ? "No Address Found" : n.Address));
                //.AddTransform<string>(n => string.IsNullOrEmpty(n) ? "No Address Found" : n);
            
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
