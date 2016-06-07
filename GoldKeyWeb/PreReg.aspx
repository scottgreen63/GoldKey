<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreReg.aspx.cs" Inherits="GoldKeyWeb.PreReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 435px;
        }
        .auto-style2 {
            width: 315px;
        }
        .auto-style3 {
            width: 413px;
        }
        .auto-style4 {
            width: 307px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <fieldset style="width:350px;">

    <legend>GoldKey PreReg Test Page</legend>    

    <table style="width: 790px; height: 190px">

    <tr>

        <td class="auto-style2">First Name *: </td>
        <td class="auto-style3">
        <asp:TextBox ID="txtFirstName" runat="server" Width="221px"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Please enter First Name" ControlToValidate="txtFirstName" Display="Dynamic" ForeColor="#FF3300"  SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>

        <td class="auto-style4">Last Name *: </td>
        <td class="auto-style1">
        <asp:TextBox ID="txtLastName" runat="server" style="margin-left: 0px" Width="251px"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Last Name" ControlToValidate="txtLastName" Display="Dynamic" ForeColor="#FF3300"  SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr>

    <td class="auto-style2">Email Id: * </td><td class="auto-style3">

        <asp:TextBox ID="txtEmailId" runat="server" Width="279px"></asp:TextBox><br />

        <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" 

            ControlToValidate="txtEmailId" Display="Dynamic" 

            ErrorMessage="Please enter Email Id" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator ID="rgeEmailId" runat="server" 

            ControlToValidate="txtEmailId" Display="Dynamic" 

            ErrorMessage="Please enter valid email id format" ForeColor="Red" 

            SetFocusOnError="True" 

            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

        </td>

    </tr>

    <tr>

    <td class="auto-style2">Address : </td><td class="auto-style3">

        <asp:TextBox ID="txtAddress" runat="server" Width="255px"></asp:TextBox></td>

    </tr>

    <tr>

    <td class="auto-style2">Contact No.</td><td class="auto-style3">

        <asp:TextBox ID="txtContactNo" runat="server" Width="257px"></asp:TextBox></td>

    </tr>

        <tr>

    <td class="auto-style2">&nbsp;</td><td class="auto-style3">

        <asp:Button ID="btnSubmit" runat="server" Text="Submit" 

                onclick="btnSubmit_Click" /></td>

    </tr>

    </table>

    </fieldset>

    </div>
    </form>
</body>
</html>
