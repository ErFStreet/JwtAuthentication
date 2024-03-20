namespace Constant.Instructure;

public class Response<T>
{
    public Response()
    {
        StatusCode = 0;

        Messages = new List<string>();
    }

    public int StatusCode { get; set; }

    public List<string> Messages { get; set; }

    public T? Value { get; set; }


    public void AddMessage(string message)
    {
        Messages.Add(message);
    }

    public void ChangeStatusCode(HttpStatusCodeEnum httpStatusCode)
    {
        StatusCode = (int)httpStatusCode;
    }
}
