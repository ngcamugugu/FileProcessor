using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Processor.Test
{
  [TestClass]
  public class FileProcessorTest
  {
    [TestMethod]
    [ExpectedException(typeof(Processor.BusinessRuleViolation))]
    public void Test_Proccessor_Rejects_When_Order_Amount_Is_Less_than_Zero()
    {
      Bootstrap.Strap();
      var processor = Bootstrap.Container.GetInstance<IFileProcessor>();
      string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\CustomerOrders-less-amount.csv");
      byte[] csvFile = GetBinaryFile(path);
      var results = processor.ProcessOrder(csvFile);
    }

    private string _fileUploadEndPoint = "http://localhost:27968/api/orders";
    private string PostFile(byte[] file, string fileName)
    {
      HttpClient httpClient = new HttpClient();
      MultipartFormDataContent form = new MultipartFormDataContent(); ;
      form.Add(new ByteArrayContent(file, 0, file.Length), "csv_file", fileName);
      HttpResponseMessage response = httpClient.PostAsync(_fileUploadEndPoint, form).Result;
      response.EnsureSuccessStatusCode();
      return response.Content.ReadAsStringAsync().Result;      
    }

    private byte[] GetBinaryFile(string filePath)
    {
      byte[] bytes;
      using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
      {
        bytes = new byte[file.Length];
        file.Read(bytes, 0, (int)file.Length);
      }
      return bytes;
    }
  }
}
