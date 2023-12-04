using AutoMapper;

namespace ProductManagement.Application.Config.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config;
        }
    }
}
