using Mapster;

namespace ProfileService.Api
{
    public static class ProfileConversion
    {
        public static Profile AsDto(this Data.Models.Profile input, bool isBlocked)
        {
            TypeAdapterConfig<Data.Models.Profile, Profile>
             .NewConfig()
             .Map(dest => dest.Blocked,
                 src => isBlocked)
             .Map(dest => dest.DisplayName,
                 src => src.Username);

            return input.Adapt<Profile>();
        }
    }
}
