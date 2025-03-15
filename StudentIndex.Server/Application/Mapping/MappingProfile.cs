using AutoMapper;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Predmeti, PredmetiDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.Status == null ? "Nema izlazaka na ispit" :
                    src.Status == "Polozeno" ? "Polozeno" : "Nepolozeno"));
            // Adjust mapping based on your Predmeti properties
        }

    }
}
