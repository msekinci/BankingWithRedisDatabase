using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;
using Tringle.Banking.Business.Containers.MicrosoftIoC;
using Tringle.Banking.DataAccess.Concrete.Context;

namespace Tringle.Banking.API
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
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("doc", new OpenApiInfo
                {
                    Title = "Tringle.Banking",
                    Description = "Tringle Banking API Document",
                    Contact = new OpenApiContact
                    {
                        Name = "Mehmet Serkan Ekinci",
                        Url = new Uri("https://www.linkedin.com/in/mehmet-serkan-ekinci-b231a412a/")
                    }
                });
            });

            services.AddDependencies();
            services.AddControllers().AddNewtonsoftJson().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opts.JsonSerializerOptions.IgnoreNullValues = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RedisContext redisContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            redisContext.Connect();

            app.UseSwagger();

            app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("/swagger/doc/swagger.json", "Tringle.Banking"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
