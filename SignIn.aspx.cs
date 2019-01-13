using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostHistory
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnLogIn.Text = Resources.Resource.signin;
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                var ds = CostHistory.Models.Connection.GetData(@"select code,user_name ,
                                                                 coalesce((select 1 from cost_user where user_code = mis_user.code limit 1 ),0) as can_export
                                                                from mis_user where code = '" + txtUsername.Text.Trim() + "' and password = '" + txtPassword.Text.Trim() + "'");
                if (ds.Rows.Count > 0)
                {
                    Session["can_export"] = ds.Rows[0]["can_export"].ToString();
                    FormsAuthentication.SetAuthCookie(ds.Rows[0]["user_name"].ToString(), false);
                    //Session.Timeout = 1000;
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                }
                else
                {
                    Label1.Text = "User or password is wrong!";
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
           
        }
    }
}