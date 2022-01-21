using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.JwtBearer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                {
                    var secretBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);
                    var key = new SymmetricSecurityKey(secretBytes);
                    
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Constants.Issuer,
                        ValidAudience = Constants.Audience,
                        IssuerSigningKey = key
                    };
                });
            
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoint => endpoint.MapDefaultControllerRoute());
        }
    }
}