using Helpers;
using Microsoft.EntityFrameworkCore;
using TweetService.Application;
using TweetService.Data;
using TweetService.Data.Context;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TweetService.Data.Consumers;

namespace TweetService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyOrigin();
                    });
            });
            services.AddControllers();
            ;
            services.AddDbContext<TweetContext>(options =>
            {
                var connString = Configuration.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connString);
            });

            services.AddTransient<ITweetRepository, TweetRepository>();

            services.AddTransient<TweetApplication>();

            services.AddTransient<IEventSender, EventSender>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserFollowedConsumer>();
                x.AddConsumer<UserUnfollowedConsumer>();
                x.AddConsumer<ProfileCreatedConsumer>();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TweetContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            // global error handler
            app.UseMiddleware<ExceptionHandelingMiddleware>();
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            context.Database.Migrate();
        }
    }
}
