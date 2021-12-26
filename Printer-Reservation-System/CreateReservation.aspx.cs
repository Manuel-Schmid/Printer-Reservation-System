using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Printer_Reservation_System
{
    public partial class CreateReservation : System.Web.UI.Page
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
            conBuilder.DataSource = @"NOTEBOOKMANY\MSSQLSERVER2019";
            conBuilder.InitialCatalog = "3D_Drucker";
            conBuilder.IntegratedSecurity = true;
            con.ConnectionString = conBuilder.ConnectionString;

            if (!IsPostBack)
            {
                ddlPrintersBind();
            }
        }

        private void ddlPrintersBind()
        {
            DropDownList ddList = ddlPrinters;

            //return DataTable havinf department data
            DataTable dt = getPrinterTable();
            ddList.DataSource = dt;
            ddList.DataTextField = "DruckerName";
            ddList.DataValueField = "DruckerID";
            ddList.DataBind();

            //DataRowView dr = e.Row.DataItem as DataRowView;
            //ddList.SelectedValue = dr[""].ToString();
        }

        private DataTable getPrinterTable()
        {
            DataTable tblPrinters = new DataTable();

            con.Open();

            SqlCommand cmd = new SqlCommand("spSelectDDLPrinters", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter dap = new SqlDataAdapter(cmd);

            dap.Fill(tblPrinters);
            con.Close();

            return tblPrinters;
        }


        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Label1.Text = ddlPrinters.SelectedValue;
        }
    }
}