<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EverythingFootballDemo.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .customAlign {
            vertical-align: central;
            text-align: center;
            align-content: center;
        }

        .customAlignCC {
            vertical-align: central;
            align-content: center;
        }

        .customPanelAlignCC {
            margin-left: auto;
            margin-right: auto;
        }

        .bodyBackground {
            background-image: url('../Images/1.jpg');
            background-size: cover;
        }

        .ddlMonth {
            width: 80px;
            margin: 0 0 0 0;
            font: 12px tahoma;
            max-height: 100px;
            overflow-y: scroll;
            position: relative;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Table runat="server" Width="1350px" Height="640px" BackImageUrl="~/Images/dark-gray.jpg">
            <asp:TableRow>
                <asp:TableCell>
                    <div id="divRegister" runat="server" style="width: 400px; /*height: 300px; */ margin-left: auto; margin-right: auto;">
                        <asp:Panel ID="pnlRegister" runat="server">
                            <asp:Table ID="tblRegister" runat="server" Style="width: 400px; /*height: 400px*/" ForeColor="White" BorderColor="White" BorderStyle="Groove" CssClass="customAlign">
                                <asp:TableHeaderRow BackColor="#262F3E">
                                    <asp:TableHeaderCell ColumnSpan="3">
                                        <asp:Label ID="lblHeading" runat="server" Text="Register" Font-Bold="true" ForeColor="White"></asp:Label>
                                    </asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                            <br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblUserName" runat="server" Text="User Name" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblPassowrd" runat="server" Text="Password" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblEmailAddress" runat="server" Text="Email" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtEmailAddress" runat="server" TextMode="Email"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revEmailAddress" runat="server" SetFocusOnError="true"
                                            Display="None" ControlToValidate="txtEmailAddress" resourceKey="revEmailAddress"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="register"></asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                            <br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkSaveCreditCardInfo" runat="server" ForeColor="Red"
                                            Text="Save Credit Card for faster purchases.."
                                            AutoPostBack="true" OnCheckedChanged="chkSaveCreditCardInfo_CheckedChanged" />
                                        <br />
                                        <asp:Table ID="tblCreditCard" runat="server" Style="width: 400px; /*height: 130px*/" CssClass="customAlign">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblCreditCardNum" runat="server" Font-Bold="true" Text="Credit Card Number"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="txtCreditCardNum" runat="server"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblExpDate" runat="server" Font-Bold="true" Text="Expriration Date"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="ddlMonth" runat="server" Font-Size="Small"
                                                        CssClass="ddlMonth">
                                                    </asp:DropDownList>
                                                    &nbsp; / &nbsp;
                                            <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblSecCode" runat="server" Font-Bold="true" Text="Security Code"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="txtSecCode" runat="server" TextMode="Number" MaxLength="3"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblNameOnCard" runat="server" Font-Bold="true" Text="Name On Card"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="txtNameOnCard" runat="server"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow>
                                    <asp:TableCell ColumnSpan="3">
                                        <br />
                                        <asp:Button ID="btnRegister" runat="server" Text="Join The Offside Club" OnClick="btnRegister_Click" />
                                    </asp:TableCell>
                                </asp:TableFooterRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                            <br />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                    </div>
                    <div id="divStatus" runat="server" style="width: 400px; margin-left: auto; margin-right: auto;" visible="false">
                        <asp:Table runat="server" Style="width: 400px;" BorderColor="#262F3E" BorderStyle="Groove" CssClass="customAlign">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell>
                                    <asp:Label ID="lblStatusMsg" runat="server" Text="Status Message" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:HyperLink ID="hlnkHomePage" runat="server" Text="Go to Home page"></asp:HyperLink>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js">
    function ShowCreditCardInfo() {
        debugger;
        var chkSaveCreditCard = document.getElementById("chkSaveCreditCardInfo");
        var tblCreditCardInfo = document.getElementById("tblCreditCard");
        if (chkSaveCreditCard.checked == true) {
            tblCreditCardInfo.style.display = "block";
        } else {
            tblCreditCardInfo.style.display = "block";
        }
    }
</script>
