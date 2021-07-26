using Processor.Models;
using Processor.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace Processor.Controllers
{
  public class OrdersController : ApiController
  {
    private readonly IFileProcessor _fileProcessor; 
    public OrdersController(IFileProcessor fileProcessor)
    {
      _fileProcessor = fileProcessor;
    }

    [HttpPost]
    public async Task<IHttpActionResult> Order()
    {
      if (!Request.Content.IsMimeMultipartContent()) throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

      var uploadPath = HostingEnvironment.MapPath("/") + @"/App_Data";
      Directory.CreateDirectory(uploadPath);
      var provider = await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());
      if (!provider.Files.Any()) return BadRequest("No document to upload.");
      IList<HttpContent> files = provider.Files;
      if (!files.Any()) return BadRequest("File not uploaded.");
      HttpContent document = files[0];
      byte[] csvFile = await document.ReadAsByteArrayAsync();
      List<OrderResponse> response = await _fileProcessor.ProcessOrder(csvFile);
      return Ok(response);
    }
  }
}
