using CommentService.Application;
using CommentService.Data;
using CommentService.Data.Context;
using Helpers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<CommentApp>();
builder.Services.AddDbContext<CommentContext>(options =>
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connString);
});
builder.Services.AddMassTransit(x =>
{
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

app.UseHttpsRedirection();
// global error handler
app.UseMiddleware<ExceptionHandelingMiddleware>();
app.UseAuthorization();

app.MapControllers();

using (var Scope = app.Services.CreateScope())
{
    var context = Scope.ServiceProvider.GetService<CommentContext>();
    context?.Database.Migrate();
}
app.Run();
