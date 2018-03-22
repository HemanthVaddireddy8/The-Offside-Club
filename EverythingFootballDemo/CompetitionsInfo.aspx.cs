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
    public partial class CompetitionsInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var competition = Request["competitionName"] != null ? Request["competitionName"].ToString() : string.Empty;
            PopulateNews(competition);
            populateMatches(competition);
            PopulateDropDowns(competition);
            ShowProfile();
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
        private void PopulateNews(string competition)
        {
            switch (competition) {
                case "PL":
                case "BL":
                case "SL":
                case "FL":
                    NewsURL = string.Format(NewsURL, "talksport");
                    break;
                case "IL":
                    NewsURL = string.Format(NewsURL, "football-italia");
                    break;
                case "CL":
                    NewsURL = string.Format(NewsURL, "four-four-two");
                    break;
                default:
                    NewsURL  = string.Format(NewsURL, "talksport");
                    break;
            }
            ShowArticles(NewsURL);

        }
        private void ShowArticles(string NewsURL)
        {
            var response = GetArticles(NewsURL);
            var articles = BuildArticles(response);
            PopulateArticles(articles);
        }
        private HttpResponseMessage GetArticles(string url)
        {
            HttpClient client;
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(url).Result;
            return response;
        }

        private List<EverythingFootballDemo.DAL.Article> BuildArticles(HttpResponseMessage response)
        {
            var listArticles = new List<EverythingFootballDemo.DAL.Article>();
            if (response.IsSuccessStatusCode)
            {
                List<RawResult> latestNews = new List<RawResult>();
                string responseString = response.Content.ReadAsStringAsync().Result;
                var objResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);
                var articles = objResponse.Where(x => x.Key.Equals("articles")).FirstOrDefault();
                var arrArticles = (JArray)objResponse["articles"];
                var articleChildren = arrArticles.Children();
                foreach (var child in articleChildren)
                {
                    var article = new EverythingFootballDemo.DAL.Article();
                    article.title = child["title"].Value<string>();
                    article.author = child["author"].Value<string>();
                    article.description = child["description"].Value<string>();
                    article.url = child["url"].Value<string>();
                    article.urlToImage = child["urlToImage"].Value<string>();
                    article.publishedAt = child["publishedAt"].Value<DateTime>();

                    listArticles.Add(article);
                }
            }
            return listArticles;
        }
        private void PopulateArticles(List<EverythingFootballDemo.DAL.Article> articles)
        {
            gvLatestNews.DataSource = articles;
            gvLatestNews.DataBind();
        }
        private void populateMatches(string competition)
        {
            var competitionID = GetCompetitionID(competition);
            /*Fixtures*/
            var responseFixtures = GetMatches(competitionID, "fixtures");
            var fixtures = BuildMatchData(responseFixtures);
            /*Live*/
            var responseLiveScores = GetMatches(competitionID, "live");
            var liveScores = BuildMatchData(responseLiveScores);
            /*Results*/
            var responseResults = GetMatches(competitionID, "results");
            var results = BuildMatchData(responseResults);

            ShowMatches(fixtures, liveScores, results);
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

        private string GetCompetitionID(string competition)
        {
            var competitionID = string.Empty;
            switch (competition)
            {
                case "PL":
                    competitionID = "2";
                    break;
                case "BL":
                    competitionID = "48";
                    break;
                case "SL":
                    competitionID = "46";
                    break;
                case "IL":
                    competitionID = "47";
                    break;
                case "FL":
                    competitionID = "49";
                    break;
                case "CL":
                    competitionID = "372";
                    break;
                default:
                    competitionID = "2";
                    break;
            }
            return competitionID;
        }

        private HttpResponseMessage GetMatches(string competitionID, string type)
        {
            var url = string.Empty;
            HttpClient client;
            if (type.ToUpper().Equals("FIXTURES"))
            {
                url = "https://api.crowdscores.com/v1/matches?from={0}T12:00:00-03:00&competition_id={1}&api_key=7e07e4528e1d4949a7baddc98ec4adde";
                url = string.Format(url, DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"), competitionID);
            }
            else if (type.ToUpper().Equals("LIVE"))
            {
                url = "https://api.crowdscores.com/v1/matches?from={0}T12:00:00-03:00&to={1}T12:00:00-04:00&competition_id={2}&api_key=7e07e4528e1d4949a7baddc98ec4adde";
                url = string.Format(url, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), competitionID);
            }
            else {
                url = "https://api.crowdscores.com/v1/matches?to={0}T12:00:00-03:00&competition_id={1}&api_key=7e07e4528e1d4949a7baddc98ec4adde";
                url = string.Format(url, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), competitionID);
            }
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(url).Result;
            return response;
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
        private List<Matches> BuildMatchData(HttpResponseMessage response)
        {
            var result = response.Content.ReadAsAsync<List<Matches>>().Result;
            return result;
        }
        protected void ddlCompetitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMatchData();
        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMatchData();
        }
        private void PopulateDropDowns(string competition)
        {
            var competitions = GetCompetiotns();
            ddlCompetitions.DataTextField = "Name";
            ddlCompetitions.DataValueField = "Value";

            ddlCompetitions.DataSource = competitions;
            ddlCompetitions.DataBind();
            ddlCompetitions.Enabled = false;


            ddlCompetitions.SelectedValue = GetCompetitionID(competition);
        }
        private DataTable GetCompetiotns()
        {
            var dtCompetitions = new DataTable();
            dtCompetitions.Columns.Add("Name");
            dtCompetitions.Columns.Add("Value");

            dtCompetitions.Rows.Add("Premier League", "2");
            dtCompetitions.Rows.Add("La Liga", "46");
            dtCompetitions.Rows.Add("Bundesliga", "48");
            dtCompetitions.Rows.Add("Serie A", "47");
            dtCompetitions.Rows.Add("Ligue 1", "49");
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

        private string FixturesURL = "https://api.crowdscores.com/v1/matches?from={0}T12:00:00-03:00&to={1}T12:00:00-04:00&competition_id={2}&api_key=7e07e4528e1d4949a7baddc98ec4adde";
        private string NewsURL = "https://newsapi.org/v2/top-headlines?sources={0}&apiKey=2aaefa087142404db935023ddbc72955";
    }
}