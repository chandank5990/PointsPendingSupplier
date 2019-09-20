<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="Points_Pending_Supplier.Page1" EnableEventValidation="false"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            text-align: left;
            height: 600px;
        }
        .style2
        {
            text-decoration: underline;
        }
        .style3
        {
            color: #CC0099;
            font-size: x-large;
            background-color: #FFFFFF;
        }
        .style4
        {
            color: #ffffff;
        }
        
        .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: white;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=90);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        color:Red;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        
        z-index: 999;
    }
        
        
        </style>

        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
     
function DisplayFullImage(ctrlimg) 
    { 
        txtCode = "<HTML><HEAD>" 
        +  "</HEAD><BODY TOPMARGIN=0 LEFTMARGIN=0 MARGINHEIGHT=0 MARGINWIDTH=0><CENTER>"   
        + "<IMG src='" + ctrlimg.src + "' BORDER=0 NAME=FullImage " 
        + "onload='window.resizeTo(document.FullImage.width,document.FullImage.height)'>"  
        + "</CENTER>"   
        + "</BODY></HTML>"; 
        mywindow= window.open  ('','image',  'toolbar=0,location=0,menuBar=0,scrollbars=0,resizable=0,width=1,height=1'); 
        mywindow.document.open(); 
        mywindow.document.write(txtCode); 
        mywindow.document.close();
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="style1">
    
        <span style="text-align: left"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToValidate="txtto" ErrorMessage="Value must be Of Date Type"
    Type="Date" Operator="DataTypeCheck" ForeColor="Red"/>
            
            
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
            ID="Label1" runat="server" 
            style="z-index: 1; left: 282px; top: 52px; position: absolute"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="style2"><em><span class="style3">Points Pending Supplier</span>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            style="z-index: 1; left: 1077px; top: 33px; position: absolute; height: 34px; width: 129px; font-weight: 700; color: #FFFF00; background-color: #CC3399" 
            Text="Download Details" />
        </em></span></strong><br />
        <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CompareValidator ID="CompareValidatortxtfrom" runat="server" 
            ControlToValidate="txtfrom" ErrorMessage="Value must be Of Date Type"
    Type="Date" Operator="DataTypeCheck" ForeColor="Red"/>

            
        </strong>
        <asp:Button ID="Button2" runat="server" BackColor="#CC3399" BorderColor="White" 
            ForeColor="Yellow" onclick="Button2_Click" 
            style="z-index: 1; left: 920px; top: 32px; position: absolute; width: 139px; height: 35px; right: 205px; font-weight: 700" 
            Text="Download Drawings" />
        <asp:Label ID="Label2" runat="server" 
            style="z-index: 1; left: 630px; top: 53px; position: absolute; height: 19px; width: 93px; font-weight: 700; font-style: italic; right: 516px" 
            Text="Total Points:"></asp:Label>
        <asp:Label ID="Label3" runat="server" 
            
            style="z-index: 1; left: 724px; top: 53px; position: absolute; height: 18px; width: 64px; color: #FF0000; font-weight: 700; font-style: italic; text-decoration: underline; background-color: #FFFF00; text-align: center;"></asp:Label>
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" BackColor="White" 
            
            style="z-index: 1; left: 317px; top: 919px; position: absolute; position:static; margin-left: 157px;" 
            ScrollBars="Auto" Height="270px" Width="1179px" >
       
        <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="true" 
            BorderWidth="3px" CellPadding="2" BorderStyle="Groove"
            
            style="z-index: 1; left: 184px; top: 107px; position: static; height: 154px; width: 755px; text-align: center;" 
            CellSpacing="1" AutoGenerateColumns="False" GridLines="None" 
            ShowFooter="True" onrowdatabound="GridView1_RowDataBound">


             <Columns>
                         <asp:TemplateField HeaderText="Sr No.">
                             <ItemTemplate>
                                  <%#Container.DataItemIndex+1 %>
                             </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NumOrd" HeaderText="UID" ReadOnly="True" />
                        <asp:BoundField DataField="ArtOrd" HeaderText="Article" ReadOnly="True" />
                        
                        <asp:TemplateField HeaderText="View">
        <ItemTemplate>
       <asp:LinkButton runat="server" ID="lnkView" OnClick="lnkView_Click">Drawing</asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField> 
                        <asp:BoundField DataField="EntOrd" HeaderText="Delivery.Date" DataFormatString = "{0:d}" ReadOnly="True" />
                        
                        <asp:BoundField DataField="PiePed" HeaderText="Order Qty" ReadOnly="True" />
                        <asp:BoundField DataField="PieRec" HeaderText="Delivered Qty" ReadOnly="True" />
                         <asp:TemplateField HeaderText="Pending Qty">
				    <ItemTemplate>
                      <asp:Label ID="Label2" runat="server"/>
                    </ItemTemplate>
                 </asp:TemplateField>
                        <asp:BoundField DataField="FecPed" HeaderText="Date_Sent.." DataFormatString = "{0:d}" ReadOnly="True" />
                        <asp:BoundField DataField="PlaPie" HeaderText="Receiving.Date" DataFormatString = "{0:d}" ReadOnly="True" />
                        <asp:BoundField DataField="PreOrd" HeaderText="Rate" ReadOnly="True" />
                        <asp:BoundField DataField="DtoOrd" HeaderText="Discount" ReadOnly="True" />
                        <asp:BoundField DataField="PrePie" HeaderText="Price" FooterText="Total" ReadOnly="True" />
                        <asp:TemplateField HeaderText="AWS Total Point">
        
				<ItemTemplate>
                      <asp:Label ID="Label1" runat="server"/>
                  </ItemTemplate>
			 <FooterTemplate>
				<asp:Label ID="lblTotalpoint" runat="server" />
			 </FooterTemplate>
       </asp:TemplateField>
                        <asp:BoundField DataField="Observaciones" HeaderText="Observation" ReadOnly="True" />
                        <asp:BoundField DataField="Datos" HeaderText="TCP Remarks" ReadOnly="True" />
                        
                        <asp:BoundField DataField="NumPed" HeaderText="Order" ReadOnly="True" />
                        <asp:BoundField DataField="CodPie" HeaderText="CodPie" DataFormatString = "{0:d}" ReadOnly="True" />
                        <asp:BoundField DataField="NumFas" HeaderText="Step No." ReadOnly="True" />                               
                    </Columns>
                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>

            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center"/>
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"/>
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"/>
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="500px"/>
<AlternatingRowStyle BackColor="White" />


        </asp:GridView>
         </asp:Panel>
        </span>
        &nbsp;<span style="text-align: left"><strong></strong></span>
        <span style="text-align: left">&nbsp;</span><br />
        <br />
        
        <div style="position:absolute;width: 140px;
    height: 300px;
    padding: 10px;
    margin-top: 60px;margin-left: 40px; top: 4px; left: -42px;">
        <strong>
        From <br /><br />
        <asp:TextBox ID="txtfrom" runat="server" placeholder=" Enter From Date" 
            style="z-index: 1; left: 8px; top: 28px; position: absolute; height: 26px; font-weight: 700; text-align: left; width: 139px;" 
            ontextchanged="txtfrom_TextChanged" ToolTip="Enter From Date">
            </asp:TextBox>
            <ajax:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtfrom" Format="dd/MM/yyyy">
            </ajax:CalendarExtender>
            <br/>
            To <br/><br />
            <asp:TextBox ID="txtto" 
                runat="server" Text="" placeholder=" Enter To Date"
            
            style="z-index: 1; left: 8px; top: 85px; position: absolute; height: 24px; font-weight: 700; text-align: left; width: 137px; right: 35px;" 
            ToolTip="Enter To Date"></asp:TextBox>
            <ajax:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" 
                TargetControlID="txtto" Format="dd/MM/yyyy">
            </ajax:CalendarExtender>
            <ajax:ToolkitScriptManager ID="toolkit1" runat="server"></ajax:ToolkitScriptManager>
            <br/>
            Supplier<br /><br />
            <asp:DropDownList 
            ID="ddsupplier" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddsupplier_SelectedIndexChanged" 
            
            style="z-index: 1; left: 7px; top: 141px; position: absolute; width: 146px; height: 33px; font-weight: 700; text-align: right;" 
            DataSourceID="SqlDataSource1" DataTextField="NomPro" 
            DataValueField="CodPro" ToolTip="Select One">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
           <asp:Button ID="btnsubmit" runat="server" onclick="btnsubmit_Click"
            
            style="z-index: 1; left: 74px; top: 182px; position: absolute; font-weight: 700; height: 31px; width: 78px; color: #CCFF66; background-color: #CC3399;" 
            Text="Submit" TabIndex="1" /> 
        </strong>
        </div>
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:TablasConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:TablasConnectionString.ProviderName %>" 
            SelectCommand="SELECT * FROM [Proveedores] ORDER BY [NomPro]"></asp:SqlDataSource>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <div  style="width: 230px;
    height: 300px;
    padding: 10px;
    
    margin-top:-60px;">
        <asp:Image ID="Image1" runat="server" Height="285px" 
            ImageUrl="~/website under construction template gif-images.PNG" Width="216px"  
                style="position:relative; top: 0px; left: 0px;" onclick="DisplayFullImage(this);"/>
           </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <div style="width: 600px;
    height: 600px;
    padding: 10px;
    
    margin-left: 400px;margin-top:-350px;">
        <iframe id="Iframe1" runat="server" name="I1"
            style="height: 581px; width: 591px; margin-right: 0px; position:static" visible="True"></iframe>
            </div>
        <br />
        <br />
        <div style="width: 210px;
    height: 40px;
    padding: 10px;
    line-height: 65%;
    position: fixed;
    bottom: 100px;
    right: 0;
    background-color: #85e085;
    border: 3px solid #990000;">
    <em><span class="style4"><strong>Downloaded Drawings Path<br /><br />
    W:\test\PPS\MergedDrawings2</strong><br />
        </span></em>
    </div>
        <div style="width: 210px;
    height: 70px;
    padding: 10px;
    line-height: 65%;
    position: fixed;
    bottom: 0;
    right: 0;
    background-color: #85e085;
    border: 3px solid #990000;"><em><span class="style4"><strong>While using this Application if</strong><br />
        </span><strong>
        <br class="style4" />
        </strong><span class="style4"><strong>you encounter with any kind of</strong><br />
        
        </span><strong>
        
        <br class="style4" />
        </strong><span class="style4"><strong>Error Kindly Inform us at</strong><br />
        </span>
        <strong>
        <br class="style4" />
        </strong><span class="style4"><strong>it@anugroup.net</strong></span></em><br /></div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       
    </form>
</body>
</html>
