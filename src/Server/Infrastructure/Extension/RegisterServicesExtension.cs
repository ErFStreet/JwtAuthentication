namespace Server.Instructure.Extension;

public static class RegisterServicesExtension
{
    public static void Register(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddScoped<ITokenGenerator, TokenGenerator>();
    }
}
