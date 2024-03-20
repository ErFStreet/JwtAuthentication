using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers.V1;

public class AuthenticationController : BaseController
{
    private readonly ITokenGenerator tokenGenerator;

    public AuthenticationController(ITokenGenerator tokenGenerator)
    {
        this.tokenGenerator = tokenGenerator;
    }

    // *** Test *** \\

    [HttpGet("Login")]
    public ActionResult<Response<string>> Login()
    {
        var response = new Response<string>();

        var token =
            tokenGenerator.GenerateToken();

        if (token is not null)
        {
            response.AddMessage(message: ResponseMessages.Success);

            response.ChangeStatusCode(httpStatusCode: HttpStatusCodeEnum.Success);

            response.Value = token;

            return response;
        }

        response.AddMessage(message: ResponseMessages.ServerError);

        response.ChangeStatusCode(httpStatusCode: HttpStatusCodeEnum.ServerError);

        return response;
    }

    [HttpGet("AuthorizeTest")]
    [Authorize(Roles = "Admin")]
    public ActionResult<Response<string>> AuthorizeTest()
    {
        var response = new Response<string>();

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(httpStatusCode: HttpStatusCodeEnum.Success);

        return response;
    }
}
