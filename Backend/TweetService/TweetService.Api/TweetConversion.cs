using Mapster;
using TweetService.Api.Models.Requests;

namespace TweetService.Api
{
    public static class TweetConversion
    {
        public static DomainModel.Tweet AsDomainModel(this PostTweetRequest req, Guid tweeterId)
        {
            TypeAdapterConfig<PostTweetRequest, DomainModel.Tweet>
             .NewConfig()
             .Map(dest => dest.TweeterId,
                 src => tweeterId);


            return req.Adapt<DomainModel.Tweet>();
        }
    }
}
