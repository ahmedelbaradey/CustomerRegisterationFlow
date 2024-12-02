using AutoMapper;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Domain.Entities;

namespace CustomerRegisterationFlow.Application.Profiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerBasicInfoDto>().ReverseMap();
            CreateMap<CustomerBasicInfoDto, Customer>().ReverseMap();
        }
    }
}
