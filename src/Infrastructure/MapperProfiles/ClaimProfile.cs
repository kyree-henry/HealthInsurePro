using HealthInsurePro.Contract.ClaimContracts;

namespace HealthInsurePro.Infrastructure.MapperProfiles
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Claim, CreateClaimModel>().ReverseMap();
            CreateMap<Claim, ClaimModel>()
                                .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.Expenses))
                                .ReverseMap();
        }
    }
}
