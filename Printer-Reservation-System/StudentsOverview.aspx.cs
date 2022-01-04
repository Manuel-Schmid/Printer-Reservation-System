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
	public partial class StudentsOverview : System.Web.UI.Page
	{
		readonly SqlConnectionStringBuilder conBuilder = new SqlConnectionStringBuilder();
		SqlConnection con = new SqlConnection();

		protected void Page_Load(object sender, EventArgs e)
		{			
			if (Session["isAdmin"].ToString() == "False")
			{
				gridviewRegistrations.Visible = false;
				gridviewStudents.Columns[0].Visible = false;
				gridviewStudents.Columns[6].Visible = false;
				gridviewStudents.Columns[8].Visible = false;
			}

			conBuilder.DataSource = GlobalVariables.dataSource;
			conBuilder.InitialCatalog = GlobalVariables.dbName;
			conBuilder.IntegratedSecurity = true;
			con.ConnectionString = conBuilder.ConnectionString;
			string userEmail = Session["email"].ToString();

			if (!IsPostBack)
			{
				gvBindAll();
			}

			if (IsSupremeAdmin(userEmail))
			{
				for (int i = 0; i < gridviewStudents.Rows.Count; i++)
				{
					if (gridviewStudents.Rows[i].Cells[3].Text == userEmail)
					{
						TableCell cell = gridviewStudents.Rows[i].Cells[0];
						BoundField field = (BoundField)((DataControlFieldCell)cell).ContainingField;
						field.ReadOnly = true;
					}
				}
			}
			else
			{
				((CheckBoxField)gridviewStudents.Columns[0]).ReadOnly = true;
			}

		}

		private void gvBindAll()
		{
			gvBindStudents();
			gvBindRegistrations();
		}

		protected void gvBindStudents()
		{
			DataTable tblStudents = new DataTable();

			con.Open();

			SqlCommand cmd;

			if (Session["isAdmin"].ToString() == "False") cmd = new SqlCommand("spSelectStudent", con);
			else cmd = new SqlCommand("spSelectStudents", con);

			cmd.CommandType = CommandType.StoredProcedure;

			if (Session["isAdmin"].ToString() == "False")
			{
				cmd.Parameters.Add(new SqlParameter("@eMail", SqlDbType.VarChar));
				cmd.Parameters["@eMail"].Value = Session["email"].ToString();
			}

			SqlDataAdapter dap = new SqlDataAdapter(cmd);

			dap.Fill(tblStudents);
			con.Close();
			if (tblStudents.Rows.Count > 0)
			{
				gridviewStudents.DataSource = tblStudents;
				gridviewStudents.DataBind();
			}
			else
			{
				tblStudents.Rows.Add(tblStudents.NewRow());
				gridviewStudents.DataSource = tblStudents;
				gridviewStudents.DataBind();
				int columncount = gridviewStudents.Rows[0].Cells.Count;
				gridviewStudents.Rows[0].Cells.Clear();
				gridviewStudents.Rows[0].Cells.Add(new TableCell());
				gridviewStudents.Rows[0].Cells[0].ColumnSpan = columncount;
				gridviewStudents.Rows[0].Cells[0].Text = "No Students Found";
			}
		}

		private bool IsSupremeAdmin(string eMail)
		{
			con.Open();

			SqlCommand cmd = new SqlCommand("spSelectIsSupremeAdmin", con);

			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@eMail", SqlDbType.VarChar));
			cmd.Parameters["@eMail"].Value = eMail;

			bool isSupreme = ((int)cmd.ExecuteScalar() >= 1);
			con.Close();
			return isSupreme;
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

		protected void gridviewStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridViewRow row = (GridViewRow)gridviewStudents.Rows[e.RowIndex];
			con.Open();
			SqlCommand cmd = new SqlCommand("spDeleteStudent", con);

			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@eMail", SqlDbType.VarChar));
			cmd.Parameters["@eMail"].Value = row.Cells[3].Text;

			cmd.ExecuteNonQuery();
			con.Close();
			gvBindStudents();
		}

		protected void gridviewStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			GridViewRow row = (GridViewRow)gridviewStudents.Rows[e.RowIndex];

			gridviewStudents.EditIndex = -1;
			con.Open();
			SqlCommand cmd = new SqlCommand("spUpdateStudent", con);

			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Vorname", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@currentEMail", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Handy", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Bemerkung", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", SqlDbType.VarChar));
			cmd.Parameters["@IsAdmin"].Value = ((CheckBox)row.Cells[0].Controls[0]).Checked;
			cmd.Parameters["@Name"].Value = ((TextBox)row.Cells[1].Controls[0]).Text;
			cmd.Parameters["@Vorname"].Value = ((TextBox)row.Cells[2].Controls[0]).Text;
			cmd.Parameters["@currentEMail"].Value = row.Cells[3].Text;
			cmd.Parameters["@Handy"].Value = ((TextBox)row.Cells[4].Controls[0]).Text;
			cmd.Parameters["@Bemerkung"].Value = ((TextBox)row.Cells[5].Controls[0]).Text;
			if (Session["isAdmin"].ToString() == "False") cmd.Parameters["@Status"].Value = 0;
			else cmd.Parameters["@Status"].Value = ((DropDownList)row.FindControl("ddl_Status")).SelectedValue;

			cmd.ExecuteNonQuery();
			con.Close();

			gvBindAll();
		}

		protected void gridviewStudents_RowEditing(object sender, GridViewEditEventArgs e)
		{
			gridviewStudents.EditIndex = e.NewEditIndex;
			gvBindStudents();
		}

		protected void gridviewStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gridviewStudents.PageIndex = e.NewPageIndex;
			gvBindStudents();
		}

		protected void gridviewStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			gridviewStudents.EditIndex = -1;
			gvBindStudents();
		}

		// registrations

		protected void gvBindRegistrations()
		{
			DataTable tblRegistrations = new DataTable();

			con.Open();

			SqlCommand cmd = new SqlCommand("spSelectRegistrations", con);

			cmd.CommandType = CommandType.StoredProcedure;

			SqlDataAdapter dap = new SqlDataAdapter(cmd);

			dap.Fill(tblRegistrations);
			con.Close();
			if (tblRegistrations.Rows.Count > 0)
			{
				gridviewRegistrations.DataSource = tblRegistrations;
				gridviewRegistrations.DataBind();
			}
			else
			{
				tblRegistrations.Rows.Add(tblRegistrations.NewRow());
				gridviewRegistrations.DataSource = tblRegistrations;
				gridviewRegistrations.DataBind();
				int columncount = gridviewRegistrations.Rows[0].Cells.Count;
				gridviewRegistrations.Rows[0].Cells.Clear();
				gridviewRegistrations.Rows[0].Cells.Add(new TableCell());
				gridviewRegistrations.Rows[0].Cells[0].ColumnSpan = columncount;
				gridviewRegistrations.Rows[0].Cells[0].Text = "No Registrations Found";
			}
		}

		protected void gridviewRegistrations_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gridviewRegistrations.PageIndex = e.NewPageIndex;
			gvBindRegistrations();
		}

		protected void gridviewRegistrations_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int index = Convert.ToInt32(e.CommandArgument);
			GridViewRow row = (GridViewRow)gridviewRegistrations.Rows[index];

			con.Open();
			SqlCommand cmd = new SqlCommand("spUpdateStudentStatus", con);

			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@eMail", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@NewStatusID", SqlDbType.Int));
			cmd.Parameters["@eMail"].Value = row.Cells[2].Text;
			if (e.CommandName == "accept")
			{
				cmd.Parameters["@NewStatusID"].Value = 2; // change status to active
			} else if (e.CommandName == "deny")
			{
				cmd.Parameters["@NewStatusID"].Value = 4; // change status to ended
			}
			else
			{
				con.Close();
				return;
			}
			cmd.ExecuteNonQuery();
			con.Close();

			gvBindAll();
		}

		private DataTable getStatuses()
		{
			DataTable tblStatuses = new DataTable();

			con.Open();

			SqlCommand cmd = new SqlCommand("spSelectStatuses", con);

			cmd.CommandType = CommandType.StoredProcedure;

			SqlDataAdapter dap = new SqlDataAdapter(cmd);

			dap.Fill(tblStatuses);
			con.Close();

			return tblStatuses;
		}

		protected void gv_StatusRowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if ((e.Row.RowState & DataControlRowState.Edit) > 0)
				{
					DropDownList ddList = (DropDownList)e.Row.FindControl("ddl_Status");

					DataTable dt = getStatuses();
					ddList.DataSource = dt;
					ddList.DataTextField = "Status";
					ddList.DataValueField = "Status_ID";
					ddList.DataBind();

					DataRowView dr = e.Row.DataItem as DataRowView;
					ddList.SelectedValue = dr["Stat_ID"].ToString();
				}
			}
		}

		protected void btnLogout_Click(object sender, EventArgs e)
		{
			//Session["email"] = "";
			//Session["isAdmin"] = false;
			Response.Redirect("~/Login.aspx");
		}
	}
}