using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Printer_Reservation_System
{
	public partial class BlockingTimesOverview : System.Web.UI.Page
	{
		readonly SqlConnectionStringBuilder conBuilder = new SqlConnectionStringBuilder();
		SqlConnection con = new SqlConnection();

		protected void Page_Load(object sender, EventArgs e)
		{
			conBuilder.DataSource = GlobalVariables.dataSource;
			conBuilder.InitialCatalog = GlobalVariables.dbName;
			conBuilder.IntegratedSecurity = true;
			con.ConnectionString = conBuilder.ConnectionString;

			if (!IsPostBack)
			{
				gvBindBlockingTimes();
			}
		}

		private void gvBindBlockingTimes()
		{
			DataTable tblBlockingTimes = new DataTable();

			con.Open();

			SqlCommand cmd = new SqlCommand("spSelectBlockingTimes", con);

			cmd.CommandType = CommandType.StoredProcedure;

			SqlDataAdapter dap = new SqlDataAdapter(cmd);

			dap.Fill(tblBlockingTimes);
			con.Close();

			if (tblBlockingTimes.Rows.Count > 0)
			{
				// bind blocking times
				gvBlockingTimes.DataSource = tblBlockingTimes;
				gvBlockingTimes.DataBind();

				// bind exception students
				for (int i = 0; i < gvBlockingTimes.Rows.Count; i++)
				{
					con.Open();
					SqlCommand cmd1 = new SqlCommand("spSelectExceptions", con);

					cmd1.CommandType = CommandType.StoredProcedure;

					cmd1.Parameters.Add(new SqlParameter("@SperrfensterID", SqlDbType.Int));
					cmd1.Parameters["@SperrfensterID"].Value = gvBlockingTimes.Rows[i].Cells[0].Text;

					SqlDataAdapter dap1 = new SqlDataAdapter(cmd1);

					DataTable tblExceptions = new DataTable();
					dap1.Fill(tblExceptions);
					con.Close();

					((BulletedList)gvBlockingTimes.Rows[i].FindControl("bltAusnahmen")).DataTextField = "Schueler";
					((BulletedList)gvBlockingTimes.Rows[i].FindControl("bltAusnahmen")).DataSource = tblExceptions;
					((BulletedList)gvBlockingTimes.Rows[i].FindControl("bltAusnahmen")).DataBind();
				}
			}
			else
			{
				tblBlockingTimes.Rows.Add(tblBlockingTimes.NewRow());
				gvBlockingTimes.DataSource = tblBlockingTimes;
				gvBlockingTimes.DataBind();
				int columncount = gvBlockingTimes.Rows[0].Cells.Count;
				gvBlockingTimes.Rows[0].Cells.Clear();
				gvBlockingTimes.Rows[0].Cells.Add(new TableCell());
				gvBlockingTimes.Rows[0].Cells[0].ColumnSpan = columncount;
				gvBlockingTimes.Rows[0].Cells[0].Text = "No BlockingTimes Found";
			}
		}


		protected void gvBlockingTimes_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridViewRow row = (GridViewRow)gvBlockingTimes.Rows[e.RowIndex];
			con.Open();
			SqlCommand cmd = new SqlCommand("spDeleteBlockingTime", con);

			cmd.CommandType = CommandType.StoredProcedure;

			DataRowView dr = row.DataItem as DataRowView;

			cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
			cmd.Parameters["@ID"].Value = row.Cells[0].Text;

			cmd.ExecuteNonQuery();
			con.Close();
			gvBindBlockingTimes();
		}

		protected void gvBlockingTimes_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			GridViewRow row = (GridViewRow)gvBlockingTimes.Rows[e.RowIndex];
			gvBlockingTimes.EditIndex = -1;

			con.Open();
			SqlCommand cmd;

			cmd = new SqlCommand("spUpdateBlockingTime", con);

			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter("@Grund", SqlDbType.Text));
			cmd.Parameters.Add(new SqlParameter("@ID_Drucker", SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter("@Von", SqlDbType.DateTime));
			cmd.Parameters.Add(new SqlParameter("@Bis", SqlDbType.DateTime));
			cmd.Parameters.Add(new SqlParameter("@Bemerkung", SqlDbType.Text));
			cmd.Parameters["@ID"].Value = row.Cells[0].Text;
			cmd.Parameters["@Grund"].Value = ((TextBox)row.Cells[1].Controls[0]).Text;
			cmd.Parameters["@ID_Drucker"].Value = ((DropDownList)row.FindControl("ddl_Drucker")).SelectedValue;
			cmd.Parameters["@Von"].Value = ((TextBox)row.Cells[3].Controls[0]).Text;
			cmd.Parameters["@Bis"].Value = ((TextBox)row.Cells[4].Controls[0]).Text;
			cmd.Parameters["@Bemerkung"].Value = ((TextBox)row.Cells[6].Controls[0]).Text;

			cmd.ExecuteNonQuery();
			con.Close();

			gvBindBlockingTimes();
		}

		protected void gvBlockingTimes_RowEditing(object sender, GridViewEditEventArgs e)
		{
			gvBlockingTimes.EditIndex = e.NewEditIndex;
			gvBindBlockingTimes();
		}

		protected void gvBlockingTimes_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvBlockingTimes.PageIndex = e.NewPageIndex;
			gvBindBlockingTimes();
		}

		protected void gvBlockingTimes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			gvBlockingTimes.EditIndex = -1;
			gvBindBlockingTimes();
		}


		protected void gvBlockingTimesRowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if ((e.Row.RowState & DataControlRowState.Edit) > 0)
				{
					DropDownList ddList = (DropDownList)e.Row.FindControl("ddl_Drucker");

					DataTable dt = getPrinterTable();
					ddList.DataSource = dt;
					ddList.DataTextField = "DruckerName";
					ddList.DataValueField = "DruckerID";
					ddList.DataBind();

					DataRowView dr = e.Row.DataItem as DataRowView;
					ddList.SelectedValue = dr["ID_Drucker"].ToString();
				}
			}
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
		protected void btnCreateBlockingTime_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/CreateBlockingTime.aspx");
		}
	}
}