using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace LKReportingSystem.Forms
{
    public partial class Play : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = GetData();
            GridView1.DataBind();

        }
        public DataSet GetData()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("Product");
            DataRow dr;
            dt.Columns.Add(new DataColumn("Price", typeof(Int32)));
            dt.Columns.Add(new DataColumn("DisCount", typeof(Int32)));
            dt.Columns.Add(new DataColumn("SellPrice", typeof(Int32)));
            for (int i = 1; i <= 10; i++)
            {
                dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i * 2;
                dr[2] = 1 * 3;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            Session["dt"] = dt;
            return ds;
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblCurrentListPrice = (Label)e.Row.FindControl("lblCurrentListPrice");
                Double ValCurrentPrice = Convert.ToDouble(lblCurrentListPrice.Text);

                TextBox txtDiscountOnItem = (TextBox)e.Row.FindControl("txtDiscountOnItem");

                TextBox lblSellPrice = (TextBox)e.Row.FindControl("lblSellPrice");

                txtDiscountOnItem.Attributes.Add("onchange", "CalcSellPrice2(" + ValCurrentPrice + ", '" + txtDiscountOnItem.ClientID + "','" + lblSellPrice.ClientID + "')");


            }

        }
    }
}