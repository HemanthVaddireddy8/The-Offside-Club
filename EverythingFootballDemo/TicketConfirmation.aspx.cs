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
    public partial class TicketConfirmation : System.Web.UI.Page
    {

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private static int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                //SendOTP();
                divStatus.Visible = false;
                tblOTP.Visible = true;
                btnCancelPayment.Visible = true;
                btnConfirmPayment.Visible = true;
            }
        }
        protected void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            var randomNumber = Session["OTP"].ToString();
            if (txtOTP.Text.Equals(randomNumber))
            {
                GenerateTicket();
                divStatus.Visible = true;
                tblOTP.Visible = false;
                lblStatusMsg.Text = "Congratulations. Your booking was successful. \n Your ticket has been sent to the registered email address";
                Session["OTP"] = null;
            }
            else {
                if (count == 0)
                {
                    count++;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "validateOTP()", true);
                }
                else {
                    divStatus.Visible = true;
                    tblOTP.Visible = false;
                    lblStatusMsg.Text = "Sorry. \nYour transaction has been cancelled due to incorrect one-time password. \n Please try again.";
                    Session["OTP"] = null;
                    System.Threading.Thread.Sleep(10000);

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "validateOTPFinal()", true);
                    Response.Redirect("HomePage.aspx");
                }
            }
        }

        protected void btnCancelPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("Fixtures.aspx");
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

                var client = new Client("hemanthvaddireddy", "T75TkBTd9SV62mledlizx8JJfssRvl");
                var link = client.SendMessage(msgBody, toNumber);
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
        private void GenerateTicket()
        {
            var pdf = new Aspose.Pdf.Document();
            //Add a page to the document
            var pdfTicketPage = pdf.Pages.Add();
            
            var strBody = Session["TicketBody"].ToString();
            var fragment = new Aspose.Pdf.Text.TextFragment(strBody.ToString());
            pdfTicketPage.Paragraphs.Add(fragment);

            DirectoryInfo dir = new DirectoryInfo(@"D:\Images\");
            FileInfo fileInfo = dir.GetFiles("*.jpg").FirstOrDefault();

            FileStream stream = new FileStream(fileInfo.FullName, FileMode.Open);
            System.Drawing.Image img = new System.Drawing.Bitmap(stream);
            var image = new Aspose.Pdf.Image { ImageStream = stream };
            image.FixHeight = 125;
            image.FixWidth = 300;
            image.Margin = new MarginInfo(5, 5, 5, 5);
            pdfTicketPage.Paragraphs.Add(image);

            pdf.Save(@"D:\Images\Ticket.pdf");
            sendEmail();
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
    }
}