<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication7.WebForm2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function OnClientItemsRequesting(sender, eventArgs) {
            debugger;
            var context = eventArgs.get_context();
            context["FilterString"] = eventArgs.get_text();
        }
    </script>
    
    <style type="text/css">
        /*.RadComboBoxDropDown.RadComboBoxDropDown_Default 
        {
           height:400px !important;
        }*/
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scp" runat="server"></asp:ScriptManager>
<%--        <div>
            <telerik:RadComboBox
                runat="server"
                ID="RadComboBox1"
                Width="300px"
                EnableLoadOnDemand="true"
                MinFilterLength="4"
                EnableAutomaticLoadOnDemand="true"
                EnableVirtualScrolling="true"
                ItemsPerRequest="100"
                ShowMoreResultsBox="true"
                OnItemsRequested="RadComboBox1_ItemsRequested"
                OnClientItemsRequesting="OnClientItemsRequesting">
                <WebServiceSettings Method="GetProducts" Path="WebForm2.aspx" />
            </telerik:RadComboBox>
        </div>--%>
        <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1">
         <div class="demo-container size-narrow">
            <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBox1" runat="server" Width="400" Height="150px"  LoadingMessage="Yes its loading" 
                EmptyMessage="Select a Product"   EnableLoadOnDemand="true"  ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                Label="Page Methods:" MinFilterLength="4"  OnItemDataBound="RadComboBox1_ItemDataBound" AutoPostBack="true" OnItemsRequested="RadComboBox1_ItemsRequested1"
                >
            </telerik:RadComboBox>
            <telerik:RadButton RenderMode="Lightweight" runat="server" Text="Select" ID="Button4" OnClick="Button4_Click"  />
            <br />
            <asp:Label CssClass="status-text" ID="Label4" runat="server" />
 
        </div>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
