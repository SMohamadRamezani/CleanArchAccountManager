using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Identity;
using App;
using App.Authentication.Handlers;
using App.Authentication.Request;
using App.Authentication.Response;
using App.Interfaces;
using App.Utils;
using App.Wrapper;
using Domain.Interfaces;
using Domain.Models.User;
using InfraData.Repository;


namespace ApiEndPoint
{
    public class DependencyInjection
    {
        public class DependencyContainer
        {
            public static void RegisterServices(IServiceCollection serviceCollection)
            {

                serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

                serviceCollection.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
                serviceCollection.AddScoped<UserManager<User>>();
                serviceCollection.AddScoped<RoleManager<Role>>();
                serviceCollection.AddScoped<IUserRepository, UserRepository>();
                serviceCollection.AddScoped<IRoleRepository, RoleRepository>();
                serviceCollection.AddScoped<IPermissionRepository, PermissionRepository>();
                serviceCollection.AddScoped<IRequestHandler<LoginRequest, ApiResponse<CommonRoleResponse>>, LoginCommandHandler>();
                serviceCollection.AddScoped<IJwtTokenReader, JwtTokenReader>();                

                //serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
                serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                serviceCollection.AddSingleton<IMessageService, MessageService>();

            }
        }
    }
}
