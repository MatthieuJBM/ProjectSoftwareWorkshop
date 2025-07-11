using AutoMapper;
using ProjectSoftwareWorkshop.Data;
using ProjectSoftwareWorkshop.Models.Category;
using ProjectSoftwareWorkshop.Models.Purchase;
using ProjectSoftwareWorkshop.Models.Shop;

namespace ProjectSoftwareWorkshop.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        // CreateMap<OneType, AnotherType>().ReverseMap();
        
        CreateMap<Category, CategoryDto>().ReverseMap();
        
        CreateMap<Shop, ShopDto>().ReverseMap();

        CreateMap<Purchase, PurchaseDto>().ReverseMap();
        CreateMap<Purchase, PurchaseCreateDto>().ReverseMap();
        
    }
}