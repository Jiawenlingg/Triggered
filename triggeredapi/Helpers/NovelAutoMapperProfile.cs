using AutoMapper;
using triggeredapi.Models;

internal class NovelAutoMapperProfile : Profile
{
    public NovelAutoMapperProfile()
    {
        CreateMap<Novel , NovelResult>();
        CreateMap<NovelResult , Novel>();
        
    }
}