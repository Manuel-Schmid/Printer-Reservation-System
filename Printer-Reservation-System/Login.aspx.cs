using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Printer_Reservation_System
{
    public partial class Login1 : System.Web.UI.Page
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
            conBuilder.DataSource = @"NOTEBOOKMANY\MSSQLSERVER2019";
            conBuilder.InitialCatalog = "3D_Drucker";
            conBuilder.IntegratedSecurity = true;
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
            Response.Redirect("~/Registration.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // remove this !!!!!
            Response.Redirect("~/StudentsOverview.aspx");
            return;
            // remove this !!!!!

            if (Page.IsValid)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = conBuilder.ConnectionString;
                con.Open();

                SqlCommand cmd = new SqlCommand("spValidateLogin", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@eMail", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@Passwort", SqlDbType.VarChar));
                cmd.Parameters["@eMail"].Value = txtEmail.Text;
                cmd.Parameters["@Passwort"].Value = GetHashString(txtPassword.Text);

                bool isValid = 1 == (int)cmd.ExecuteScalar();
                con.Close();

                if (isValid) // login successful
                {
                    lblInvalidLogin.Text = "";
                    Response.Redirect("~/StudentsOverview.aspx");
                    //Response.Redirect("~/PrinterOverview.aspx");
                } else
                {
                    lblInvalidLogin.Text = "Your login credentials were incorrect.";
                }

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

                //Response.Redirect("~/PrinterOverview.aspx");

            }
        }

        private byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}