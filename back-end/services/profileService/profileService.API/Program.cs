using Helpers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProfileService.Application;
using ProfileService.Data;
using ProfileService.Data.Consumers;
using ProfileService.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProfileContext>(options =>
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connString);
});

builder.Services.AddTransient<IProfileRepository, ProfileRepository>();

builder.Services.AddTransient<ProfileApp>();

builder.Services.AddTransient<IEventSender, EventSender>();
builder.Services.AddMassTransit(x =>
{

    x.AddConsumer<UserRegisteredConsmer>();

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

app.MapControllers();

using (var Scope = app.Services.CreateScope())
{
    var context = Scope.ServiceProvider.GetService<ProfileContext>();
    context?.Database.Migrate();
}

app.Run();
