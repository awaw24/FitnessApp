using CoachBuddy.Application.Client.Commands.DeleteClient;
using CoachBuddy.Application.Group.Commands.DeleteGroup;
using CoachBuddy.Application.Mappings;
using CoachBuddy.Domain.Interfaces.Client;
using CoachBuddy.Domain.Interfaces.Exercise;
using CoachBuddy.Domain.Interfaces.Group;
using CoachBuddy.Domain.Interfaces.TrainingPlan;
using CoachBuddy.Infrastructure.Persistence;
using CoachBuddy.Infrastructure.Repositories;
using CoachBuddy.Infrastructure.Seeders;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoachBuddy.Infrastructure.Extensions
{
    public static class ServiceCollecionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<CoachBuddyDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("CoachBuddy")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CoachBuddyDbContext>();

            services.AddScoped<CoachBuddySeeder>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientTrainingRepository, ClientTrainingRepository>();

            services.AddScoped<IGroupRepository, GroupRepository>();

            services.AddTransient<IRequestHandler<DeleteClientCommand>, DeleteClientCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteGroupCommand>, DeleteGroupCommandHandler>();

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IExerciseRepository, ExerciseRepository>();

            services.AddScoped<ITrainingPlanRepository, TrainingPlanRepository>();


        }
    }
}
