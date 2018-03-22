using System;
using System.IO;
using System.Reflection;
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
using System.Net.Mail;
using System.Configuration;
using System.Threading;

using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using EverythingFootballDemo.DAL;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net;
using System.Text;
using System.Collections.Specialized;

using TextmagicRest;
using TextmagicRest.Model;

using Aspose.Pdf;

namespace EverythingFootballDemo
{
    public partial class BookTickets : System.Web.UI.Page
    {
        private static bool isFirstPageLoad = false;
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateFixtureInfo();
                populateUserInfo();
                populateTicketInfo();
                ShowProfile();
                FillDetails();
                isFirstPageLoad = true;
                string script = "$(document).ready(function () { $('[id*=btnMakePayment]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
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
        private void populateUserInfo()
        {
            if (Session["userInfo"] != null)
            {
                var user = new EverythingFootballDemo.DAL.User();
                user = (EverythingFootballDemo.DAL.User)Session["userInfo"];



            }
        }

        private void populateFixtureInfo()
        {
            if (Session["bookingFixture"] != null)
            {
                var bookingFixtureInfo = new EverythingFootballDemo.DAL.BookingFixture();
                bookingFixtureInfo = (EverythingFootballDemo.DAL.BookingFixture)Session["bookingFixture"];

                lblHomeTeam.Text = bookingFixtureInfo.HomeTeamName;
                imgHomeTeam.ImageUrl = bookingFixtureInfo.HomeTeamUrl;
                lblAwayTeam.Text = bookingFixtureInfo.AwayTeamName;
                imgAwayTeam.ImageUrl = bookingFixtureInfo.AwayTeamUrl;
                lblCompetitionName.Text = bookingFixtureInfo.CompetitionName;
                lblTeamID.Text = bookingFixtureInfo.HomeTeamID.ToString();
                GetHomeTeamInfo(bookingFixtureInfo.HomeTeamID.ToString());
            }
        }
        private void GetHomeTeamInfo(string teamID)
        {
            var url = string.Format("https://api.crowdscores.com/v1/teams/{0}?api_key=7e07e4528e1d4949a7baddc98ec4adde", teamID);
            var Client = new HttpClient();
            Client.BaseAddress = new Uri(url);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = Client.GetAsync(url).Result;
            var result = response.Content.ReadAsAsync<Team>().Result;

            var test = result;
            lblStadiumName.Text = test.defaultHomeVenue.name;
            lblStadiumCapacity.Text = test.defaultHomeVenue.capacity.ToString();
        }
        private void populateTicketInfo()
        {
            ddlSelectTickets.Items.AddRange(Enumerable.Range(1, 10).Select(e => new ListItem(e.ToString())).ToArray());
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");

            dt.Rows.Add("Anfield Road End - 10 USD", "10");
            dt.Rows.Add("Kop stand - 15 USD", "15");
            dt.Rows.Add("Main Stand - 12 USD", "12");
            dt.Rows.Add("Kenny Dalglish stand - 18 USD", "18");

            ddlStand.DataTextField = "Name";
            ddlStand.DataValueField = "Value";

            ddlStand.DataSource = dt;
            ddlStand.DataBind();
        }

        protected void ddlStand_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateTotalPrice();
        }

