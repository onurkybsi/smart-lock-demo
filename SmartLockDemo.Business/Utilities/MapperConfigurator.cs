using AutoMapper;
using SmartLockDemo.Business.Service.Administration;
using SmartLockDemo.Business.Service.User;

namespace SmartLockDemo.Business.Utilities
{
    internal static class MapperConfigurator
    {
        private static readonly Mapper Mapper = new Mapper(new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<Data.Entities.User, User>()
                    .ForMember(dest => dest.Tags, opt => opt.MapFrom(source => source.UserTags));
                cfg.CreateMap<Data.Entities.Door, Door>();
                cfg.CreateMap<Data.Entities.Tag, Tag>()
                    .ForMember(dest => dest.Doors, opt => opt.MapFrom(source => source.TagDoors));
                cfg.CreateMap<Data.Entities.UserTag, Tag>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.TagId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Tag.Name))
                    .ForMember(dest => dest.Doors, opt => opt.MapFrom(source => source.Tag.TagDoors));
                cfg.CreateMap<Data.Entities.TagDoor, Door>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.DoorId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Door.Name));
            }
        ));

        public static TDest MapTo<TDest>(this object src)
            => (TDest)Mapper.Map<TDest>(src);
    }
}
