using AutoMapper;
using backend.Models;
namespace backend.DTOs.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDTO>()
                    .ForMember(dest => dest.Toppings,
                        opt => opt.MapFrom(src => src.OrderToppings.Select(ot => ot.PizzaTopping)));

            CreateMap<PizzaSize, PizzaSizeDTO>();
            CreateMap<PizzaTopping, PizzaToppingDTO>();

        }
    }
}
