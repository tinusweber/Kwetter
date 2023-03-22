using Mapster;
using profileService.API.Models;

namespace ProfileService.Api
{
    public static class ProfileConversion
    {
        public static Profile AsDto(this Data.Models.ProfileData input, bool isBlocked)
        {
            TypeAdapterConfig<Data.Models.ProfileData, Profile>
             .NewConfig()
             .Map(dest => dest.Blocked,
                 src => isBlocked)
             .Map(dest => dest.DisplayName,
                 src => src.Username);

            return input.Adapt<Profile>();
        }
    }
}
