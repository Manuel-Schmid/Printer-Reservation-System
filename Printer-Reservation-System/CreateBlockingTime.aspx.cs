using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
			if ((Request.Cookies["secureCookie"] == null) || Session["isAdmin"].ToString() == "False") Response.Redirect("~/Login.aspx");

			conBuilder.DataSource = GlobalVariables.dataSource;
			conBuilder.InitialCatalog = GlobalVariables.dbName;
			conBuilder.IntegratedSecurity = true;
			con.ConnectionString = conBuilder.ConnectionString;

			if (!IsPostBack)
			{
				ddlPrintersBind();
				listStudentsBind();
			}
		}

		private void listStudentsBind()
		{
			CheckBoxList list = listStudents;

			DataTable dt = getStudentTable();
			list.DataSource = dt;
			list.DataTextField = "Schueler";
			list.DataValueField = "Schueler";
			list.DataBind();
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

		private DataTable getStudentTable()
		{
			DataTable tblStudents = new DataTable();

			con.Open();

			SqlCommand cmd = new SqlCommand("spSelectStudentNames", con);

			cmd.CommandType = CommandType.StoredProcedure;

			SqlDataAdapter dap = new SqlDataAdapter(cmd);

			dap.Fill(tblStudents);
			con.Close();

			return tblStudents;
		}

		private void dateVal()
		{
			if (calFromDate.SelectedDate < DateTime.Now || calFromDate.SelectedDate == DateTime.MinValue) calFromDateValidator.IsValid = false;
			if (calToDate.SelectedDate < DateTime.Now || calToDate.SelectedDate == DateTime.MinValue || calToDate.SelectedDate < calFromDate.SelectedDate) calToDateValidator.IsValid = false;
		}

		protected void btnCreate_Click(object sender, EventArgs e)
		{
			lblWrongDateOrder.ForeColor = System.Drawing.Color.Red;
			lblWrongDateOrder.Text = "";
			dateVal();

			if (Page.IsValid)
			{
				try
				{
					DateTime fromDate = Convert.ToDateTime(calFromDate.SelectedDate.ToString().Split(' ')[0] + " " + txtFromTime.Text);
					DateTime toDate = Convert.ToDateTime(calToDate.SelectedDate.ToString().Split(' ')[0] + " " + txtToTime.Text);
					if (fromDate >= toDate)
					{
						lblWrongDateOrder.Text = "Geben Sie eine gültige Zeitspanne ein.";
					}
					else // successful
					{
						List<String> studentList = new List<String>();

						int i = 0;
						foreach (ListItem item in listStudents.Items)
						{
							if (item.Selected)
							{
								studentList.Add(item.Value);
								i++;
							}
						}

						insertBlockingTime(txtAreaReason.Text, int.Parse(ddlPrinters.SelectedValue), fromDate, toDate, studentList, txtAreaComment.Text);
						Response.Redirect("~/BlockingTimesOverview.aspx");
					}
				}

					catch (Exception ex)
				{
					lblWrongDateOrder.Text = "Geben Sie eine gültige Zeitspanne ein.";
				}

			}
		}

		private void insertBlockingTime(string reason, int printerID, DateTime fromDate, DateTime toDate, List<String> students, string comment)
		{
			DataTable studentTable = new DataTable();
			studentTable.Columns.Add(new DataColumn("StudentID", typeof(int)));
			studentTable.Columns.Add(new DataColumn("Name", typeof(string)));
			studentTable.Columns.Add(new DataColumn("Vorname", typeof(string)));

			for (int i = 0; i < students.Count; i++)
			{
				string vorname = students[i].Split(' ')[0];
				string name = students[i].Split(' ')[1];
				studentTable.Rows.Add(i + 1, name, vorname);
			}

			con.Open();
			SqlCommand cmd = new SqlCommand("spInsertBlockingTime", con);

			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@Grund", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@ID_Drucker", SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter("@Von", SqlDbType.DateTime));
			cmd.Parameters.Add(new SqlParameter("@Bis", SqlDbType.DateTime));
			cmd.Parameters.Add(new SqlParameter("@Bemerkung", SqlDbType.Text));
			cmd.Parameters["@Grund"].Value = reason;
			cmd.Parameters["@ID_Drucker"].Value = printerID;
			cmd.Parameters["@Von"].Value = fromDate;
			cmd.Parameters["@Bis"].Value = toDate;
			cmd.Parameters["@Bemerkung"].Value = comment;
			cmd.Parameters.AddWithValue("@Schueler", studentTable);

			cmd.ExecuteNonQuery();
			con.Close();
		}
	}
}