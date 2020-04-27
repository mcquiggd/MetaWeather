using System;

using MetaWeather.Api.Models;
using MetaWeather.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace MetaWeather.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRefitClient<IMetaWeatherService>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://www.metaweather.com");
                });

            services.AddControllers();

            // An alternative to IOptions, that enables access within the ConfigureService method
            var apiOptions = new ApiOptions();
            Configuration.GetSection("ApiOptions").Bind(apiOptions);

            services.AddSingleton(apiOptions);

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer",
                              options =>
                {
                    options.Authority = apiOptions.Authority;
                    options.RequireHttpsMetadata = true;

                    options.Audience = apiOptions.Audience;
                });
        }

        public IConfiguration Configuration { get; }
    }
}