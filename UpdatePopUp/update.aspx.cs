using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace UpdatePopUp
{
    public partial class update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    String product_id;
                    product_id = row.Cells[0].Text;
                    String product_name;
                    product_name = row.Cells[1].Text;
                    String price;
                    price = row.Cells[2].Text;
                    String quantity;
                    quantity = row.Cells[3].Text;

                    TextBox1.Text = "" + product_id;
                    TextBox2.Text = "" + product_name;
                    TextBox3.Text = "" + price;
                    TextBox4.Text = "" + quantity;


                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
            TextBox1.Visible = true;
            TextBox2.Visible = true;
            TextBox3.Visible = true;
            TextBox4.Visible = true;
            TextBox1.Enabled = false;
            ModalPopupExtender1.Show();

            //TextBox1.Text = GridView1.SelectedRow.Cells[0].Text;
            //TextBox2.Text = GridView1.SelectedRow.Cells[1].Text;
            //TextBox3.Text = GridView1.SelectedRow.Cells[2].Text;
            //TextBox4.Text = GridView1.SelectedRow.Cells[3].Text;
            
        }
        protected void Save(object sender, EventArgs e)
        {

            String mycon = "Data Source=.;Initial Catalog=db_product;Integrated Security=True";
            String updatedata = "Update details_product set product_name='" + TextBox2.Text + "', price='" + TextBox3.Text + "', quantity='" + TextBox4.Text + "' where product_id=" + TextBox1.Text;
            SqlConnection con = new SqlConnection(mycon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = updatedata;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            //Label3.Text = "Roll Number " + TextBox1.Text + " Data Has Been Updated Successfully";
            SqlDataSource1.DataBind();
            GridView1.DataSource = null;
            GridView1.DataSourceID = "SqlDataSource1";
            GridView1.SelectedIndex = -1;

        }
        protected void Delete(object sender, EventArgs e)
        {

            String mycon = "Data Source=.;Initial Catalog=db_product;Integrated Security=True";
            String delete = "delete from details_product where product_id=" + TextBox1.Text;
            SqlConnection con = new SqlConnection(mycon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = delete;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            //Label3.Text = "Roll Number " + Label3.Text + " Data Has Been Deleted Successfully";
            SqlDataSource1.DataBind();
            GridView1.DataSource = null;
            GridView1.DataSourceID = "SqlDataSource1";
            GridView1.SelectedIndex = -1;

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }

        }
    }
}