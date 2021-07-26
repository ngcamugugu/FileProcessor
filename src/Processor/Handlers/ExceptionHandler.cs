using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Processor.Handlers
{
  public class ExceptionHandler: IExceptionHandler
  {
    public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
    {
      if (context.Exception.GetType() == typeof(BusinessRuleViolation))
      {
        var businessRuleException = (BusinessRuleViolation)context.Exception;
        context.Result = new TextPlainErrorResult
        {
          StatusCode = HttpStatusCode.BadRequest,
          Request = context.Request,
          Content = Newtonsoft.Json.JsonConvert.SerializeObject(businessRuleException.Errors)
        };
      }
      else
      {
        context.Result = new TextPlainErrorResult
        {
          StatusCode = HttpStatusCode.InternalServerError,
          Request = context.Request,
          Content = context.Exception.Message
        };
      }
      return Task.FromResult(0);
    }

    private class TextPlainErrorResult : IHttpActionResult
    {
      public HttpRequestMessage Request { get; set; }

      public string Content { get; set; }
      public HttpStatusCode StatusCode { get; set; }
      public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
      {
        HttpResponseMessage response =
                         new HttpResponseMessage(StatusCode);
        response.Content = new StringContent(Content);
        response.RequestMessage = Request;
        return Task.FromResult(response);
      }
    }
  }
}
