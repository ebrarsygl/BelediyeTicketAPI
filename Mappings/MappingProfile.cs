using AutoMapper;
using BelediyeTicketAPI.DTOs.Category;
using BelediyeTicketAPI.DTOs.Ticket;
using BelediyeTicketAPI.Models;

namespace BelediyeTicketAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

        // Ticket
        CreateMap<Ticket, TicketDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category!.Name));

        CreateMap<CreateTicketDto, Ticket>();
        CreateMap<UpdateTicketDto, Ticket>();
    }
}