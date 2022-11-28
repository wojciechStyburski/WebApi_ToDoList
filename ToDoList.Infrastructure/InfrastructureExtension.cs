namespace ToDoList.Infrastructure;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<ExceptionMiddleware>()
            .AddSecurity()
            .AddScoped<ISieveProcessor, ApplicationSieveProcessor>()
            .AddDatabaseConnection(configuration)
            .AddAuth(configuration)
            .AddHttpContextAccessor();

        var infrastructureAssembly = typeof(DatabaseOptions).Assembly;
        services
            .Scan(s => s.FromAssemblies(infrastructureAssembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddCors(options =>
        {
            options.AddPolicy("ApiClients", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.WithOrigins("http localization")
                    .AllowAnyOrigin();
            });
        });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app
            .UseCors("ApiClients")
            .UseMiddleware<ExceptionMiddleware>()
            .UseAuthentication()
            .UseAuthorization();

        return app;
    }

}