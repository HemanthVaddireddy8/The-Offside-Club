<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Fixtures.aspx.cs" Inherits="EverythingFootballDemo.Fixtures" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #upBookingInfo {
            position: fixed;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.5);
        }
        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }
    </style>
    <script type="text/javascript">
        function LoginReqd() {
            alert('Please login to continue to book your matchday ticket');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div style="width: 400px; height: 300px; margin-left: 150px; margin-right: auto;">
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:Label ID="lblCompetition" runat="server" Text="Competition - " ForeColor="White" Font-Bold="true"></asp:Label>
                     &nbsp;&nbsp;
                <asp:DropDownList ID="ddlCompetitions" runat="server" BackColor="LightGray" AutoPostBack="true" OnSelectedIndexChanged="ddlCompetitions_SelectedIndexChanged"></asp:DropDownList>
                    <br />
                    <br />
                    <asp:Panel ID="pnlFixtures" runat="server" Width="500px" Height="350px" BorderStyle="None" GroupingText="Fixtures" ForeColor="White">
                        <asp:GridView ID="gvFixtures" runat="server" AllowSorting="false" EmptyDataText="No fixtures available currently." GridLines="Horizontal" Width="450px" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Latest News" ItemStyle-Width="320px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHomeTeamID" runat="server" Text='<% # Bind("homeTeam.dbid") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblHTUrl" runat="server" Text='<% # Bind("homeTeam.shirtUrl") %>' Visible="false"></asp:Label>
                                        <asp:Image ID="imgHomeTeam" runat="server" ImageUrl='<%# Bind("homeTeam.shirtUrl") %>' Width="20px" Height="20px" />
                                        <asp:Label ID="lblHomeTeam" runat="server" Width="100px" Font-Bold="true" Text='<%# Bind("homeTeam.shortName") %>' ForeColor="White"></asp:Label>
                                        &nbsp;
                                                    <asp:Label ID="lblHomeScore" runat="server" Font-Bold="true" Text='<%# Bind("homeGoals") %>' ForeColor="White" Visible="false"></asp:Label>
                                        &nbsp; - &nbsp;
                                        <asp:Label ID="lblAwayScore" runat="server" Font-Bold="true" Text='<%# Bind("awayGoals") %>' ForeColor="White" Visible="false"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblAwayTeam" Width="100px" runat="server" Font-Bold="true" Text='<%# Bind("awayTeam.shortName") %>' ForeColor="White"></asp:Label>
                                        <asp:Image ID="imgAwayTeam" runat="server" Width="20px" Height="20px" ImageUrl='<%# Bind("awayTeam.shirtUrl") %>' />
                                        <asp:Label ID="lblATUrl" runat="server" Text='<% # Bind("awayTeam.shirtUrl") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblAwayTeamID" runat="server" Text='<% # Bind("awayTeam.dbid") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblVenue" runat="server" Text='<% # Bind("venue.name") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBookTickets" runat="server" OnClick="imgBookTickets_Click" ImageUrl="~/Images/imageedit_1_8250802204.png" Width="75px" Height="75px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <div id="divLoginConfirm" runat="server" visible="false" class="modal">
        <asp:Table ID="tblLogin" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <h1>Login</h1>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <h2>Login/Register on The Offside Club to continue with ticket booking..</h2>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnLogin" runat="server" Text="Ok" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
