using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Reports.Application.Database;
using Reports.Application.Finders;
using Reports.Application.Interfaces;
using Reports.Application.Services;
using Reports.Core.Interfaces;
using Reports.Core.Services;

namespace Reports.Server
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
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reports.Server", Version = "v1" });
            });

            services.AddDbContext<ReportsDatabaseContext>(opt =>
            {
                opt.UseSqlite(@"Filename=..\Reports.sqlite");
                opt.EnableSensitiveDataLogging();
            });
            services.AddScoped<IEmployeeApplicationService, EmployeeApplicationService>();
            services.AddScoped<ITaskApplicationService, TaskApplicationService>();
            services.AddScoped<IReportApplicationService, ReportApplicationService>();
            services.AddScoped<IEmployeesFinder, EmployeesFinder>();
            services.AddScoped<IReportsFinder, ReportsFinder>();
            services.AddScoped<ISubordinatesFinder, SubordinatesFinder>();
            services.AddScoped<ITasksFinder, TasksFinder>();
            services.AddScoped<ICommentsFinder, CommentsFinder>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ITaskService, TaskService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reports.Server v1"));
            }

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