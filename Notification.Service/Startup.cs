using AutoMapper;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.Mongo;
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
using Notification.Business.Validation.FluentValidation;
using Notification.Contract.Abstract;
using Notification.Entities.Configuration;
using Notification.Service.ActionFilters;
using Notification.Service.Common.Service;

namespace Notification.Service
{
    public class Startup
    {
        private IConfiguration _configuration { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
        }

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

            services.AddSingleton<INotificationInfoRepository, MongoDbNotificationInfoRepository>();

            services.AddMvcCore(options =>
                         {
                             options.Filters.Add(new ModelStateFilter());
                         })
                        .AddJsonFormatters(options => options.ContractResolver = new DefaultContractResolver())
                        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<NotificationRequestValidator>())
                        .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSingleton<MongoDbContext>();

            services.Configure<Settings>(options =>
            {
                options.ConnectionString = _configuration.GetSection("MongoDbConnection:ConnectionString").Value;
                options.Database = _configuration.GetSection("MongoDbConnection:Database").Value;
            });

            #region Hangfire

            var migrationOptions = new MongoMigrationOptions
            {
                Strategy = MongoMigrationStrategy.Migrate,
                BackupStrategy = MongoBackupStrategy.Collections
            };

            var storageOptions = new MongoStorageOptions
            {
                MigrationOptions = migrationOptions
            };

            services.AddHangfire(options =>
            {
                options.UseMongoStorage(_configuration.GetSection("MongoDbConnection:ConnectionString").Value,
                                        _configuration.GetSection("MongoDbConnection:Database").Value,
                                        storageOptions);
            });

            #endregion
        }

        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IHostingEnvironment env)
        {
            app.UseMvc();

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}