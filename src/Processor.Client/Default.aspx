<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Processor.Client.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
    <asp:FileUpload ID="FileUploadControl" runat="server" />
    <asp:Button runat="server" ID="UploadButton" Text="Upload" OnClick="UploadButton_Click" />
    <br />
    <br />
    <asp:Label runat="server" ID="lblMessage" />
  </form>
</body>
</html>
