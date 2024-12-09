using System.Globalization;
using e_parliament.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using EvaluationBackend.Helpers.OneSignal;
using EvaluationBackend.DATA;
using EvaluationBackend.Helpers;
using EvaluationBackend.Repository;
using EvaluationBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using EvaluationBackend.Entities;

namespace EvaluationBackend.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {


            services.AddDbContext<DataContext>(options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ISectionsService,SectionsService>();
            services.AddScoped<ISetsService,SetsService>();
            services.AddScoped<IOfferService,OfferService>();
            services.AddScoped<ISubscriptionService,SubscriptionService>();
            services.AddScoped<IMuscleService,MuscleService>();
            services.AddScoped<IExerciseService,ExerciseService>();
            services.AddScoped<ICourseService,CourseService>();
            services.AddScoped<INotificationService,NotificationService>();
            services.AddScoped<IMessageService,MessageService>();


          

            services.AddSignalR();




            // services.AddApiVersioning(config =>
            //   {
            //       config.DefaultApiVersion = new ApiVersion(1, 0);
            //       config.AssumeDefaultVersionWhenUnspecified = true;
            //       config.ReportApiVersions = false;
            //   });

            // services.AddVersionedApiExplorer(options =>
            //  {
            //      options.GroupNameFormat = "'v'VVV";
            //      options.SubstituteApiVersionInUrl = true;
            //  });


            // services.AddScoped<AuthorizeActionFilter>();
            // services.AddScoped<PermissionSeeder>();
            // seed data from permission seeder service
            var serviceProvider = services.BuildServiceProvider();
            // var permissionSeeder = serviceProvider.GetService<PermissionSeeder>();
            // permissionSeeder.SeedPermissions();
            return services;
        }
    }
}