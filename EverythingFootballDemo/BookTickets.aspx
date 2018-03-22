<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BookTickets.aspx.cs" Inherits="EverythingFootballDemo.BookTickets" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Panels {
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 400px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function CCInfoAlert() {
            alert('Please provide the required Credit Card Information to confirm the booking.');
        }
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
        function validateOTP() {
            alert('Please login to continue to book your matchday ticket');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div style="width: 400px; height: 300px; margin-left: auto; margin-right: auto;">
        <asp:Panel ID="pnlBookTicket" runat="server" Width="100%" Height="100%">
            <asp:Table ID="tblBookTickets" runat="server" ForeColor="White" BorderColor="White" CellSpacing="5" BorderWidth="1px">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell HorizontalAlign="Right">
                        <asp:Label ID="lblHeading" runat="server" Font-Bold="true" ForeColor="White"
                            Text="Book Tickets" Visible="false"></asp:Label>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>
                        <br />                    
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Image ID="imgHomeTeam" runat="server" Width="75px" Height="75px" />
                        <br />
                        <asp:Label ID="lblHomeTeam" runat="server" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblTeamID" runat="server" Visible="false"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label ID="lblCompetitionName" runat="server"></asp:Label>
                        <br />
                        <br />
                        -
                        <br />
                        <br />
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Image ID="imgAwayTeam" runat="server" Width="75px" Height="75px" />
                        <br />
                        <asp:Label ID="lblAwayTeam" runat="server" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow BackColor="Red" CssClass="Panels">
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Label ID="lblVenueHeading" runat="server" ForeColor="White" Font-Bold="true" Font-Size="Large"
                            Text="Venue"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        Stadium:
                        <asp:Label ID="lblStadiumName" runat="server"></asp:Label>
                        <br />
                        Capacity:
                        <asp:Label ID="lblStadiumCapacity" runat="server"></asp:Label>
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow BackColor="Red">
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Label ID="lblTicketsHeading" runat="server" ForeColor="White" Font-Bold="true" Font-Size="Large"
                            Text="Tickets"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <asp:Label ID="lblSelectTicketsMsg" runat="server" Text="Tickets:"></asp:Label>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlSelectTickets" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectTickets_SelectedIndexChanged"></asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblStand" runat="server" Text="Stand:"></asp:Label>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlStand" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStand_SelectedIndexChanged"></asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label ID="lblAmount" runat="server" BackColor="DarkSlateGray" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow BackColor="Red">
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Label ID="lblPaymentTitle" runat="server" ForeColor="White" Font-Bold="true" Font-Size="Large"
                            Text="Payment"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3">
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    Name:
                                    <asp:TextBox ID="txtFirstName" runat="server" Width="125px"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:TextBox ID="txtLastName" runat="server" Width="125px"></asp:TextBox>
                                    <br />
                                    <br />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <div id="divCardInfo" runat="server">
                                        <asp:CheckBox ID="chkSaveCreditCardInfo" runat="server" ForeColor="Red"
                                            Text="Use existing card to purchase the tickets."
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
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow Visible="false">
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Label ID="lblOTP" runat="server" Text="OTP: " Visible="false"></asp:Label>
                        <asp:TextBox ID="txtOTP" runat="server" Visible="false" TextMode="Number"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Button ID="btnMakePayment" runat="server" Text="Make Payment" Font-Bold="true"
                            OnClick="btnMakePayment_Click" Width="125px" Height="30px" CssClass="Panels" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow Visible="false">
                    <asp:TableCell HorizontalAlign="Right">
                        <asp:Button ID="btnConfirmPayment" runat="server" Text="Confirm Payment" Font-Bold="true"
                            Width="125px" Height="30px" CssClass="Panels" OnClick="btnConfirmPayment_Click" Visible="false" />
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Button ID="btnCancelPayment" runat="server" Text="Cancel Payment" Font-Bold="true"
                            Width="125px" Height="30px" CssClass="Panels" OnClick="btnCancelPayment_Click" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
        <div class="loading" align="center">
            Loading. Please wait while we send you the One-time password.<br />
            <br />
            <img src="Images/untitled-3.gif" width="150" height="75" />
        </div>
    </div>
</asp:Content>
