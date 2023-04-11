using MassTransit;
using MessagingModels;
using ReportService.DomainModels;

namespace ReportService.Application;

public class PostRejectedConsumer : IConsumer<IPostRejectedEvent>
{
    private readonly ReportApp app;

    public PostRejectedConsumer(ReportApp app)
    {
        this.app = app;
    }
    public async Task Consume(ConsumeContext<IPostRejectedEvent> context)
    {
        await this.app.Add(new Report(new Guid(), context.Message.Id, Guid.Empty, "Rejected by automatic scanning"));
    }
}