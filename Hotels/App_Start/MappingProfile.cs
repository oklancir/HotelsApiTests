using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;

namespace Hotels.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Guest, GuestDto>();
            CreateMap<GuestDto, Guest>().ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>().ForMember(r => r.Id, opt => opt.Ignore());
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>().ForMember(r => r.Id, opt => opt.Ignore());
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>().ForMember(i => i.Id, opt => opt.Ignore());
            CreateMap<Item, ItemDto>();
            CreateMap<ItemDto, Item>().ForMember(i => i.Id, opt => opt.Ignore());
        }
    }
}