using BoardService.AppSettings;
using BoardService.Data;
using BoardService.Helper;
using BoardService.Repository.Implementation;
using BoardService.Repository.Interfaces;
using BoardService.ServiceImplementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService
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

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(ManageMessage), typeof(ManageMessage));
            services.AddScoped(typeof(Formatter), typeof(Formatter));

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("C1", new OpenApiInfo
                {
                    Title = "Board Service API",
                    Version = "v1"
                });
                opt.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            LogManager _LogManager = new LogManager(Configuration);

            try
            {
                AppSettingsPath DBConSetUp = new AppSettingsPath(Configuration);

                bool UseEncryptedDBCon = DBConSetUp.UseEncryptedDBCon();
                string conString = DBConSetUp.GetDefaultCon();

                services.AddDbContext<DatabContext>(options =>
                options.UseSqlServer(conString));
            }
            catch (SqlException ex)
            {
                decimal number = ex.Number;
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
            }

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt => {
                opt.SwaggerEndpoint("/swagger/C1/swagger.json", "Board Service API");
                opt.RoutePrefix = string.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            //app.UseSwagger();
            //app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Board Service API"));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
