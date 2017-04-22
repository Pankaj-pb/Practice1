<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication7.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type = "text/javascript">
          function Confirm(message) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%=btnConfirm.ClientID%>").click();
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="PlaceHolder_AssociateExistingCandidate" runat="server">
    <div clientidmode="Static" width="99.5%">
        <div class="dialogwarning">
        </div>
        <div class="dialogwarningcontainer" width="100%">
            <div class="dialogtitle">
                <asp:Literal Text="Warning" ID="Label4"
                    runat="server" />
            </div>
            <div class="dialogtext">
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="Label_ExistingCandidateWarning" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button_UseExistingCandidate" runat="server" Text="assoaiate buttsdf sdkf sjldfjdsf "
                                   OnClick="Button_UseExistingCandidate_Click"/>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="text-align: right">
                </div>
            </div>
        </div>
    </div>
</asp:PlaceHolder>
    <div>
     <asp:Button ID="btnConfirm" runat="server"
                  OnClick="btnConfirm_Click"
                  Text="Raise Confirm"/>
    </div>
    </form>
</body>
</html>
