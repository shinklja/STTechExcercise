using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using STTechExercise.Configuration;
using System;

namespace STTechExercise
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Reimbursement Calculator",
                    Description = "Rest Api for calculating reimbursements.",
                    Contact = new OpenApiContact { Name = "Not Me", Email = "unknown@simplethread.com", Url = new Uri("https://www.simplethread.com") }
                });
            });
            var reimbursementValues = new ReimbursementValuesConfiguration();
            Configuration.Bind("ReimbursementValues", reimbursementValues);
            services.AddSingleton(reimbursementValues);
            services.AddSingleton<Services.ProjectReimbursementService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"v1/swagger.json", "Reimbursement Calculator V1");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
           
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
