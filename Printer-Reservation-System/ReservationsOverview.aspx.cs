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
	public partial class ReservationsOverview : System.Web.UI.Page
	{
		readonly SqlConnectionStringBuilder conBuilder = new SqlConnectionStringBuilder();
		SqlConnection con = new SqlConnection();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.Cookies["secureCookie"] == null) Response.Redirect("~/Login.aspx");

			if (Session["isAdmin"].ToString() == "True")
			{
				gvAllRes.Visible = false;
				gvAllResTxt.Visible = false;
				lblgvReservations.Text = "Reservationen";
			}
			else
			{
				lblgvReservations.Text = "Eigene Reservationen";
			}

			conBuilder.DataSource = GlobalVariables.dataSource;
			conBuilder.InitialCatalog = GlobalVariables.dbName;
			conBuilder.IntegratedSecurity = true;
			con.ConnectionString = conBuilder.ConnectionString;

			if (!IsPostBack)
			{
				gvBindReservations();
			}
		}

		private void gvBindReservations()
		{
			DataTable tblReservations = new DataTable();

			con.Open();

			SqlCommand cmd;

			if (Session["isAdmin"].ToString() == "False") cmd = new SqlCommand("spSelectOwnReservations", con);
			else cmd = new SqlCommand("spSelectAllReservations", con);

			cmd.CommandType = CommandType.StoredProcedure;

			if (Session["isAdmin"].ToString() == "False")
			{
				cmd.Parameters.Add(new SqlParameter("@eMail", SqlDbType.VarChar));
				cmd.Parameters["@eMail"].Value = Session["email"].ToString();
			}


			SqlDataAdapter dap = new SqlDataAdapter(cmd);

			dap.Fill(tblReservations);
			con.Close();

			if (tblReservations.Rows.Count > 0)
			{
				gvReservations.DataSource = tblReservations;
				gvReservations.DataBind();
			}
			else
			{
				tblReservations.Rows.Add(tblReservations.NewRow());
				gvReservations.DataSource = tblReservations;
				gvReservations.DataBind();
				int columncount = gvReservations.Rows[0].Cells.Count;
				gvReservations.Rows[0].Cells.Clear();
				gvReservations.Rows[0].Cells.Add(new TableCell());
				gvReservations.Rows[0].Cells[0].ColumnSpan = columncount;
				gvReservations.Rows[0].Cells[0].Text = "Keine Reservationen vorhanden";
			}

			if (Session["isAdmin"].ToString() == "False")
			{
				gvBindAllRes();
			}
		}

		private void gvBindAllRes()
		{
			DataTable tblReservations = new DataTable();

			con.Open();

			SqlCommand cmd = new SqlCommand("spSelectAllReservations", con);

			cmd.CommandType = CommandType.StoredProcedure;

			SqlDataAdapter dap = new SqlDataAdapter(cmd);

			dap.Fill(tblReservations);
			con.Close();

			if (tblReservations.Rows.Count > 0)
			{
				gvAllRes.DataSource = tblReservations;
				gvAllRes.DataBind();
			}
			else
			{
				tblReservations.Rows.Add(tblReservations.NewRow());
				gvAllRes.DataSource = tblReservations;
				gvAllRes.DataBind();
				int columncount = gvAllRes.Rows[0].Cells.Count;
				gvAllRes.Rows[0].Cells.Clear();
				gvAllRes.Rows[0].Cells.Add(new TableCell());
				gvAllRes.Rows[0].Cells[0].ColumnSpan = columncount;
				gvAllRes.Rows[0].Cells[0].Text = "Keine Reservationen vorhanden";
			}
		}

		protected void gvReservations_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridViewRow row = (GridViewRow)gvReservations.Rows[e.RowIndex];
			con.Open();
			SqlCommand cmd = new SqlCommand("spDeleteReservation", con);

			cmd.CommandType = CommandType.StoredProcedure;

			DataRowView dr = row.DataItem as DataRowView;

			cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
			cmd.Parameters["@ID"].Value = row.Cells[0].Text;

			cmd.ExecuteNonQuery();
			con.Close();
			gvBindReservations();
		}

		protected void gvReservations_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			GridViewRow row = (GridViewRow)gvReservations.Rows[e.RowIndex];
			gvReservations.EditIndex = -1;

			con.Open();
			SqlCommand cmd;

			cmd = new SqlCommand("spUpdateReservation", con);

			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter("@ID_Drucker", SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter("@Von", SqlDbType.DateTime));
			cmd.Parameters.Add(new SqlParameter("@Bis", SqlDbType.DateTime));
			cmd.Parameters.Add(new SqlParameter("@Bemerkung", SqlDbType.Text));
			cmd.Parameters["@ID"].Value = row.Cells[0].Text;
			cmd.Parameters["@ID_Drucker"].Value = ((DropDownList)row.FindControl("ddl_Drucker")).SelectedValue;
			cmd.Parameters["@Von"].Value = ((TextBox)row.Cells[4].Controls[0]).Text;
			cmd.Parameters["@Bis"].Value = ((TextBox)row.Cells[5].Controls[0]).Text;
			cmd.Parameters["@Bemerkung"].Value = ((TextBox)row.Cells[6].Controls[0]).Text;
			

			cmd.ExecuteNonQuery();
			con.Close();

			gvBindReservations();
		}

		protected void gvReservations_RowEditing(object sender, GridViewEditEventArgs e)
		{
			gvReservations.EditIndex = e.NewEditIndex;
			gvBindReservations();
		}

		protected void gvReservations_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvReservations.PageIndex = e.NewPageIndex;
			gvBindReservations();
		}

		protected void gvReservations_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			gvReservations.EditIndex = -1;
			gvBindReservations();
		}

		protected void btnAddReservation_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/CreateReservation.aspx");
		}

		protected void gvReservationsRowDataBound(object sender, GridViewRowEventArgs e)
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
	}
}