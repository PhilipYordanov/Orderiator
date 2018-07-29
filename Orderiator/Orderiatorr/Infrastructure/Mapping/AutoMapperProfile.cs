using AutoMapper;
using Orderiator.Web.Models;
using Orderiator.Web.ViewModels.CustomerViewModels;

namespace Orderiator.Web.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<CustomerDTO, CustomersViewModel>()
                    .ForMember(x => x.OrderCount, opt => opt.MapFrom(src => src.Orders.Count));
        }
    }
}
