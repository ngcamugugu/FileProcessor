using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Processor.Client
{
  public partial class Default : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
      if (FileUploadControl.HasFile)
      {
        string filename = Path.GetFileName(FileUploadControl.FileName);
        FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
        string path = Server.MapPath("~/") + filename;
        byte[] csvFile = GetBinaryFile(path);
        PostFile(csvFile, filename);
      }
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


    private string _fileUploadEndPoint = "http://localhost:27968/api/orders";
    private void PostFile(byte[] file, string fileName)
    {
      HttpClient httpClient = new HttpClient();
      MultipartFormDataContent form = new MultipartFormDataContent();;
      form.Add(new ByteArrayContent(file, 0, file.Length), "csv_file", fileName);      
      try
      {
        HttpResponseMessage response = httpClient.PostAsync(_fileUploadEndPoint, form).Result;
        response.EnsureSuccessStatusCode();
        lblMessage.Text = response.Content.ReadAsStringAsync().Result;
      }
      catch (Exception ex)
      {
        lblMessage.Text = $"An error occurred: {ex.Message}";
      }
      finally
      {
        httpClient.Dispose();
      }  
    }
  }
}