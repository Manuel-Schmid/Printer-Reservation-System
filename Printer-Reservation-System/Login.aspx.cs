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
        SqlConnection con = new SqlConnection();

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
            conBuilder.DataSource = GlobalVariables.dataSource;
            conBuilder.InitialCatalog = GlobalVariables.dbName;
            conBuilder.IntegratedSecurity = true;
            con.ConnectionString = conBuilder.ConnectionString;
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
<<<<<<< Updated upstream
            //Response.Redirect("~/RegistrationsOverview.aspx");
            //return;
=======
            Response.Redirect("~/ReservationsOverview.aspx");
            return;
>>>>>>> Stashed changes
            // remove this !!!!!

            if (Page.IsValid)
            {
                if (isLoginValid())
                {
                    switch (getStudentStatus(txtEmail.Text))
                    {
                        case "Aktiv":
                            lblInvalidLogin.Text = "";
                            Session["email"] = txtEmail.Text;
                            Session["isAdmin"] = IsStudentAdmin(txtEmail.Text);
                            Response.Redirect("~/ReservationsOverview.aspx");
                            break;

                        case "Anfrage Registration ":
                            lblInvalidLogin.Text = "Ihre Registrationsanfrage wurde gespeichert und ist noch in Bearbeitung. Dies kann 2 - 5 Tage dauern.";
                            break;

                        case "Gesperrt":
                            lblInvalidLogin.Text = "Dieses Konto wurde durch einen Administrator gesperrt.";
                            break;

                        case "Beendet":
                            lblInvalidLogin.Text = "Die Registration dieses Kontos wurde von einem Administrator abgelehnt.";
                            break;

                        default:
                            lblInvalidLogin.Text = "An error occured.";
                            break;
                    }
                } else
                {
                    lblInvalidLogin.Text = "Die eingegebenen Login-Daten sind inkorrekt.";
                }
            

                // set cookie
                //HttpCookie userCookie = new HttpCookie("userCookie");
                //userCookie.Values.Add("firstName", txtFirstName.Text);
                //userCookie.Values.Add("lastName", txtLastName.Text);
                //Response.Cookies.Add(userCookie);
            }
        }

        private bool isLoginValid()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("spValidateLogin", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@eMail", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@Passwort", SqlDbType.VarChar));
            cmd.Parameters["@eMail"].Value = txtEmail.Text;
            cmd.Parameters["@Passwort"].Value = GetHashString(txtPassword.Text);

            bool isValid = (int)cmd.ExecuteScalar() >= 1;
            con.Close();
            return isValid;
        }

        private bool IsStudentAdmin(string eMail)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("spSelectIsStudentAdmin", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@eMail", SqlDbType.VarChar));
            cmd.Parameters["@eMail"].Value = eMail;

            bool isValid = ((int)cmd.ExecuteScalar() >= 1);
            con.Close();
            return isValid;
        }

        private string getStudentStatus(string eMail)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spSelectStudentStatus", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@eMail", SqlDbType.VarChar));
            cmd.Parameters["@eMail"].Value = eMail;


            string status = "";

            object o = cmd.ExecuteScalar();
            con.Close();

            if (o != null)
            {
                status = o.ToString();
            }
            else { status = "error"; }
            return status;
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