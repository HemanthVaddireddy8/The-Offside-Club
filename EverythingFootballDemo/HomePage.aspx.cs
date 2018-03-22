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
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateHomePage();
            }
        }

        private void populateHomePage()
        {
            var competition = string.IsNullOrEmpty(ddlCompetitions.SelectedValue) ? "2" : ddlCompetitions.SelectedValue;
            populateMatches(competition);
            ShowArticlesAndNews();
            PopulateDropDowns();
            ShowProfile();
        }
        private void populateMatches(string competition)
        {
            /*Fixtures*/
            var responseFixtures = GetMatches(competition, "fixtures");
            var fixtures = BuildMatchData(responseFixtures);
            /*Live*/
            var responseLiveScores = GetMatches(competition, "live");
            var liveScores = BuildMatchData(responseLiveScores);
            /*Results*/
            var responseResults = GetMatches(competition, "results");
            var results = BuildMatchData(responseResults);

            ShowMatches(fixtures, liveScores, results);
        }

        private HttpResponseMessage GetMatches(string competitionID, string type)
        {
            var url = string.Empty;
            HttpClient client;
            if (type.ToUpper().Equals("FIXTURES"))
            {
                url = "https://api.crowdscores.com/v1/matches?from={0}T12:00:00-03:00&to={1}T12:00:00-03:00&competition_id={2}&api_key=7e07e4528e1d4949a7baddc98ec4adde";
                url = string.Format(url, DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(8).ToString("yyyy-MM-dd"), competitionID);
            }
            else if (type.ToUpper().Equals("LIVE"))
            {
                url = "https://api.crowdscores.com/v1/matches?from={0}T12:00:00-03:00&to={1}T12:00:00-04:00&competition_id={2}&api_key=7e07e4528e1d4949a7baddc98ec4adde";
                url = string.Format(url, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), competitionID);
            }
            else
            {
                url = "https://api.crowdscores.com/v1/matches?from={0}T12:00:00-03:00&to={1}T12:00:00-03:00&competition_id={2}&api_key=7e07e4528e1d4949a7baddc98ec4adde";
                url = string.Format(url, DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"), competitionID);
            }
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(url).Result;
            return response;
        }
        private List<Matches> BuildMatchData(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsAsync<List<Matches>>().Result;
            return result;
        }
        private void ShowMatches(List<Matches> fixtures, List<Matches> liveScores, List<Matches> results)
        {
            gvFixtures.DataSource = fixtures;
            gvFixtures.DataBind();

            gvLiveScores.DataSource = liveScores;
            gvLiveScores.DataBind();

            gvResults.DataSource = results;
            gvResults.DataBind();

            showGrids();
        }
        private void showGrids()
        {
            var competitionSelected = ddlCompetitions.SelectedValue;
            gvFixtures.Visible = true;
            gvLiveScores.Visible = true;
            gvResults.Visible = true;
        }
        private void RefreshMatchData()
        {
            var competitionSelected = ddlCompetitions.SelectedValue;
            populateMatches(competitionSelected);
            showGrids();
        }


        #region Articles
        private void ShowArticlesAndNews()
        {
            var responseArticles = GetArticles();
            PopulateArticles(responseArticles);

            var responseNews = GetNews();
            PopulateNews(responseNews);
        }

        private HttpResponseMessage GetArticles()
        {
            HttpClient client;
            client = new HttpClient();
            client.BaseAddress = new Uri(ArcticlesURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(ArcticlesURL).Result;
            return response;
        }

        private void PopulateArticles(HttpResponseMessage response)
        {
            var listArticles = new List<EverythingFootballDemo.DAL.Article>();
            if (response.IsSuccessStatusCode)
            {

                var result = response.Content.ReadAsAsync<RawResult>().Result;
                listArticles = result.articles;
            }
            gvLatestNews.DataSource = listArticles;
            gvLatestNews.DataBind();
        }
        private HttpResponseMessage GetNews()
        {
            HttpClient client;
            client = new HttpClient();
            client.BaseAddress = new Uri(NewsURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(NewsURL).Result;
            return response;
        }
        private void PopulateNews(HttpResponseMessage response)
        {
            var listArticles = new List<EverythingFootballDemo.DAL.Article>();
            if (response.IsSuccessStatusCode)
            {

                var result = response.Content.ReadAsAsync<RawResult>().Result;
                listArticles = result.articles;
            }
            gvNews.DataSource = listArticles;
            gvNews.DataBind();
            pnlNews.Height = (listArticles.Count * 300);
        }
        private void ShowProfile() {
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

        #endregion
        protected void ddlCompetitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMatchData();
        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMatchData();
        }
        private void PopulateDropDowns()
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

        private DataTable GetTypes()
        {
            var dtTypes = new DataTable();
            dtTypes.Columns.Add("Name");
            dtTypes.Columns.Add("Value");

            dtTypes.Rows.Add("Fixtures", "fixtures");
            dtTypes.Rows.Add("Live Scores", "livescores");
            dtTypes.Rows.Add("Results", "results");

            return dtTypes;
        }

        private string ArcticlesURL = "https://newsapi.org/v2/top-headlines?sources=talksport&apiKey=2aaefa087142404db935023ddbc72955";
        private string NewsURL = "https://newsapi.org/v2/top-headlines?sources=four-four-two&apiKey=2aaefa087142404db935023ddbc72955";
    }
}