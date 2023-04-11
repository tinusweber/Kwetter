using MassTransit;
using PostScanner;
using PostScanner.Consumers;
using IHost = Microsoft.Extensions.Hosting.IHost;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PostPostedConsumer>();

            x.UsingRabbitMq((cfx, cnf) =>
            {
                cnf.Host(Environment.GetEnvironmentVariable("RabbitMQConnectionString"));
                cnf.ConfigureEndpoints(cfx);
            });
        });
        services.Configure<MassTransitHostOptions>(options =>
        {

            options.WaitUntilStarted = true;
            options.StartTimeout = TimeSpan.FromSeconds(30);
            options.StopTimeout = TimeSpan.FromMinutes(1);
        });
    })
    .Build();
await host.RunAsync();