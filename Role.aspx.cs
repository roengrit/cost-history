using CostHistory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostHistory
{
    public partial class Role : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity.Name.ToLower().Contains("admin"))
                    {
                        this.BindData();
                    }
                    else {
                        Response.Redirect(ResolveUrl("~/Default.aspx"));
                    }
                }
                else {
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                }
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

            e.Row.Cells[1].Width = new Unit("300px");

        }

        public void BindData()
        {
            var dt = Connection.GetData(@"select cost_user.* ,
                                                 mis_user.user_name
                                          from cost_user join mis_user on cost_user.user_code = mis_user.code");
            Grid.DataSource = dt;
            Grid.DataBind();            
        }

        protected void Grid_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            string UserCode =  Grid.DataKeys[(int)e.Item.ItemIndex].ToString();
            Connection.ExecuteSqlTransaction(new List<string>() { "delete from cost_user where user_code = '" + UserCode + "'" });
            this.BindData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Connection.ExecuteSqlTransaction(new List<string>() { "insert into cost_user (user_code) values('" + TextBox1.Text + "')" });
            this.BindData();
        }
    }
}