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
                cfg.CreateMap<Data.Entities.User, User>();
                cfg.CreateMap<Data.Entities.Door, Door>();
                cfg.CreateMap<Data.Entities.Tag, Tag>();
            }
        ));

        public static TDest MapTo<TDest>(this object src)
            => (TDest)Mapper.Map<TDest>(src);
    }
}
