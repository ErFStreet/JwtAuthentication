namespace Server.Instructure.Contracts;

public interface ITokenGenerator
{
    string? GenerateToken();
}
