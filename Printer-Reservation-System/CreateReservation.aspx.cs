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
		private void dateVal()
		{
			if (calFromDate.SelectedDate == DateTime.MinValue) calFromDateValidator.IsValid = false;
			if (calToDate.SelectedDate == DateTime.MinValue) calToDateValidator.IsValid = false;
		}

		protected void btnCreate_Click(object sender, EventArgs e)
		{
			lblReservationError.ForeColor = System.Drawing.Color.Red;
			lblReservationError.Text = "";
			dateVal();

			if (Page.IsValid)
			{
				try
				{
					
					DateTime fromDate = Convert.ToDateTime(calFromDate.SelectedDate.ToString().Split(' ')[0] + " " + txtFromTime.Text);
					DateTime toDate = Convert.ToDateTime(calToDate.SelectedDate.ToString().Split(' ')[0] + " " + txtToTime.Text);
					if (fromDate >= toDate)
					{
						lblReservationError.Text = "Geben Sie eine gültige Zeitspanne ein.";
					}
					else
					{
						int printerID = int.Parse(ddlPrinters.SelectedValue);
						int blockingTimeOverlapsCount = (Session["isAdmin"].ToString() == "False") ? overlapsBlockingTime(printerID, fromDate, toDate) : 0;
						int reservationOverlapsCount = overlapsReservation(printerID, fromDate, toDate);

						if (blockingTimeOverlapsCount > 0)
						{
							lblReservationError.Text = "Diese Zeitspanne überschneidet sich mit " + blockingTimeOverlapsCount + " Sperrfenster" + (blockingTimeOverlapsCount > 1 ? "n" : "") + ". Bitte passen Sie die Zeitspanne entsprechend an.";
						}
						else if (reservationOverlapsCount > 0)
						{
							lblReservationError.Text = "Diese Zeitspanne überschneidet sich mit " + reservationOverlapsCount + " anderen Reservation" + (reservationOverlapsCount > 1 ? "en" : "") + ". Bitte passen Sie die Zeitspanne entsprechend an.";
						}
						else // successful
						{
							insertReservation(printerID, Session["email"].ToString(), fromDate, toDate, txtAreaComment.Text);
							Response.Redirect("~/ReservationsOverview.aspx");
						}
					}
				} catch (Exception ex)
				{
					lblReservationError.Text = "Geben Sie eine gültige Zeitspanne ein.";
				}

			}
		}

		private int overlapsBlockingTime(int printerID, DateTime fromDate, DateTime toDate)
		{
			con.Open();

			SqlCommand cmd = new SqlCommand("spOverlapsBlockingTime", con);

			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@Student_eMail", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@ID_Drucker", SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter("@Von", SqlDbType.DateTime));
			cmd.Parameters.Add(new SqlParameter("@Bis", SqlDbType.DateTime));
			cmd.Parameters["@Student_eMail"].Value = Session["email"].ToString();
			cmd.Parameters["@ID_Drucker"].Value = printerID;
			cmd.Parameters["@Von"].Value = fromDate;
			cmd.Parameters["@Bis"].Value = toDate;

			int overlapsWith = (int)cmd.ExecuteScalar();
			con.Close();
			return overlapsWith;
		}


		private int overlapsReservation(int printerID, DateTime fromDate, DateTime toDate)
		{
			con.Open();

			SqlCommand cmd = new SqlCommand("spOverlapsReservation", con);

			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@ID_Drucker", SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter("@Von", SqlDbType.DateTime));
			cmd.Parameters.Add(new SqlParameter("@Bis", SqlDbType.DateTime));
			cmd.Parameters["@ID_Drucker"].Value = printerID;
			cmd.Parameters["@Von"].Value = fromDate;
			cmd.Parameters["@Bis"].Value = toDate;

			int overlapsWith = (int)cmd.ExecuteScalar();
			con.Close();
			return overlapsWith;
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