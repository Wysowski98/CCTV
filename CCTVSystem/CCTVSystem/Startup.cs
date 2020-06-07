using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CctvDB;
using Services.Service;
using Infrastructure;
using Hangfire;
using Hangfire.SqlServer;
using System.Diagnostics;
using Hangfire.Common;
using Microsoft.AspNetCore.Identity;
using Domain;

namespace CCTVSystem
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

            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CCTVSystem", Version = "Version 1.0" });
            });

            services.AddHangfire(config =>
            config.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));
            services.AddHangfireServer();

            services.AddDbContext<CctvDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CCTVSystem")));
            services.AddIdentity<Client, IdentityRole>(config =>
            {
                // password configuration
                config.Password.RequiredLength = 8;
                config.Password.RequireDigit = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<CctvDbContext>();

            MapperSetup.Configurate();
            SetIOC(services);
        }

        private void SetIOC(IServiceCollection service)
        {
            service.AddScoped<IClientService, ClientService>();
            service.AddScoped<ITransmissionService, TransmissionService>();
            service.AddScoped<ICameraService, CameraService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseSwagger();

            app.UseHangfireDashboard();

            recurringJobs.AddOrUpdate("Clean", Job.FromExpression<HangfireSetup>(x => x.ScheduleReccurringCleaning()), Cron.Minutely());

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CCTVSystem Version 1.0");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
