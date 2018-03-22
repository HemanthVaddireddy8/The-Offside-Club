<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EverythingFootballDemo.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="scripts/Styles.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .formClass {
            background: url('../1.jpg') no-repeat center center;
            background-size: cover;
        }

        .button {
            display: block;
            margin: 0 auto;
            text-align: center;
        }
    </style>
</head>
<body class="bodyClass">
    <form id="form1" runat="server" class="formClass">
        <asp:Table runat="server" Width="1350px" Height="640px" ForeColor="White" BackImageUrl="~/Images/dark-gray.jpg">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                    <div>
                        <div style="margin-left: auto; margin-right: auto;">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <asp:Login ID="loginUser" TitleText="Sign In" runat="server" CssClass="LoginControl"
                                CreateUserText="Haven't joined The Offside Club yet? Register here" HyperLinkStyle-ForeColor="Red"
                                CreateUserUrl="~/Register.aspx" Width="400px" Height="250px" DisplayRememberMe="false"
                                LoginButtonText="Sign In" LoginButtonStyle-BackColor="#262F3E"
                                LoginButtonStyle-ForeColor="White" BorderStyle="Groove" OnAuthenticate="loginUser_Authenticate">

                                <TitleTextStyle BackColor="#262F3E" Font-Bold="True" ForeColor="#FFFFFF" />
                                <LoginButtonStyle BorderStyle="Groove" CssClass="button" Width="75px" />
                            </asp:Login>
                        </div>
                    </div>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="100%" Height="300px"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
