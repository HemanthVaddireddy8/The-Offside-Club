using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EverythingFootballDemo.DAL;
using System.Globalization;

namespace EverythingFootballDemo
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                FillDetails();
                tblCreditCard.Visible = false;
            }
        }
        private void FillDetails() {
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

        protected void chkSaveCreditCardInfo_CheckedChanged(object sender, EventArgs e)
        {
            tblCreditCard.Visible = chkSaveCreditCardInfo.Checked;
            this.txtPassword.Attributes.Add("value", this.txtPassword.Text);
            this.txtConfirmPassword.Attributes.Add("value", this.txtConfirmPassword.Text);
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            var user = new EverythingFootballDemo.DAL.User();
            user.UserName = txtUserName.Text;
            user.Password = txtPassword.Text;
            user.EmailID = txtEmailAddress.Text;
            user.CreditCardNumber = Convert.ToInt64(txtCreditCardNum.Text);
            user.SecurityCode = Convert.ToInt16(txtSecCode.Text);
            user.NameOnCard = txtNameOnCard.Text;
            user.CCExpMonth = ddlMonth.SelectedItem.Text;
            user.CCExpYear = ddlYear.SelectedValue;
            user.PhoneNumber = Convert.ToInt64(txtPhoneNumber.Text);
            var isUserValid = ValidateUserDetails(user);
            if (isUserValid) {
                var usersBLL = new EverythingFootballDemo.BLL.Users();
                var isUserCreatedSuccessfully = usersBLL.SaveUser(user);
                if (isUserCreatedSuccessfully)
                {
                    Session["userInfo"] = user;
                    divRegister.Visible = false;
                    divStatus.Visible = true;
                    lblStatusMsg.Text = "Congratulations. You have successfully joined The Offside Club.";
                    hlnkHomePage.NavigateUrl = "HomePage.aspx?UserReg=Y";
                }
                else {
                    divRegister.Visible = false;
                    divStatus.Visible = true;
                    lblStatusMsg.Text = "Sorry. We are unable to process your request. Please try later.";
                    hlnkHomePage.NavigateUrl = "HomePage.aspx?UserReg=N";
                }
            }
        }

        private bool ValidateUserDetails(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password)
                || string.IsNullOrEmpty(user.EmailID) || string.IsNullOrEmpty(user.NameOnCard) 
                || string.IsNullOrEmpty(ddlMonth.SelectedValue) || string.IsNullOrEmpty(ddlYear.SelectedValue)) {
                return false;
            }
            var extensions = new EverythingFootballDemo.BLL.Extensions();
            if (!extensions.isInt(user.SecurityCode.ToString()) || !extensions.isLong(user.CreditCardNumber.ToString())) {
                return false;
            }
            if (!extensions.isValidEmailID(user.EmailID)) {
                return false;
            }
            return true;
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
    }
}