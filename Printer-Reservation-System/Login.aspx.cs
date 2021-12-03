using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Printer_Reservation_System
{
    public partial class Login1 : System.Web.UI.Page
    {
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
    }
}