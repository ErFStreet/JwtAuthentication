namespace Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // *** Register Services *** \\

        builder.Services.Register();

        // *** Register Jwt *** \\

        builder.Services.RegisterJwt(configuration: builder.Configuration);

        // *** Register Application *** \\

        var app = builder.Build();

        app.Register();

        // *** Run Application *** \\

        app.Run();
    }
}