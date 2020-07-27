using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Playground.Swagger.WeatherForecast
{
    public static class SwaggerConfigurationExtensions
    {
        private const string ApiTitle = "Weather Forecast API";
        private const string ApiVersion = "v1";

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup => {
                setup.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Version = ApiVersion,
                    Title = ApiTitle,
                    Description = "API to consume weather foreacast",
                    Contact= new OpenApiContact
                    {
                        Name = "William Santos",
                        Email = "me@wsantos.dev",
                        Url = new Uri("https://wsantos.dev")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup => 
            {
                setup.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApiTitle);
                setup.RoutePrefix = string.Empty;
                setup.DocumentTitle = ApiTitle;
            });

            return app;
        }
    }
}