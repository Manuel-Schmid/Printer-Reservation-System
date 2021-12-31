using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

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
            conBuilder.DataSource = GlobalVariables.dataSource;
            conBuilder.InitialCatalog = GlobalVariables.dbName;
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
            if (Page.IsValid)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = conBuilder.ConnectionString;
                con.Open();

                SqlCommand cmd = new SqlCommand("spInsertStudent", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@Vorname", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@eMail", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@Handy", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@Passwort", SqlDbType.VarChar));
                cmd.Parameters["@Name"].Value = String.Concat((txtLastName.Text).Where(c => !Char.IsWhiteSpace(c)));
                cmd.Parameters["@Vorname"].Value = String.Concat((txtFirstName.Text).Where(c => !Char.IsWhiteSpace(c)));
                cmd.Parameters["@eMail"].Value = String.Concat((txtEmail.Text).Where(c => !Char.IsWhiteSpace(c)));
                cmd.Parameters["@Handy"].Value = txtHandy.Text;
                cmd.Parameters["@Passwort"].Value = GetHashString(txtPassword.Text);

                try
                {
                    cmd.ExecuteNonQuery();
                    lblMsg.Text = "Ihre Registrationsanfrage wurde gespeichert. Bitte warten Sie bis Ihre Anfrage von einem Adminsitrator angenommen wird. Dies kann 2 - 5 Tage dauern.";
                }
                catch
                {
                    duplicateEmailValidator.IsValid = false;
                }
                con.Close();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
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
