
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Drawing.aspx.cs" Inherits="Points_Pending_Supplier.Drawing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="jquery.js" type="text/javascript"></script>
<script src="jquery.imageLens.js" type="text/javascript"></script>
</head>
<body>
    <form id="form2" runat="server">
    <div>
    
        <table align="center" >
            <tr>
                <td align="center" >
                    <asp:Button ID="Button1" runat="server" Font-Bold="True" Height="20px" 
                        onclick="Button1_Click" Text="Drawing" />
                    <asp:Button ID="Button2" runat="server" Font-Bold="True" Height="20px" 
                        onclick="Button2_Click" Text="PT" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Image ID="Image1" runat="server" Height="454px" Width="440px" 
                        ImageAlign="Middle" 
                        ImageUrl="~/website under construction template gif-images.PNG" ToolTip="Customer Drawing"
             BorderWidth="3"
             BorderStyle="Solid"
             BorderColor="Crimson"/>
                        <pre>$("#img_02").imageLens({ lensSize: 200 });</pre>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
