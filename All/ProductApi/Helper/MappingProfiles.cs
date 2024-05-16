using AutoMapper;
using ProductApi.DTOs;
using ProductData.Entites;

namespace ProductApi.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(x => x.Brand, o => o.MapFrom(S => S.Brand.Name))
                .ForMember(X => X.Type, o => o.MapFrom(s => s.Type.Name))
                .ForMember(x => x.PictureUrl, o => o.MapFrom<PictureUrlResolver>());
            
        }
    }
}
