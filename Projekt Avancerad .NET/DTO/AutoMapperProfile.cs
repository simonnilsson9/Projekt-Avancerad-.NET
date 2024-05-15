using AutoMapper;
using Models.DTO;
using Models.Models;
using Projekt_Avancerad_.NET.Controllers;

namespace Projekt_Avancerad_.NET.DTO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTOAdd>().ReverseMap();
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
        }
    }
}
