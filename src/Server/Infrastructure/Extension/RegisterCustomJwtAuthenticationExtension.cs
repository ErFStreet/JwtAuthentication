namespace Server.Infrastructure.Extension;

public static class RegisterCustomJwtAuthenticationExtension
{
    public static void RegisterJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var secretKey =
           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
              {
                  ValidateIssuer = false,

                  ValidateAudience = false,

                  ValidateLifetime = true,

                  ValidateIssuerSigningKey = true,

                  IssuerSigningKey = secretKey,
              };

              options.Events = new JwtBearerEvents
              {
                  OnMessageReceived = async context =>
                  {
                      string? access_token = context.HttpContext.Request.Headers["Authorization"];

                      if (!string.IsNullOrEmpty(access_token))
                      {
                          context.Token = access_token;

                          return;
                      }

                      access_token = context.HttpContext.Request.Headers["access_token"];

                      if (!string.IsNullOrEmpty(access_token))
                      {
                          context.Token = access_token;

                          return;
                      }

                      context.Token = access_token;

                      await Task.CompletedTask;

                  },
                  OnTokenValidated = async context =>
                  {
                      var tokenValidator =
                           new ValidateTokenHelper();

                      await tokenValidator.ExecuteAsync(context: context);
                  },
                  OnChallenge = async context =>
                  {
                      context.HandleResponse();

                      await CreateUnAuthorizeResult(response: context.Response);
                  }
              };

          });
    }

    public static async Task CreateUnAuthorizeResult(HttpResponse response)
    {
        response.StatusCode = (int)HttpStatusCode.Unauthorized;

        var result = new Response<string>();

        result.AddMessage(message: nameof(HttpStatusCode.Unauthorized));

        result.ChangeStatusCode(httpStatusCode: HttpStatusCodeEnum.UnAuthrozied);

        await response.WriteAsJsonAsync(result);
    }
}
