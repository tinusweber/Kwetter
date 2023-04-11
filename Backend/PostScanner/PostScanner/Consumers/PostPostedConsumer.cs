using Google.Cloud.Language.V1;
using MassTransit;
using MessagingModels;

namespace PostScanner.Consumers;

public class PostPostedConsumer : IConsumer<IPostReadyForReviewEvent>
{
    public async Task Consume(ConsumeContext<IPostReadyForReviewEvent> context)
    {
        await Console.Out.WriteLineAsync($"Post {context.Message.PostId} with message {context.Message.Body} is ready for review");


        if (!GoogleCloudClient.IsApproved(context.Message.Body))
        {
            await Console.Out.WriteLineAsync($"Post {context.Message.PostId} is not suitable for kwetter");
            await context.Publish<IPostRejectedEvent>(new
            {
                Id = context.Message.PostId
            });
        }
        else
        {
            await Console.Out.WriteLineAsync($"Post {context.Message.PostId} is suitable for kwetter");
        }
    }

}