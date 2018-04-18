using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Security;

namespace RecipesAPI
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = SecurityConstants.Issuer,
                ValidAudience = SecurityConstants.Issuer,
                ClockSkew = TimeSpan.FromMinutes(10),
                IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(SecurityConstants.SecurityKey))
            };
        });

            services.AddMvc();

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();

            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(
                options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseAuthentication();

            app.UseMvc();
        }


    }
}
