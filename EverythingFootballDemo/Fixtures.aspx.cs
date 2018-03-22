using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http.Headers;
using Json.NET;
using System.Data;

using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using EverythingFootballDemo.DAL;
using Newtonsoft.Json.Linq;

namespace EverythingFootballDemo
{
    public partial class Fixtures : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var competition = string.IsNullOrEmpty(ddlCompetitions.SelectedValue) ? "2" : ddlCompetitions.SelectedValue;
                populateFixtures(competition);
                PopulateDropDowns(competition);
                ShowProfile();
                
            }
        }
        private void ShowProfile()
        {
            var divMyProfile = this.Master.FindControl("divMyProfile");
            var btnLogin = this.Master.FindControl("btnLogin") as Button;
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                btnLogin.Visible = false;
                divMyProfile.Visible = true;
            }
            else
            {
                btnLogin.Visible = true;
                divMyProfile.Visible = false;
            }
        }

        private void populateFixtures(string competition)
        {
            /*Fixtures*/
            var responseFixtures = GetMatches(competition, "fixtures");
            var fixtures = BuildMatchData(responseFixtures);

            ShowMatches(fixtures);
        }

        private void ShowMatches(List<Matches> fixtures)
        {
            gvFixtures.DataSource = fixtures;
            gvFixtures.DataBind();
        }

        private List<Matches> BuildMatchData(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsAsync<List<Matches>>().Result;
            return result;
        }
        protected void ddlCompetitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlCompetitionSelected = sender as DropDownList;
            var competition = ddlCompetitionSelected.SelectedValue;
            RefreshMatchData(competition);
        }
        private void RefreshMatchData(string competitionSelected)
        {
            populateFixtures(competitionSelected);
            showGrids();
        }
        private void showGrids()
        {
            var competitionSelected = ddlCompetitions.SelectedValue;
            gvFixtures.Visible = true;
        }

        private HttpResponseMessage GetMatches(string competition, string type)
        {
            var url = string.Empty;
            HttpClient client;
            url = "https://api.crowdscores.com/v1/matches?from={0}T12:00:00-03:00&to={1}T12:00:00-03:00&competition_id={2}&api_key=7e07e4528e1d4949a7baddc98ec4adde";
            url = string.Format(url, DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(8).ToString("yyyy-MM-dd"), competition);
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(url).Result;
            return response;
        }

        protected void imgBookTickets_Click(object sender, ImageClickEventArgs e)
        {
            if (HttpContext.Current.Session["userInfo"] != null)
            {
                var imgBookTickets = sender as ImageButton;
                var row = imgBookTickets.NamingContainer;
                var homeTeamID = (row.FindControl("lblHomeTeamID") as Label).Text;
                var awayTeamID = (row.FindControl("lblAwayTeamID") as Label).Text;
                var homeTeamName = (row.FindControl("lblHomeTeam") as Label).Text;
                var awayTeamName = (row.FindControl("lblAwayTeam") as Label).Text;
                var homeTeamURL = (row.FindControl("lblHTUrl") as Label).Text;
                var awayTeamURL = (row.FindControl("lblATUrl") as Label).Text;
                var venue = (row.FindControl("lblVenue") as Label).Text;

                var bookingFixture = new EverythingFootballDemo.DAL.BookingFixture();
                bookingFixture.HomeTeamName = homeTeamName;
                bookingFixture.HomeTeamUrl = homeTeamURL;
                bookingFixture.AwayTeamName = awayTeamName;
                bookingFixture.AwayTeamUrl = awayTeamURL;
                bookingFixture.venue = venue;
                bookingFixture.CompetitionName = ddlCompetitions.SelectedItem.Text;
                bookingFixture.HomeTeamID = Convert.ToInt16(homeTeamID);
                bookingFixture.AwayTeamID = Convert.ToInt16(awayTeamID);

                Session["bookingFixture"] = bookingFixture;
                Response.Redirect("BookTickets.aspx");
            }
            else {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "confirn", "LoginReqd()", true);
            }
        }
        private void PopulateDropDowns(string competition)
        {
            var competitions = GetCompetiotns();
            ddlCompetitions.DataTextField = "Name";
            ddlCompetitions.DataValueField = "Value";

            ddlCompetitions.DataSource = competitions;
            ddlCompetitions.DataBind();
        }
        private DataTable GetCompetiotns()
        {
            var dtCompetitions = new DataTable();
            dtCompetitions.Columns.Add("Name");
            dtCompetitions.Columns.Add("Value");

            dtCompetitions.Rows.Add("Premier League", "2");
            dtCompetitions.Rows.Add("La Liga", "46");
            dtCompetitions.Rows.Add("Bundesliga", "48");
            dtCompetitions.Rows.Add("Serie A", "49");
            dtCompetitions.Rows.Add("Ligue 1", "47");
            dtCompetitions.Rows.Add("Chanpions League", "372");

            return dtCompetitions;
        }
    }
}