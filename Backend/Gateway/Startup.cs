using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FS_Gateway
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
            services.AddAuthentication(c =>
                {
                    c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("TestKey", o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.Authority = Environment.GetEnvironmentVariable("AuthServer");
                    o.Audience = "myresourceapi";
                });

            services.AddOcelot();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            await app.UseOcelot();
        }
    }
}
