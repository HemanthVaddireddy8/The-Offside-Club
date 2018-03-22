<%@ Page Title="Everything Football" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="EverythingFootballDemo.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<style type="text/css">
        fieldset {border-color:whitesmoke}
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
    <asp:Table ID="tblHomePage" runat="server">
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">
                <asp:Panel ID="pnlLatestNews" runat="server" Width="60%" BorderStyle="None" GroupingText="Latest News" Font-Bold="true" ForeColor="white">
                    <asp:Panel ID="pnlLatestNewsChild" runat="server" Width="100%" BorderStyle="None">
                        <asp:GridView ID="gvLatestNews" runat="server" AllowSorting="false" BackColor="#262F3E" CellPadding="20" GridLines="Horizontal" Width="450px" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("title") %>' BackColor="#262F3E" ForeColor="White" Font-Bold="true"></asp:Label>
                                        <br /><br />
                                        <asp:Image ID="imgArticle" runat="server" Width="550px" Height="250px" ImageUrl='<%# Bind("urlToImage") %>'></asp:Image>
                                        <br />
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("description") %>' ForeColor="White"></asp:Label>
                                        <br />
                                        <asp:HyperLink ID="hlnkReadFullStory" runat="server" Text="Read Full Story Here.." ForeColor="REd" NavigateUrl='<%# Bind("url") %>'></asp:HyperLink>
                                        <br /><br />
                                        <asp:Label ID="lblPublishedAt" runat="server" Text='<%# Bind("publishedAt") %>' ForeColor="White"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblCompetition" runat="server" Text="Competition -" ForeColor="White" Font-Bold="true"></asp:Label> &nbsp;&nbsp;
                <asp:DropDownList ID="ddlCompetitions" runat="server" BackColor="LightGray" AutoPostBack="true" OnSelectedIndexChanged="ddlCompetitions_SelectedIndexChanged"></asp:DropDownList>
                        <br /><br />
                
                    <asp:Panel ID="pnlFixtures" runat="server" Width="400px" Height="350px" BorderStyle="None" GroupingText="Fixtures" ForeColor="White">
                        <asp:Panel ID="pnlChildFixtures" runat="server" Width="100%" Height="350px" ScrollBars="Vertical">
                        <asp:GridView ID="gvFixtures" runat="server" AllowSorting="false" EmptyDataText="No fixtures available currently." GridLines="Horizontal" BackColor="#262F3E" Width="350px" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Latest News">
                                    <ItemTemplate>
                                        <asp:Image ID="imgHomeTeam" runat="server" ImageUrl='<%# Bind("homeTeam.shirtUrl") %>' Width="20px" Height="20px" />
                                        <asp:Label ID="lblHomeTeam" runat="server" Width="100px" Font-Bold="true" Text='<%# Bind("homeTeam.shortName") %>' ForeColor="White"></asp:Label>
                                        &nbsp;
                                                    <asp:Label ID="lblHomeScore" runat="server" Font-Bold="true" Text='<%# Bind("homeGoals") %>' ForeColor="White" Visible="false"></asp:Label>
                                                    &nbsp; - &nbsp;
                                        <asp:Label ID="lblAwayScore" runat="server" Font-Bold="true" Text='<%# Bind("awayGoals") %>' ForeColor="White" Visible="false"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblAwayTeam" width="100px" runat="server" Font-Bold="true" Text='<%# Bind("awayTeam.shortName") %>' ForeColor="White"></asp:Label>
                                        <asp:Image ID="imgAwayTeam" runat="server" Width="20px" Height="20px" ImageUrl='<%# Bind("awayTeam.shirtUrl") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%--END--%>
                            </asp:Panel>
                        </asp:Panel>
                <br /><br />
                    <asp:Panel ID="pnlLiveScores" runat="server" Width="400px" Height="350px" BorderStyle="None" GroupingText="Live Scores" ForeColor="White">
                        <%--GV LIVE SCORES--%>
                        <asp:Panel ID="pnlChildLiveScores" runat="server" Width="100%" Height="350px" ScrollBars="Vertical">
                        <asp:GridView ID="gvLiveScores" runat="server" AllowSorting="false" EmptyDataText="No live matches currently." GridLines="Horizontal" BackColor="#262F3E" Width="350px" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Latest News">
                                    <ItemTemplate>
                                        <asp:Image ID="imgHomeTeam" runat="server" ImageUrl='<%# Bind("homeTeam.shirtUrl") %>' Width="20px" Height="20px" />
                                        <asp:Label ID="lblHomeTeam" runat="server" Width="100px" Font-Bold="true" Text='<%# Bind("homeTeam.shortName") %>' ForeColor="White"></asp:Label>
                                        &nbsp;
                                                    <asp:Label ID="lblHomeScore" runat="server" Font-Bold="true" Text='<%# Bind("homeGoals") %>' ForeColor="White"></asp:Label>
                                                    &nbsp; - &nbsp;
                                        <asp:Label ID="lblAwayScore" runat="server" Font-Bold="true" Text='<%# Bind("awayGoals") %>' ForeColor="White"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblAwayTeam" width="100px" runat="server" Font-Bold="true" Text='<%# Bind("awayTeam.shortName") %>' ForeColor="White"></asp:Label>
                                        <asp:Image ID="imgAwayTeam" runat="server" Width="20px" Height="20px" ImageUrl='<%# Bind("awayTeam.shirtUrl") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                            </asp:Panel>
                        <%--END--%>
                        </asp:Panel>
                <br /><br />
                    <asp:Panel ID="pnlResults" runat="server" Width="400px" Height="350px" BorderStyle="None" GroupingText="Results" ForeColor="White">
                        <%--GV RESULTS--%>
                        <asp:Panel ID="pnlChildResults" runat="server" Width="100%" Height="350px" ScrollBars="Vertical">
                        <asp:GridView ID="gvResults" runat="server" AllowSorting="false" EmptyDataText="Results not available currently." GridLines="Horizontal" BackColor="#262F3E" Width="350px" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Latest News">
                                    <ItemTemplate>
                                        <asp:Image ID="imgHomeTeam" runat="server" ImageUrl='<%# Bind("homeTeam.shirtUrl") %>' Width="20px" Height="20px" />
                                        <asp:Label ID="lblHomeTeam" runat="server" Width="100px" Font-Bold="true" Text='<%# Bind("homeTeam.shortName") %>' ForeColor="White"></asp:Label>
                                        &nbsp;
                                                    <asp:Label ID="lblHomeScore" runat="server" Font-Bold="true" Text='<%# Bind("homeGoals") %>' ForeColor="White"></asp:Label>
                                                    &nbsp; - &nbsp;
                                        <asp:Label ID="lblAwayScore" runat="server" Font-Bold="true" Text='<%# Bind("awayGoals") %>' ForeColor="White"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblAwayTeam" width="100px" runat="server" Font-Bold="true" Text='<%# Bind("awayTeam.shortName") %>' ForeColor="White"></asp:Label>
                                        <asp:Image ID="imgAwayTeam" runat="server" Width="20px" Height="20px" ImageUrl='<%# Bind("awayTeam.shirtUrl") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                            </asp:Panel>
                        <%--END--%>
                        </asp:Panel>
                <%--</asp:Panel>--%>
                <br /><br />
                    <asp:Panel ID="pnlNews" runat="server" Width="400px" Height="1350px" ScrollBars="Vertical" BorderStyle="None" GroupingText="News" ForeColor="White">
                        <%--GV LIVE SCORES--%>
                        <asp:GridView ID="gvNews" runat="server" AllowSorting="false" BackColor="#262F3E" CellPadding="20" GridLines="Horizontal" Width="350px" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("title") %>' BackColor="#262F3E" ForeColor="White" Font-Bold="true"></asp:Label>
                                        <br /><br />
                                        <asp:Image ID="imgArticle" runat="server" Width="310px" Height="250px" ImageUrl='<%# Bind("urlToImage") %>'></asp:Image>
                                        <br />
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("description") %>' ForeColor="White"></asp:Label>
                                        <br />
                                        <asp:HyperLink ID="hlnkReadFullStory" runat="server" Text="Read Full Story Here.." ForeColor="REd" NavigateUrl='<%# Bind("url") %>'></asp:HyperLink>
                                        <br /><br />
                                        <asp:Label ID="lblPublishedAt" runat="server" Text='<%# Bind("publishedAt") %>' ForeColor="White"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%--END--%>
                        </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
