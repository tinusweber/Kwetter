using Helpers;
using MassTransit;
using MassTransit.Configuration;
using MessagingModels;
using ReportService.Application;
using ReportService.Data;
using ReportService.Data.Context;
using MongoDB.Driver;
using System.Xml.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyOrigin();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<ReportApp>();

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new MongoClient(connString);
});

builder.Services.AddScoped<ReportContext>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var databaseName = "report_mongodb"; // Replace with your desired database name

    var mongoClient = serviceProvider.GetService<IMongoClient>();
    var mongoDatabase = mongoClient.GetDatabase(databaseName);

    return new ReportContext(connectionString, databaseName);
});


builder.Services.AddMassTransit(x =>
{
    x.RegisterConsumer<PostRejectedConsumer>();
    x.UsingRabbitMq((cfx, cnf) =>
    {
        cnf.Host(Environment.GetEnvironmentVariable("RabbitMQConnectionString"));

        cnf.ConfigureEndpoints(cfx);
    });
});

builder.Services.Configure<MassTransitHostOptions>(options =>
{
    options.WaitUntilStarted = true;
    options.StartTimeout = TimeSpan.FromSeconds(30);
    options.StopTimeout = TimeSpan.FromMinutes(1);
});

var app = builder.Build();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// global error handler
app.UseMiddleware<ExceptionHandelingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var initialize = serviceProvider.GetService<ReportRepository>();
    var reportRepository = serviceProvider.GetService<IReportRepository>();
    initialize?.Initialize(); // Add a method in ReportRepository to handle initialization if required
}
app.Run();