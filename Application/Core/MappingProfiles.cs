using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Maps properties inside the first class to the properties inside the second class.
            // In this case it only matches the property names.
            CreateMap<Activity, Activity>();
        }
    }
}