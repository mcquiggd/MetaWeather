using System;

using MetaWeather.Api.Models;
using MetaWeather.Application;
using MetaWeather.Core.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetaWeather Api V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
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


            services.AddRefitClient<IMetaWeatherService>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(apiOptions.ApiUrl);
                });

            services.AddScoped<IApiProxy, ApiProxy>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                             new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "MetaWeather Api",
                        Description = "Sample Api for Assesment task",
                        //TermsOfService = new Uri("https://example.com/terms"),
                        Contact =
                    new OpenApiContact
                            {
                                Name = "David McQuiggin",
                                Email = string.Empty,
                                Url = new Uri("https://twitter.com/mcquiggd"),
                            },
                        License =
                    new OpenApiLicense
                            { Name = "Use under LICX", Url = new Uri("https://example.com/license"), }
                    });
            });
        }

        public IConfiguration Configuration { get; }
    }
}