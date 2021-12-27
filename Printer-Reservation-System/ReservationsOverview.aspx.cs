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

			SqlCommand cmd = new SqlCommand("spSelectReservations", con);

			cmd.CommandType = CommandType.StoredProcedure;

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
				gvReservations.Rows[0].Cells[0].Text = "No Reservations Found";
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

			string[] druckbereich = RemoveWhitespace(((TextBox)row.Cells[5].Controls[0]).Text).Split('x');

			cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter("@Marke", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Modell", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Typ", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Beschreibung", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Druckbereich_Laenge", SqlDbType.Float));
			cmd.Parameters.Add(new SqlParameter("@Druckbereich_Breite", SqlDbType.Float));
			cmd.Parameters.Add(new SqlParameter("@Druckbereich_Hoehe", SqlDbType.Float));
			cmd.Parameters["@ID"].Value = row.Cells[0].Text;
			cmd.Parameters["@Marke"].Value = ((TextBox)row.Cells[1].Controls[0]).Text;
			cmd.Parameters["@Modell"].Value = ((TextBox)row.Cells[2].Controls[0]).Text;
			cmd.Parameters["@Typ"].Value = ((TextBox)row.Cells[3].Controls[0]).Text;
			cmd.Parameters["@Beschreibung"].Value = ((TextBox)row.Cells[4].Controls[0]).Text;
			cmd.Parameters["@Druckbereich_Laenge"].Value = druckbereich[0];
			cmd.Parameters["@Druckbereich_Breite"].Value = druckbereich[1];
			cmd.Parameters["@Druckbereich_Hoehe"].Value = druckbereich[2];

			cmd.ExecuteNonQuery();
			con.Close();

			gvBindReservations();
		}

		private void insertPrinter(GridViewRow row)
		{
			con.Open();
			SqlCommand cmd = new SqlCommand("spInsertReservation", con);

			cmd.CommandType = CommandType.StoredProcedure;

			string[] druckbereich = RemoveWhitespace(((TextBox)row.Cells[5].Controls[0]).Text).Split('x');

			cmd.Parameters.Add(new SqlParameter("@Marke", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Modell", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Typ", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Beschreibung", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Druckbereich_Laenge", SqlDbType.Float));
			cmd.Parameters.Add(new SqlParameter("@Druckbereich_Breite", SqlDbType.Float));
			cmd.Parameters.Add(new SqlParameter("@Druckbereich_Hoehe", SqlDbType.Float));
			cmd.Parameters["@Marke"].Value = ((TextBox)row.Cells[1].Controls[0]).Text;
			cmd.Parameters["@Modell"].Value = ((TextBox)row.Cells[2].Controls[0]).Text;
			cmd.Parameters["@Typ"].Value = ((TextBox)row.Cells[3].Controls[0]).Text;
			cmd.Parameters["@Beschreibung"].Value = ((TextBox)row.Cells[4].Controls[0]).Text;
			cmd.Parameters["@Druckbereich_Laenge"].Value = druckbereich[0];
			cmd.Parameters["@Druckbereich_Breite"].Value = druckbereich[1];
			cmd.Parameters["@Druckbereich_Hoehe"].Value = druckbereich[2];

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

		private string RemoveWhitespace(string input)
		{
			return new string(input.ToCharArray()
				.Where(c => !Char.IsWhiteSpace(c))
				.ToArray());
		}

		protected void btnAddReservation_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/CreateReservation.aspx");
		}
	}
}