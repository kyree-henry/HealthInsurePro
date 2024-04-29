using HealthInsurePro.Contract.PolicyHolderContracts;
using HealthInsurePro.Infrastructure.Extensions;

namespace HealthInsurePro.Infrastructure.MapperProfiles
{
    public class PolicyHolderProfile : Profile
    {
        public PolicyHolderProfile() 
        {
            CreateMap<PolicyHolder, CreatePolicyHolderModel>().ReverseMap();
            CreateMap<PolicyHolder, PolicyHolderModel>()
                                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                                .ForMember(dest => dest.FullNames, opt => opt.MapFrom(src => $"{src.SurName} {src.FirstName}"))
                                .ReverseMap();
        }
    }
}