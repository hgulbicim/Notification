using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Notification.Business.Abstract;
using Notification.Business.Assembler;
using Notification.Business.DataAccess.MongoDB;
using Notification.Business.Mapper.AutoMapper;
using Notification.Business.OperationService;
using Notification.Business.Repository.Abstract;
using Notification.Business.Repository.MongoDB;
using Notification.Business.Service;
using Notification.Contract.Abstract;
using Notification.Entities.Configuration;
using Notification.Service.Common.Service;
using System.IO;

namespace Notification.Service
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Configuration = configuration;
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                return new UrlHelper(factory.GetService<IActionContextAccessor>().ActionContext);
            });

            services.AddSingleton<IOperationService, OperationService>();
            services.AddSingleton<IOperationServiceAssembler, OperationServiceAssembler>();
            services.AddSingleton<INotificationService, NotificationManager>();
            services.AddSingleton<INotificationTemplateService, NotificationTemplateManager>();
            services.AddSingleton<IPromptService, PromptManager>();
            services.AddSingleton<INotificationInfoRepository, MongoDBNotificationInfoRepository>();

            services.AddMvcCore()
                        .AddJsonFormatters(options => options.ContractResolver = new DefaultContractResolver())
                        .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSingleton<MongoDbContext>();

            services.Configure<Settings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoDbConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoDbConnection:Database").Value;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}