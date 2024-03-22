namespace Server.Instructure.TokenHelper;

public class ValidateTokenHelper
{
    public async Task ExecuteAsync(TokenValidatedContext context)
    {
        var userId =
            context.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        if (string.IsNullOrEmpty(value: userId))
            context.Fail(nameof(HttpStatusCode.Unauthorized));

        await Task.CompletedTask;
    }
}
