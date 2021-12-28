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
	public partial class CreateBlockingTime : System.Web.UI.Page
	{
		readonly SqlConnectionStringBuilder conBuilder = new SqlConnectionStringBuilder();
		SqlConnection con = new SqlConnection();

		protected override void OnInit(EventArgs e)
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

			if (!IsPostBack)
			{
				ddlPrintersBind();
			}
		}

		private void ddlPrintersBind()
		{
			DropDownList ddList = ddlPrinters;

			DataTable dt = getPrinterTable();
			ddList.DataSource = dt;
			ddList.DataTextField = "DruckerName";
			ddList.DataValueField = "DruckerID";
			ddList.DataBind();
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
			lblWrongDateOrder.ForeColor = System.Drawing.Color.Red;
			lblWrongDateOrder.Text = "";
			if (Page.IsValid)
			{
				try
				{
					DateTime fromDate = Convert.ToDateTime(txtFromDate.Text + " " + txtFromTime.Text);
					DateTime toDate = Convert.ToDateTime(txtToDate.Text + " " + txtToTime.Text);
					if (fromDate >= toDate)
					{
						lblWrongDateOrder.Text = "Geben Sie eine gültige Zeitspanne ein.";
					}
					else // successful
					{
						insertReservation(int.Parse(ddlPrinters.SelectedValue), Session["email"].ToString(), fromDate, toDate, txtAreaComment.Text);
						Response.Redirect("~/ReservationsOverview.aspx");
					}
				}
				catch (Exception ex)
				{
					lblWrongDateOrder.Text = "Geben Sie eine gültige Zeitspanne ein.";
				}

			}
		}

		private void insertReservation(int printerID, string studentEmail, DateTime fromDate, DateTime toDate, string comment)
		{
			con.Open();
			SqlCommand cmd = new SqlCommand("spInsertReservation", con);

			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@ID_Drucker", SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter("@Student_eMail", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Von", SqlDbType.DateTime));
			cmd.Parameters.Add(new SqlParameter("@Bis", SqlDbType.DateTime));
			cmd.Parameters.Add(new SqlParameter("@Bemerkung", SqlDbType.Text));
			cmd.Parameters["@ID_Drucker"].Value = printerID;
			cmd.Parameters["@Student_eMail"].Value = studentEmail;
			cmd.Parameters["@Von"].Value = fromDate;
			cmd.Parameters["@Bis"].Value = toDate;
			cmd.Parameters["@Bemerkung"].Value = comment;

			cmd.ExecuteNonQuery();
			con.Close();
		}
	}
}