using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Printer_Reservation_System
{
    public partial class Login : System.Web.UI.Page
    {
        readonly SqlConnectionStringBuilder conBuilder = new SqlConnectionStringBuilder();

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            //load Scriptmanager at Applicytion Start (jquery used in ASP-Validator-Controls)
            // https://msdn.microsoft.com/de-de/library/system.web.ui.scriptmanager.scriptresourcemapping(v=vs.110).aspx            
            //
            ScriptResourceDefinition myScriptResDef = new ScriptResourceDefinition();
            myScriptResDef.Path = "~/Scripts/jquery-1.4.2.min.js";
            myScriptResDef.DebugPath = "~/Scripts/jquery-1.4.2.js";
            myScriptResDef.CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.2.min.js";
            myScriptResDef.CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.2.js";
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", null, myScriptResDef);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpCookie userCookie = Request.Cookies.Get("userCookie");
            //if (IsPostBack && userCookie != null && checkIfCookieIsSet())
            //{
            //    string first = Request.Cookies["userCookie"]["firstName"];
            //    string last = Request.Cookies["userCookie"]["lastName"];
            //    string name = first + " " + last;
            //    litInfo.Text = "Hallo " + name + ", Bitte geben Sie Ihre persönlichen Daten an:";
            //}
            //else
            //{
            //    litInfo.Text = "Bitte geben Sie Ihre persönlichen Daten an:";
            //}

            //if (Session["firstName"] != null)
            //{
                //txtFirstName.Text = Session["firstName"].ToString();
                //txtLastName.Text = Session["lastName"].ToString();
                //txtEmail.Text = Session["email"].ToString();
                //txtHandy.Text = Session["handy"].ToString();
                //txtPassword.Text = Session["class"].ToString();
            //}

            conBuilder.DataSource = @"NOTEBOOKMANY\MSSQLSERVER2019";
            conBuilder.InitialCatalog = "3D_Drucker";
            conBuilder.IntegratedSecurity = true;
        }

        private Boolean checkIfCookieIsSet()
        {
            return (Request.Cookies["userCookie"]["firstName"] != null && Request.Cookies["userCookie"]["lastName"] != null);
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected void email_validator(object source, ServerValidateEventArgs args)
        {
            String value = args.Value.ToString();
            args.IsValid = args != null && IsValidEmail(value) && !String.IsNullOrEmpty(value) && (value != "");
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // set cookie
                //HttpCookie userCookie = new HttpCookie("userCookie");
                //userCookie.Values.Add("firstName", txtFirstName.Text);
                //userCookie.Values.Add("lastName", txtLastName.Text);
                //Response.Cookies.Add(userCookie);

                // set session data
                //Session["firstName"] = txtFirstName.Text;
                //Session["lastName"] = txtLastName.Text;
                //Session["email"] = txtEmail.Text;
                //Session["birthdate"] = txtBirthdate.Text;
                //Session["class"] = txtClass.Text;
                //Session["interest"] = radioInterests.SelectedItem.Value.ToString();

                Response.Redirect("~/PrinterOverview.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}