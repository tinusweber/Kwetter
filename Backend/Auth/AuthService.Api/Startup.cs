using AuthService.Api.Constants;
using AuthService.Application;
using AuthService.Data;
using AuthService.Data.Context;
using AuthService.Model;
using Helpers;
using IdentityServer4.Services;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace AuthService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.Seq("http://localhost:80")
                .Enrich.FromLogContext()
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(logger, dispose: true);
            });

            services.AddCors();
            services.AddTransient<IProfileService, ProfileService>();

            services.AddDbContext<AuthContext>(options =>
            {
                var connString = Configuration.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connString);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp1", Version = "v1" });
            });

            services.AddMassTransit(x =>
            {
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

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthContext>()
                .AddDefaultTokenProviders();
            services.AddCors();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(Config.Config.GetApiResources())
             .AddInMemoryApiScopes(Config.Config.GetApiScopes())

             .AddAspNetIdentity<ApplicationUser>()
             .AddProfileService<ProfileService>()
             .AddInMemoryClients(Config.Config.GetClients());

            services.Configure<IdentityOptions>(options =>
            {

                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                //user options
                options.User.RequireUniqueEmail = true;
            });
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddControllers();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AuthContext context, RoleManager<IdentityRole> roleManager)
        {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "WebApp1 v1"));
            // global error handler
            app.UseMiddleware<ExceptionHandelingMiddleware>();
            app.UseRouting();
            app.UseCors(x => x.AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });

            SetupDb(context, roleManager).Wait();
        }

        public async Task SetupDb(AuthContext context, RoleManager<IdentityRole> roleManager)
        {
            context.Database.Migrate();

            if (!await roleManager.RoleExistsAsync(Roles.Admin))
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            if (!await roleManager.RoleExistsAsync(Roles.User))
                await roleManager.CreateAsync(new IdentityRole(Roles.User));
        }
    }
}