        protected void ddlSelectTickets_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateTotalPrice();
        }
        private void populateTotalPrice()
        {
            lblAmount.Text = "Total Amount Payable: " + (Convert.ToInt32(ddlSelectTickets.SelectedItem.Text) *
                Convert.ToInt32(string.IsNullOrEmpty(ddlStand.SelectedValue) ? "0" : ddlStand.SelectedValue)).ToString()
                + " USD";
        }

        protected void chkSaveCreditCardInfo_CheckedChanged(object sender, EventArgs e)
        {
            var chkUseExistingCreditCard = sender as CheckBox;
            if (chkSaveCreditCardInfo.Checked)
            {
                if (Session["userInfo"] != null)
                {
                    var user = new EverythingFootballDemo.DAL.User();
                    user = (EverythingFootballDemo.DAL.User)Session["userInfo"];
                    txtCreditCardNum.Text = user.CreditCardNumber.ToString();
                    txtNameOnCard.Text = user.NameOnCard.ToString();
                    txtSecCode.Text = user.SecurityCode.ToString();
                    ddlMonth.SelectedItem.Text = user.CCExpMonth.ToString();
                    ddlYear.SelectedItem.Text = user.CCExpYear.ToString();

                    tblCreditCard.Enabled = false;
                }
            }
            else
            {
                txtCreditCardNum.Text = string.Empty;
                txtNameOnCard.Text = string.Empty;
                txtSecCode.Text = string.Empty;
                ddlMonth.SelectedValue = "0";
                ddlYear.SelectedValue = "2018";
            }
        }
        private void FillDetails()
        {
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < months.Length; i++)
            {
                ddlMonth.Items.Add(new ListItem(months[i], i.ToString()));
            }
            var data = GetYearField();
            ddlYear.DataTextField = "Name";
            ddlYear.DataValueField = "Value";
            ddlYear.DataSource = data;
            ddlYear.DataBind();
        }
        private DataTable GetYearField()
        {
            var dtCompetitions = new DataTable();
            dtCompetitions.Columns.Add("Name");
            dtCompetitions.Columns.Add("Value");

            dtCompetitions.Rows.Add("2018", "2018");
            dtCompetitions.Rows.Add("2018", "2018");
            dtCompetitions.Rows.Add("2019", "2019");
            dtCompetitions.Rows.Add("2020", "2020");
            dtCompetitions.Rows.Add("2021", "2021");
            dtCompetitions.Rows.Add("2022", "2022");
            dtCompetitions.Rows.Add("2023", "2023");
            dtCompetitions.Rows.Add("2024", "2024");
            dtCompetitions.Rows.Add("2025", "2025");
            dtCompetitions.Rows.Add("", "");

            return dtCompetitions;
        }

        protected void btnMakePayment_Click(object sender, EventArgs e)
        {
            if (ValidateAllFields())
            {
                System.Threading.Thread.Sleep(5000);
                lblOTP.Visible = true;
                txtOTP.Visible = true;
                btnConfirmPayment.Visible = true;
                btnCancelPayment.Visible = true;
                btnMakePayment.Visible = false;
                divCardInfo.Disabled = true;

                GenerateTicketBody();
                SendOTP();
                Response.Redirect("TicketConfirmation.aspx");
            }
            else
            {
                lblOTP.Visible = false;
                txtOTP.Visible = false;
                btnConfirmPayment.Visible = false;
                btnCancelPayment.Visible = false;
                btnMakePayment.Visible = true;
                divCardInfo.Disabled = true;

                if(!isFirstPageLoad)ClientScript.RegisterStartupScript(this.GetType(), "alert", "CCInfoAlert()", true);
                isFirstPageLoad = false;

            }
        }
        private bool ValidateAllFields()
        {
            bool allFieldsValid = false;
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(txtCreditCardNum.Text) || string.IsNullOrEmpty(txtSecCode.Text) ||
                string.IsNullOrEmpty(txtNameOnCard.Text) || string.IsNullOrEmpty(ddlMonth.SelectedItem.Text) ||
                string.IsNullOrEmpty(ddlYear.SelectedItem.Text))
            {
                allFieldsValid = false;
            }
            else
            {
                allFieldsValid = true;
            }
            return allFieldsValid;
        }
        private bool SendOTP()
        {
            bool msgSent = false;
            try
            {
                var randomNumber = RandomNumber(10000, 99999);
                var user = new EverythingFootballDemo.DAL.User();
                user = (EverythingFootballDemo.DAL.User)Session["userInfo"];
                var msgBody = "Your OTP for the transaction on The Offside Club is " + randomNumber.ToString();
                var toNumber = "+1" + (user.PhoneNumber).ToString();

                //var client = new Client("hemanthvaddireddy", "T75TkBTd9SV62mledlizx8JJfssRvl");
                var client = new Client("kowshikchitti", "Rzt0hqwTVGx9jvLjXLJl438vAiXDZd");
                var link = client.SendMessage(msgBody, toNumber);
                Session["OTP"] = randomNumber.ToString();
                if (link.Success) { msgSent = true; } else { msgSent = false; }
            }
            catch (Exception e)
            {
                if (e.Message != null)
                {
                    msgSent = false;
                }
            }
            return msgSent;
        }
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }
        private void GenerateTicketBody()
        {
            var strBody = new StringBuilder();
            strBody.AppendLine("=================== THE OFFSIDE CLUB ========================");
            strBody.AppendLine("");
            strBody.AppendLine("");
            strBody.AppendFormat("                  {0}", lblCompetitionName.Text);
            strBody.AppendLine("");
            strBody.AppendLine("");
            strBody.AppendFormat("                  {0} - {1}", lblHomeTeam.Text, lblAwayTeam.Text);
            strBody.AppendLine("");
            strBody.AppendLine("");
            strBody.AppendFormat("                  Stadium - {0}", lblStadiumName.Text);
            strBody.AppendLine("");
            strBody.AppendLine("");
            strBody.AppendFormat("                  Capacity - {0}", lblStadiumCapacity.Text);
            strBody.AppendLine("");
            strBody.AppendLine("");
            strBody.AppendFormat("                  Amount - {0}", GeneratePriceForPDF());
            strBody.AppendLine("");
            strBody.AppendLine("");
            strBody.AppendLine("=================== THE OFFSIDE CLUB ========================");

            Session["TicketBody"] = strBody;

            //var fragment = new Aspose.Pdf.Text.TextFragment(strBody.ToString());
            //pdfTicketPage.Paragraphs.Add(fragment);

            //DirectoryInfo dir = new DirectoryInfo(@"D:\Images\");
            //FileInfo fileInfo = dir.GetFiles("*.jpg").FirstOrDefault();

            //FileStream stream = new FileStream(fileInfo.FullName, FileMode.Open);
            //System.Drawing.Image img = new System.Drawing.Bitmap(stream);
            //var image = new Aspose.Pdf.Image { ImageStream = stream };
            //image.FixHeight = 125;
            //image.FixWidth = 300;
            //image.Margin = new MarginInfo(5, 5, 5, 5);
            //pdfTicketPage.Paragraphs.Add(image);

            //pdf.Save(@"D:\Images\Ticket.pdf");
            //sendEmail();
        }

        private void sendEmail()
        {
            var user = new EverythingFootballDemo.DAL.User();
            user = (EverythingFootballDemo.DAL.User)Session["userInfo"];
            string strFrom = ConfigurationManager.AppSettings["SenderEmail"].ToString();
            var mailMsg = new MailMessage(strFrom, user.EmailID);
            Attachment attachment;
            attachment = new System.Net.Mail.Attachment(@"D:\Images\Ticket.pdf");
            mailMsg.Attachments.Add(attachment);
            mailMsg.Body = "Congratulations. Your ticket has been generated succesfully";
            mailMsg.Subject = "Ticket Booking Confirmation - The Offside Club";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential()
            {
                UserName = ConfigurationManager.AppSettings["UserName"].ToString(),
                Password = ConfigurationManager.AppSettings["Password"].ToString()
            };
            smtp.EnableSsl = true;
            smtp.Send(mailMsg);
        }

        protected void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            GenerateTicketBody();
        }

        protected void btnCancelPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("Fixtures.aspx");
        }
        private string GeneratePriceForPDF() {
            return (Convert.ToInt32(ddlSelectTickets.SelectedItem.Text) *
                Convert.ToInt32(string.IsNullOrEmpty(ddlStand.SelectedValue) ? "0" : ddlStand.SelectedValue)).ToString()
                + " USD";
        }
    }
}