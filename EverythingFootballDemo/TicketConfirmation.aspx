<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketConfirmation.aspx.cs" Inherits="EverythingFootballDemo.TicketConfirmation" %>

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
    <script type="text/javascript">
        function validateOTP() {
            alert('The OTP you have entered is incorrect. You have one attempt left.');
        }
        function validateOTPFinal() {
            alert('The OTP you have entered is incorrect. Your transaction has been cancelled.');
        }
    </script>
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
                            <asp:Table ID="tblOTP" runat="server" BorderStyle="Solid" BorderColor="White" Width="300px" Height="150px" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        OTP:
                                        <asp:TextBox ID="txtOTP" runat="server" Width="150px"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <br />
                                        <asp:Button ID="btnConfirmPayment" runat="server" Text="Confirm Payment" Font-Bold="true"
                                            Width="125px" Height="30px" CssClass="Panels" OnClick="btnConfirmPayment_Click" Visible="false" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <br />
                                        <asp:Button ID="btnCancelPayment" runat="server" Text="Cancel Payment" Font-Bold="true"
                                            Width="125px" Height="30px" CssClass="Panels" OnClick="btnCancelPayment_Click" Visible="false" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <div id="divStatus" runat="server" style="width: 400px; margin-left: auto; margin-right: auto;" visible="false">
                                <asp:Table runat="server" Style="width: 400px;" BorderColor="White" BorderStyle="Groove" CssClass="customAlign">
                                    <asp:TableHeaderRow>
                                        <asp:TableHeaderCell>
                                            <asp:Label ID="lblStatusMsg" runat="server" Text="Status Message" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        </asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center">
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center">
                                            <asp:HyperLink ID="hlnkHomePage" ForeColor="White" runat="server" Text="Go to Home page" NavigateUrl="~/HomePage.aspx"></asp:HyperLink>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
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
