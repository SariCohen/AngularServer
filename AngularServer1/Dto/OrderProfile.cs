using AngularServer1.Modal;
using AngularServer1.Models;
using AutoMapper;

namespace AngularServer1.Dto
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>()
           .ForMember(dest => dest.presentId, opt => opt.MapFrom(src => src.presentId))
           .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId));
        }
}
}
